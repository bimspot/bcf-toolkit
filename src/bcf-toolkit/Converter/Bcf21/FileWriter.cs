using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf21;
using BcfToolkit.Model.Interfaces;
using BcfToolkit.Utils;
using Version = BcfToolkit.Model.Bcf21.Version;

namespace BcfToolkit.Converter.Bcf21;

public static class FileWriter {
  /// <summary>
  ///   The method writes the BCF object to json file.
  /// </summary>
  /// <param name="bcf">The `Bcf` object that should be written.</param>
  /// <param name="target">The target path where the json files will be saved.</param>
  /// <returns></returns>
  public static Task WriteBcfToJson(Bcf bcf, string target) {
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
  ///   The method serializes the BCF content to xml from the given object,
  ///   then either saves the xml to the target file or creates a zip entry
  ///   from a memory stream based on the input. It returns a stream of the
  ///   archive.
  /// 
  ///   The markups will be written into the topic folder structure:
  ///   * markup.bcf
  ///   * viewpoint files (.bcfv)
  ///   * snapshot files (PNG, JPEG)
  ///   The root files depend on the version of the BCF.
  ///   * project.bcfp (optional)
  ///   * bcf.version
  /// 
  ///   WARNING: Disposing the stream is the responsibility of the user!
  /// </summary>
  /// <param name="bcf">The `BCF` object that should be written.</param>
  /// <param name="cancellationToken"></param>
  /// <returns>It returns a stream of the archive.</returns>
  public static async Task<Stream> SerializeAndWriteBcf(IBcf bcf,
    CancellationToken? cancellationToken) {
    var workingDir = Directory.GetCurrentDirectory();
    var tmpBcfTargetPath = workingDir + $"/{Guid.NewGuid()}.bcfzip";
    var tmpFolder =
      await SerializeAndWriteBcfToFolder(bcf, tmpBcfTargetPath, false,
        cancellationToken);
    var fileStream =
      new FileStream(tmpBcfTargetPath, FileMode.Open, FileAccess.Read);

    Directory.Delete(tmpFolder, true);
    File.Delete(tmpBcfTargetPath);

    return fileStream;
  }

  /// <summary>
  ///   The method serializes the BCF content to xml from the given object,
  ///   creates a zip entry from to the specified stream.
  /// </summary>
  /// <param name="bcf">The `Bcf` object that should be written.</param>
  /// <param name="zip">The zip archive which the object is written in.</param>
  /// <param name="cancellationToken"></param>
  /// <returns>Generated stream from bcf zip.</returns>
  /// <exception cref="ApplicationException"></exception>
  public static void SerializeAndWriteBcfToStream(
    IBcf bcf,
    ZipArchive zip,
    CancellationToken? cancellationToken = null) {
    var bcfObject = (Bcf)bcf;

    zip.CreateEntryFromObject("bcf.version", new Version());

    // Writing markup files to zip archive, one markup per entry.
    foreach (var markup in bcfObject.Markups) {
      if (cancellationToken is { IsCancellationRequested: true }) {
        return;
      }

      var guid = markup.GetTopic()?.Guid;
      if (guid == null) {
        Log.Debug(" - Topic Guid is missing, skipping markup");
        continue;
      }

      var topicFolder = $"{guid}";

      zip.CreateEntryFromObject($"{topicFolder}/markup.bcf", markup);

      foreach (var viewpoint in markup.Viewpoints) {
        zip.CreateEntryFromObject($"{topicFolder}/{viewpoint.Viewpoint}", viewpoint.VisualizationInfo);

        var snapshotFileName = viewpoint.Snapshot;
        var snapshotBase64String = viewpoint.SnapshotData?.Data;
        if (string.IsNullOrEmpty(snapshotFileName) || snapshotBase64String == null)
          continue;
        var snapshotBytes = Convert.FromBase64String(snapshotBase64String);
        zip.CreateEntryFromBytes($"{topicFolder}/{snapshotFileName}", snapshotBytes);
      }

      //Additional files can be referenced by other files via their relative
      //paths. It is recommended to put them in a folder called Documents in the
      //root folder of the zip archive.
      var internalDocuments = markup
        .Topic.DocumentReference
        .Where(d => !d.IsExternal);
      foreach (var document in internalDocuments) {
        var documentFileName = Path.GetFileName(document.ReferencedDocument);
        var documentBase64String = document.DocumentData.Data;
        if (string.IsNullOrEmpty(documentFileName)) continue;
        var documentBytes = Convert.FromBase64String(documentBase64String);
        zip.CreateEntryFromBytes($"documents/{documentFileName}", documentBytes);
      }
    }

    zip.CreateEntryFromObject("project.bcfp", bcfObject.Project);
  }

  /// <summary>
  ///   The method writes the BCF content from the given objects to the
  ///   specified target and compresses it. The folder is deleted 
  /// </summary>
  /// <param name="bcf">The BCF object.</param>
  /// <param name="target">The target file name of the BCFzip.</param>
  /// <param name="delete">Should delete the generated tmp folder now or later.</param>
  /// <param name="cancellationToken"></param>
  /// <returns>Generated temp folder path.</returns>
  /// <exception cref="ApplicationException"></exception>
  public static async Task<string> SerializeAndWriteBcfToFolder(
    IBcf bcf,
    string target,
    bool delete = true,
    CancellationToken? cancellationToken = null) {
    var targetFolder = Path.GetDirectoryName(target);
    if (targetFolder == null)
      throw new ApplicationException(
        $"Target folder not found ${targetFolder}");

    // Creating a tmp folder for the intermediate files.
    var tmpFolder =
      $"{targetFolder}/tmp{Path.GetFileNameWithoutExtension(target)}";
    if (Directory.Exists(tmpFolder)) Directory.Delete(tmpFolder, true);
    Directory.CreateDirectory(tmpFolder);

    var bcfObject = (Bcf)bcf;

    var writeTasks = new List<Task> {
      BcfExtensions.SerializeAndWriteXmlFile(
        tmpFolder,
        "bcf.version",
        new Version())
    };

    // Writing markup files to disk, one markup per folder.
    foreach (var markup in bcfObject.Markups) {
      if (cancellationToken is { IsCancellationRequested: true }) {
        return string.Empty;
      }

      var guid = markup.GetTopic()?.Guid;
      if (guid == null) {
        Log.Debug(
          " - Topic Guid is missing, skipping markup");
        continue;
      }

      var topicFolder = $"{tmpFolder}/{guid}";
      Directory.CreateDirectory(topicFolder);

      writeTasks.Add(BcfExtensions.SerializeAndWriteXmlFile(
        topicFolder,
        "markup.bcf",
        markup));

      foreach (var viewpoint in markup.Viewpoints) {
        writeTasks.Add(BcfExtensions.SerializeAndWriteXmlFile(
          topicFolder,
          viewpoint.Viewpoint,
          viewpoint.VisualizationInfo));

        var snapshotFileName = viewpoint.Snapshot;
        var snapshotBase64String = viewpoint.SnapshotData?.Data;
        if (string.IsNullOrEmpty(snapshotFileName) || snapshotBase64String == null)
          continue;
        writeTasks.Add(File.WriteAllBytesAsync(
          $"{topicFolder}/{snapshotFileName}",
          Convert.FromBase64String(snapshotBase64String)));
      }

      //Additional files can be referenced by other files via their relative
      //paths. It is recommended to put them in a folder called Documents in the
      //root folder of the zip archive.
      var documentFolder = $"{tmpFolder}/documents";
      var internalDocuments = markup
        .Topic.DocumentReference
        .Where(d => !d.IsExternal);
      foreach (var document in internalDocuments) {
        var documentFileName = Path.GetFileName(document.ReferencedDocument);
        var documentBase64String = document.DocumentData.Data;
        if (string.IsNullOrEmpty(documentFileName)) continue;

        if (Directory.Exists(documentFolder) is not true)
          Directory.CreateDirectory(documentFolder);

        writeTasks.Add(File.WriteAllBytesAsync(
          $"{documentFolder}/{documentFileName}",
          Convert.FromBase64String(documentBase64String)));
      }
    }

    writeTasks.Add(
      BcfExtensions.SerializeAndWriteXmlFile(
        tmpFolder,
        "project.bcfp",
        bcfObject.Project));

    await Task.WhenAll(writeTasks);

    Log.Debug($"Zipping the output: {target}");
    if (File.Exists(target)) File.Delete(target);
    ZipFile.CreateFromDirectory(tmpFolder, target);

    if (delete)
      Directory.Delete(tmpFolder, true);

    return tmpFolder;
  }
}