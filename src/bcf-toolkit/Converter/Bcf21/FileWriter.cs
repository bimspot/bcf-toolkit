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
    if (Directory.Exists(target)) Directory.Delete(target, true);
    Directory.CreateDirectory(target);

    // Writing markups to disk, one markup per file.
    var writeTasks = bcf.Markups
      .Select(markup => {
        var pathMarkup = $"{target}/{markup.GetTopic().Guid}.json";
        return JsonExtensions.WriteJson(pathMarkup, markup);
      })
      .ToList();

    var pathProject = $"{target}/project.json";
    writeTasks.Add(JsonExtensions.WriteJson(pathProject, bcf.Project));

    var pathVersion = $"{target}/version.json";
    writeTasks.Add(JsonExtensions.WriteJson(pathVersion, bcf.Version));

    return Task.WhenAll(writeTasks);
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
  /// <returns>Generated stream from bcf zip</returns>
  /// <exception cref="ApplicationException"></exception>
  public static Task<Stream> WriteBcfToStream(IBcf bcf) {
    var bcfObject = (Bcf)bcf;

    var ms = new MemoryStream();
    using var zip = new ZipArchive(ms, ZipArchiveMode.Create, true);
    // Write bcf.version
    BcfExtensions.CreateBcfZipEntry(zip, "bcf.version", new Version());

    // Writing markup files to disk, one markup per folder.
    foreach (var markup in bcfObject.Markups) {
      var guid = markup.GetTopic()?.Guid;
      if (guid == null) {
        Console.WriteLine(" - Topic Guid is missing, skipping markup");
        continue;
      }

      var topicFolder = $"{guid}";

      BcfExtensions.CreateBcfZipEntry(zip, $"{topicFolder}/markup.bcf", markup);

      var visInfo = (VisualizationInfo)markup.GetFirstViewPoint()?.GetVisualizationInfo()!;
      BcfExtensions.CreateBcfZipEntry(zip, $"{topicFolder}/viewpoint.bcf", visInfo);

      // Write snapshot
      var snapshotFileName = markup.GetFirstViewPoint()?.Snapshot;
      var base64String = markup.GetFirstViewPoint()?.SnapshotData;
      if (snapshotFileName == null || base64String == null) continue;
      const string pattern = @"^data:image\/[a-zA-Z]+;base64,";
      var result = Regex.Replace(base64String, pattern, string.Empty);
      var bytes = Convert.FromBase64String(result);
      BcfExtensions.CreateBcfZipEntry(zip, $"{topicFolder}/{snapshotFileName}", bytes);
    }

    BcfExtensions.CreateBcfZipEntry(zip, "project.bcf", bcfObject.Project);

    return Task.FromResult<Stream>(ms);
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
  public static async Task<string> WriteBcfToFolder(IBcf bcf, string target, bool delete = true) {
    var targetFolder = Path.GetDirectoryName(target);
    if (targetFolder == null)
      throw new ApplicationException(
        $"Target folder not found ${targetFolder}");

    // Creating a tmp folder for the intermediate files.
    var tmpFolder = $"{targetFolder}/tmp{Path.GetFileNameWithoutExtension(target)}";
    if (Directory.Exists(tmpFolder)) Directory.Delete(tmpFolder, true);
    Directory.CreateDirectory(tmpFolder);

    var bcfObject = (Bcf)bcf;

    var writeTasks = new List<Task>();
    writeTasks.Add(BcfExtensions.WriteBcfFile(
      tmpFolder,
      "bcf.version",
      new Version()));

    // Writing markup files to disk, one markup per folder.
    foreach (var markup in bcfObject.Markups) {
      var guid = markup.GetTopic()?.Guid;
      if (guid == null) {
        Console.WriteLine(
          " - Topic Guid is missing, skipping markup");
        continue;
      }

      var topicFolder = $"{tmpFolder}/{guid}";
      Directory.CreateDirectory(topicFolder);

      writeTasks.Add(BcfExtensions.WriteBcfFile(
        topicFolder,
        "markup.bcf",
        markup));

      var visInfo =
        (VisualizationInfo)markup.GetFirstViewPoint()?.GetVisualizationInfo()!;
      writeTasks.Add(
        BcfExtensions.WriteBcfFile(
          topicFolder,
          "viewpoint.bcfv",
          visInfo));

      var snapshotFileName = markup.GetFirstViewPoint()?.Snapshot;
      var base64String = markup.GetFirstViewPoint()?.SnapshotData;
      if (snapshotFileName == null || base64String == null) continue;
      const string pattern = @"^data:image\/[a-zA-Z]+;base64,";
      var result = Regex.Replace(base64String,
        pattern, string.Empty);
      writeTasks.Add(File.WriteAllBytesAsync(
        $"{topicFolder}/{snapshotFileName}",
        Convert.FromBase64String(result)));
    }

    writeTasks.Add(
      BcfExtensions.WriteBcfFile(
        tmpFolder,
        "project.bcfp",
        bcfObject.Project));

    await Task.WhenAll(writeTasks);

    Console.WriteLine($"Zipping the output: {target}");
    if (File.Exists(target)) File.Delete(target);
    ZipFile.CreateFromDirectory(tmpFolder, target);

    if (delete)
      Directory.Delete(tmpFolder, true);

    return tmpFolder;
  }
}