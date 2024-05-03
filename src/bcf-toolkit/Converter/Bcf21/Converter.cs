using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BcfToolkit.Builder.Bcf21;
using BcfToolkit.Utils;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf21;
using Version = BcfToolkit.Model.Bcf21.Version;

namespace BcfToolkit.Converter.Bcf21;

/// <summary>
///   Converter strategy class for converting BCF 2.1 to different versions,
///   JSON, and BCFzip.
/// </summary>
public class Converter : IConverter {

  private BcfBuilder _builder = new();

  private readonly Dictionary<Type, Func<Bcf, IBcf>> _converterFnMapper = new();

  public Converter() {
    _converterFnMapper[typeof(Model.Bcf30.Bcf)] = SchemaConverterToBcf30.Convert;
    _converterFnMapper[typeof(Bcf)] = b => b;
  }

  public async Task BcfZipToJson(Stream source, string target) {
    var bcf = await _builder.BuildFromStream(source);

    // Writing json files
    await WriteJson(target, bcf);
  }

  public async Task BcfZipToJson(string sourcePath, string target) {
    try {
      await using var fileStream =
        new FileStream(sourcePath, FileMode.Open, FileAccess.Read);
      await BcfZipToJson(fileStream, target);
    }
    catch (Exception ex) {
      throw new ArgumentException($"Source path is not readable. {ex.Message}", ex);
    }
  }

  public async Task JsonToBcfZip(string source, string target) {
    // Parsing BCF project - it is an optional file
    var projectPath = $"{source}/project.json";
    var project = Path.Exists(projectPath)
      ? await JsonExtensions.ParseObject<ProjectExtension>(projectPath)
      : new ProjectExtension();

    // Parsing markups
    var markups = await JsonExtensions.ParseMarkups<Markup>(source);

    var bcf = new Bcf {
      Markups = markups,
      Project = project
    };

    // Writing bcf files
    await WriteBcf(bcf, target);
  }

  public async Task<Stream> ToBcfStream(IBcf bcf) {
    var workingDir = Directory.GetCurrentDirectory();
    var bcfTargetPath = workingDir + "/bcf.bcfzip";

    var tmpFolder = await WriteBcf((Bcf)bcf, bcfTargetPath, false);

    var stream = new FileStream(bcfTargetPath, FileMode.Open, FileAccess.Read);

    // After the filestream is ready we can delete the folders
    Directory.Delete(tmpFolder, true);
    File.Delete(bcfTargetPath);

    return stream;
  }

  public Task ToBcfZip(IBcf bcf, string target) {
    return WriteBcf((Bcf)bcf, target);
  }

  public Task ToJson(IBcf bcf, string target) {
    return WriteJson(target, (Bcf)bcf);
  }

  public async Task<T> BuildBcfFromStream<T>(Stream stream) {
    var bcf = await _builder.BuildFromStream(stream);
    var targetVersion = typeof(T);
    var converterFn = _converterFnMapper[targetVersion];
    return (T)converterFn(bcf);
  }

  /// <summary>
  ///   The method writes the BCF object to json file.
  /// </summary>
  /// <param name="targetPath">The target path where the json files will be saved.</param>
  /// <param name="bcf">The BCF object which will be written.</param>
  /// <returns></returns>
  private static Task WriteJson(string targetPath, Bcf bcf) {
    // Creating the target folder
    if (Directory.Exists(targetPath)) Directory.Delete(targetPath, true);
    Directory.CreateDirectory(targetPath);

    // Writing markups to disk, one markup per file.
    var tasks = bcf.Markups
      .Select(markup => {
        var pathMarkup = $"{targetPath}/{markup.GetTopic().Guid}.json";
        return JsonExtensions.WriteJson(pathMarkup, markup);
      })
      .ToList();

    // Writing BCF project file
    var pathProject = $"{targetPath}/project.json";
    tasks.Add(JsonExtensions.WriteJson(pathProject, bcf.Project));

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
  private static async Task<string> WriteBcf(Bcf bcf, string target, bool delete = true) {
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
    tasks.Add(BcfExtensions.WriteBcfFile(tmpFolder, "bcf.version", new Version()));

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
      var result = Regex.Replace(base64String,
        @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
      tasks.Add(File.WriteAllBytesAsync(
        $"{topicFolder}/{snapshotFileName}",
        Convert.FromBase64String(result)));
    }

    // Writing project file
    tasks.Add(BcfExtensions.WriteBcfFile(tmpFolder, "project.bcfp", bcf.Project));

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