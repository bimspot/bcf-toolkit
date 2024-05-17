using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf21;
using BcfToolkit.Utils;
using Version = BcfToolkit.Model.Bcf21.Version;

namespace BcfToolkit.Converter.Bcf21;

public static class FileWriter {
  /// <summary>
  ///   The method writes the BCF object to json file.
  /// </summary>
  /// <param name="bcf">The BCF object which will be written.</param>
  /// <param name="target">The target path where the json files will be saved.</param>
  /// <returns></returns>
  public static Task WriteJson(Bcf bcf, string target) {
    // Creating the target folder
    if (Directory.Exists(target)) Directory.Delete(target, true);
    Directory.CreateDirectory(target);

    // Writing markups to disk, one markup per file.
    var tasks = bcf.Markups
      .Select(markup => {
        var pathMarkup = $"{target}/{markup.GetTopic().Guid}.json";
        return JsonExtensions.WriteJson(pathMarkup, markup);
      })
      .ToList();

    // Writing BCF project file
    var pathProject = $"{target}/project.json";
    tasks.Add(JsonExtensions.WriteJson(pathProject, bcf.Project));

    // Writing BCF version file
    var pathVersion = $"{target}/version.json";
    tasks.Add(JsonExtensions.WriteJson(pathVersion, bcf.Version));

    return Task.WhenAll(tasks);
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
  /// <param name="bcf">The BCF object.</param>
  /// <param name="target">The target file name of the BCFzip.</param>
  /// <param name="delete">Should delete the generated tmp folder now or later</param>
  /// <returns>Generated temp folder path</returns>
  /// <exception cref="ApplicationException"></exception>
  public static async Task<string> WriteBcf(IBcf bcf, string target, bool delete = true) {
    var targetFolder = Path.GetDirectoryName(target);
    if (targetFolder == null)
      throw new ApplicationException(
        $"Target folder not found ${targetFolder}");

    // Will create a tmp folder for the intermediate files.
    var tmpFolder = $"{targetFolder}/tmp";
    if (Directory.Exists(tmpFolder)) Directory.Delete(tmpFolder, true);
    Directory.CreateDirectory(tmpFolder);

    // cast bcf to object
    var bcfObject = (Bcf)bcf;

    var tasks = new List<Task>();

    // Creating the version file
    tasks.Add(BcfExtensions.WriteBcfFile(tmpFolder, "bcf.version", new Version()));

    // Writing markup folders and files
    foreach (var markup in bcfObject.Markups) {
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
      tasks.Add(BcfExtensions.WriteBcfFile(topicFolder, "markup.bcf", markup));

      // Viewpoint
      var visInfo =
        (VisualizationInfo)markup.GetFirstViewPoint()?.GetVisualizationInfo()!;
      tasks.Add(BcfExtensions.WriteBcfFile(topicFolder, "viewpoint.bcfv",
        visInfo));

      // Snapshot
      var snapshotFileName = markup.GetFirstViewPoint()?.Snapshot;
      var base64String = markup.GetFirstViewPoint()?.SnapshotData;
      if (snapshotFileName == null || base64String == null) continue;
      const string pattern = @"^data:image\/[a-zA-Z]+;base64,";
      var result = Regex.Replace(base64String,
        pattern, string.Empty);
      tasks.Add(File.WriteAllBytesAsync(
        $"{topicFolder}/{snapshotFileName}",
        Convert.FromBase64String(result)));
    }

    // Writing project file
    tasks.Add(BcfExtensions.WriteBcfFile(tmpFolder, "project.bcfp", bcfObject.Project));

    // Waiting for all the file writing
    await Task.WhenAll(tasks);

    // zip shit
    Console.WriteLine($"Zipping the output: {target}");
    if (File.Exists(target)) File.Delete(target);
    ZipFile.CreateFromDirectory(tmpFolder, target);

    if (delete)
      Directory.Delete(tmpFolder, true);

    return tmpFolder;
  }
}