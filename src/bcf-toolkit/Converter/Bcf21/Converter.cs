using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf21;
using Version = BcfToolkit.Model.Bcf21.Version;

namespace BcfToolkit.Converter.Bcf21;

/// <summary>
///   Converter strategy class for converting BCF 2.1 files to JSON
///   and back.
/// </summary>
public class Converter : IConverter {
  /// <summary>
  ///   The method parses the BCF file of version 2.1 and writes into JSON.
  ///   The root of the BCF zip contains the following files:
  ///   - project.bcfp (optional)
  ///   - bcf.version
  ///   Topic folder structure inside a BCFzip archive:
  ///   - markup.bcf
  ///   Additionally:
  ///   - Viewpoint files (BCFV)
  ///   - Snapshot files (PNG/JPEG)
  ///   - Bitmaps
  /// </summary>
  /// <param name="source">The source stream of the BCFzip.</param>
  /// <param name="target">The target path where the JSON is written.</param>
  public async Task BcfToJson(Stream source, string target) {
    // Parsing BCF root file structure
    var project = await BcfConverter.ParseProject<ProjectExtension>(source);

    // Parsing topics folder (markups)
    var markups =
      await BcfConverter.ParseMarkups<Markup, VisualizationInfo>(source);

    var bcf = new Bcf {
      Markups = markups,
      Project = project
    };

    // Writing json files
    await WriteJson(target, bcf);
  }

  /// <summary>
  ///   The method parses the BCF file of version 2.1 and writes into JSON.
  ///   The root of the BCF zip contains the following files:
  ///   - project.bcfp (optional)
  ///   - bcf.version
  ///   Topic folder structure inside a BCFzip archive:
  ///   - markup.bcf
  ///   Additionally:
  ///   - Viewpoint files (BCFV)
  ///   - Snapshot files (PNG/JPEG)
  ///   - Bitmaps
  /// </summary>
  /// <param name="source">The path to the BCFzip.</param>
  /// <param name="target">The target path where the JSON is written.</param>
  public async Task BcfToJson(string source, string target) {
    try {
      await using FileStream fileStream = new FileStream(source, FileMode.Open, FileAccess.Read);
      await BcfToJson(fileStream, target);
    }
    catch (Exception ex) {
      throw new ArgumentException("Source path is not readable.");
    }
  }

  /// <summary>
  ///   The method writes the BCF object to json file.
  /// </summary>
  /// <param name="target">The target path where the json files will be saved.</param>
  /// <param name="bcf">The BCF object which will be written.</param>
  /// <returns></returns>
  private static async Task WriteJson(string target, Bcf bcf) {
    // Creating the target folder
    if (Directory.Exists(target)) Directory.Delete(target, true);
    Directory.CreateDirectory(target);

    // Writing markups to disk, one markup per file.
    var tasks = bcf.Markups
      .Select(markup => {
        var pathMarkup = $"{target}/{markup.GetTopic().Guid}.json";
        return JsonConverter.WriteJson(pathMarkup, markup);
      })
      .ToList();

    // Writing BCF project file
    var pathProject = $"{target}/project.json";
    tasks.Add(JsonConverter.WriteJson(pathProject, bcf.Project));

    await Task.WhenAll(tasks);
  }

  /// <summary>
  ///   The method reads the JSON files and creates BCF 2.1 version.
  ///   The json folder must contain files which are named using the
  ///   `uuid` of the `Topic` within, and `project.json` optionally.
  /// </summary>
  /// <param name="source">The source folder to the JSON files.</param>
  /// <param name="target">The target path where the BCF is written.</param>
  public async Task JsonToBcf(string source, string target) {
    // Parsing BCF project - it is an optional file
    var projectPath = $"{source}/project.json";
    var project = Path.Exists(projectPath)
      ? await JsonConverter.ParseObject<ProjectExtension>(projectPath)
      : new ProjectExtension();

    // Parsing markups
    var markups = await JsonConverter.ParseMarkups<Markup>(source);

    var bcf = new Bcf {
      Markups = markups,
      Project = project
    };

    // Writing bcf files
    await WriteBcf(target, bcf);
  }

