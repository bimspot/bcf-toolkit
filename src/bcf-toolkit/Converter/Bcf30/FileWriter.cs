using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf30;
using BcfToolkit.Utils;
using File = System.IO.File;
using Version = System.Version;

namespace BcfToolkit.Converter.Bcf30;

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
  public static async Task<string> WriteBcf(IBcf bcf, string target, bool delete = true) {
    var targetFolder = Path.GetDirectoryName(target);
    if (targetFolder == null)
      throw new ApplicationException(
        $"Target folder not found ${targetFolder}");

    // Will create a tmp folder for the intermediate files.
    var tmpFolder = $"{targetFolder}/tmp";
    if (Directory.Exists(tmpFolder)) Directory.Delete(tmpFolder, true);
    Directory.CreateDirectory(tmpFolder);

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
      var result = Regex.Replace(base64String,
        @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
      tasks.Add(File.WriteAllBytesAsync(
        $"{topicFolder}/{snapshotFileName}",
        Convert.FromBase64String(result)));
    }

    // Writing extensions file
    tasks.Add(BcfExtensions.WriteBcfFile(tmpFolder, "extensions.xml", bcfObject.Extensions));
    // Writing project file
    tasks.Add(BcfExtensions.WriteBcfFile(tmpFolder, "project.bcfp", bcfObject.Project));
    // Writing documents file
    tasks.Add(BcfExtensions.WriteBcfFile(tmpFolder, "documents.xml", bcfObject.Document));

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