using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BcfToolkit.Builder.Bcf30;
using BcfToolkit.Utils;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf30;
using File = System.IO.File;
using Version = BcfToolkit.Model.Bcf30.Version;

namespace BcfToolkit.Converter.Bcf30;

/// <summary>
///   Converter strategy class for converting BCF 3.0 files to JSON
///   and back.
/// </summary>
public class Converter : IConverter {

  private BcfBuilder _builder = new();

  private readonly Dictionary<Type, Func<Bcf,IBcf>> _converterFnMapper = new();

  public Converter() {
    _converterFnMapper[typeof(Model.Bcf21.Bcf)] = SchemaConverterToBcf21.Convert;
    _converterFnMapper[typeof(Bcf)] = b => b;
  }

  public async Task BcfZipToJson(Stream source, string target) {
    var builder = new BcfBuilder();
    var bcf = await builder.BuildFromStream(source);

    // Writing json files
    await WriteJson(bcf, target);
  }

  public async Task BcfZipToJson(string source, string target) {
    await using var fileStream = new FileStream(source, FileMode.Open, FileAccess.Read);
    await BcfZipToJson(fileStream, target);
  }

  public async Task JsonToBcfZip(string source, string target) {
    // Parsing BCF root files
    var extensions =
      await JsonExtensions.ParseObject<Extensions>($"{source}/extensions.json");
    var project =
      await JsonExtensions.ParseObject<ProjectInfo>($"{source}/project.json");
    var documents =
      await JsonExtensions.ParseObject<DocumentInfo>($"{source}/documents.json");

    // Parsing markups
    var markups = await JsonExtensions.ParseMarkups<Markup>(source);

    var bcf = new Bcf {
      Markups = markups,
      Extensions = extensions,
      Project = project,
      Document = documents,
      Version = new Version()
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
    return WriteJson((Bcf)bcf, target);
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
  /// <param name="bcf">The BCF object which will be written.</param>
  /// <param name="target">The target path where the json files will be saved.</param>
  /// <returns></returns>
  private static Task WriteJson(Bcf bcf, string target) {
    // Creating the target folder
    if (Directory.Exists(target)) Directory.Delete(target, true);
    Directory.CreateDirectory(target);

    // Writing markups to disk, one markup per file.
    var tasks = bcf.Markups
      .Select(markup => {
        var pathMarkup = $"{target}/{markup.GetTopic().Guid}.json";
        return JsonExtensions.WriteJson<Markup>(pathMarkup, markup);
      })
      .ToList();

    // Writing BCF extensions file
    var pathExt = $"{target}/extensions.json";
    tasks.Add(JsonExtensions.WriteJson(pathExt, bcf.Extensions));

    // Writing BCF project file
    var pathProject = $"{target}/project.json";
    tasks.Add(JsonExtensions.WriteJson(pathProject, bcf.Project));

    // Writing BCF document file
    var pathDoc = $"{target}/documents.json";
    tasks.Add(JsonExtensions.WriteJson(pathDoc, bcf.Document));

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
  /// <param name="delete">Should delete the generated tmp folder now or later.</param>
  /// <returns>Temp folder path</returns>
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

    // Writing extensions file
    tasks.Add(BcfExtensions.WriteBcfFile(tmpFolder, "extensions.xml", bcf.Extensions));
    // Writing project file
    tasks.Add(BcfExtensions.WriteBcfFile(tmpFolder, "project.bcfp", bcf.Project));
    // Writing documents file
    tasks.Add(BcfExtensions.WriteBcfFile(tmpFolder, "documents.xml", bcf.Document));

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