  /// <summary>
  ///   The method writes the BCF content from the given objects to the
  ///   specified target and compresses it.
  ///   The markups will be written into the topic folder structure:
  ///   * markup.bcf
  ///   * viewpoint files (.bcfv)
  ///   * snapshot files (PNG, JPEG)
  ///   The root files depend on the version of the BCF.
  ///   * project.bcfp (optional)
  ///   * bcf.version
  /// </summary>
  /// <param name="target">The target file name of the BCFzip.</param>
  /// <param name="bcf">The BCF object.</param>
  /// <returns></returns>
  /// <exception cref="ApplicationException"></exception>
  private static async Task WriteBcf(string target, Bcf bcf) {
    var targetFolder = Path.GetDirectoryName(target);
    if (targetFolder == null)
      throw new ApplicationException(
        $"Target folder not found ${targetFolder}");

    // Will create a tmp folder for the intermediate files.
    var tmpFolder = $"{targetFolder}/tmp";
    if (Directory.Exists(tmpFolder)) Directory.Delete(tmpFolder, true);
    Directory.CreateDirectory(tmpFolder);

    var tasks = new List<Task>();

    // Creating the version file
    tasks.Add(BcfConverter.WriteBcfFile(tmpFolder, "bcf.version", new Version()));

    // Writing markup folders and files
    foreach (var markup in bcf.Markups) {
      // Creating the target folder
      var guid = markup.GetTopic()?.Guid;
      if (guid == null) {
        Console.WriteLine(
          " - Topic Guid is missing, skipping markup");
        continue;
      }

      var topicFolder = $"{tmpFolder}/{guid}";
      Directory.CreateDirectory(topicFolder);

      // Markup
      tasks.Add(BcfConverter.WriteBcfFile(topicFolder, "markup.bcf", markup));

      // Viewpoint
      var visInfo =
        (VisualizationInfo)markup.GetFirstViewPoint()?.GetVisualizationInfo()!;
      tasks.Add(BcfConverter.WriteBcfFile(topicFolder, "viewpoint.bcfv",
        visInfo));

      // Snapshot
      var snapshotFileName = markup.GetFirstViewPoint()?.Snapshot;
      var base64String = markup.GetFirstViewPoint()?.SnapshotData;
      if (snapshotFileName == null || base64String == null) continue;
      var result = Regex.Replace(base64String,
        @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
      tasks.Add(File.WriteAllBytesAsync(
        $"{topicFolder}/{snapshotFileName}",
        Convert.FromBase64String(result)));
    }

    // Writing project file
    tasks.Add(BcfConverter.WriteBcfFile(tmpFolder, "project.bcfp", bcf.Project));

    // Waiting for all the file writing
    await Task.WhenAll(tasks);

    // zip shit
    Console.WriteLine($"Zipping the output: {target}");
    if (File.Exists(target)) File.Delete(target);
    ZipFile.CreateFromDirectory(tmpFolder, target);
    Directory.Delete(tmpFolder, true);
  }

  /// <summary>
  ///   The method writes the specified BCF 2.1 models to BCF 2.1 files.
  /// </summary>
  /// <param name="target">The target path where the BCF is written.</param>
  /// <param name="bcf">The `IBcf` interface of the BCF.</param>
  /// <returns></returns>
  public Task ToBcf(string target, IBcf bcf) {
    return WriteBcf(target, (Bcf)bcf);
  }

  /// <summary>
  ///   The method writes the specified BCF 2.1 models to JSON files.
  /// </summary>
  /// <param name="target">The target path where the JSON is written.</param>
  /// <param name="bcf">The `IBcf` interface of the BCF.</param>
  /// <returns></returns>
  public Task ToJson(string target, IBcf bcf) {
    return WriteJson(target, (Bcf)bcf);
  }
}