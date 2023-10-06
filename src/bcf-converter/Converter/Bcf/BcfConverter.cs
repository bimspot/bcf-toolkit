using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using BcfConverter.Model;

namespace BcfConverter.Converter;

/// <summary>
///   The `BcfConverter` static class unzips and parses BCF zips and
///   puts their contents into the BCF models. It also writes the in
///   memory BCF models into BCFzip.
/// </summary>
public static class BcfConverter {
  /// <summary>
  ///   The method unzips the BCFzip at the specified path into the memory,
  ///   and parses the markup xml files within to create an in memory
  ///   representation of the data.
  ///   Topic folder structure inside a BCFzip archive:
  ///   The folder name is the GUID of the topic. This GUID is in the UUID form.
  ///   The GUID must be all-lowercase. The folder contains the following file:
  ///   * markup.bcf
  ///   Additionally the folder can contain other files:
  ///   * Viewpoint files
  ///   * Snapshot files
  ///   * Bitmaps
  /// </summary>
  /// <param name="path">The path to the BCFzip.</param>
  /// <returns>Returns a Task with a List of `Markup` models.</returns>
  public static Task<ConcurrentBag<TMarkup>> ParseMarkups<TMarkup,
    TVisualizationInfo>(string path)
    where TMarkup : IMarkup
    where TVisualizationInfo : IVisualizationInfo {
    return Task.Run(async () => {
      Console.WriteLine("'\nProcessing markups at {0} \n", path);

      // A thread-safe storage for the parsed topics.
      var markups = new ConcurrentBag<TMarkup>();

      // Unzipping the bcfzip
      using var archive = ZipFile.OpenRead(path);

      // This iterates through the archive file-by-file and the sub-folders
      // being just in the names of the entries.
      //
      // We know it is a new Markup, when the folder (uuid) changes. In that
      // case the Markup object is created and pushed into the bac. A special
      // case is the last entry in the archive, when that is reached, the
      // markup is created as well.

      // The BCF data is collected from several files, therefore references
      // are kept here for the currently processes ones.
      var currentUuid = "";
      var markup = default(TMarkup);
      var viewpoint = default(TVisualizationInfo);
      string? snapshot = null;

      var topicEntries = archive.Entries
        .Where(entry =>
          Regex.IsMatch(entry.FullName.Split("/")[0].Replace("-", ""),
            "^[a-fA-F0-9]+$"))
        .ToList();

      foreach (var entry in topicEntries) {
        var isLastTopicEntry = entry == topicEntries.Last();

        Console.WriteLine(entry.FullName);

        // This sets the folder context
        var uuid = entry.FullName.Split("/")[0];
        //var isTopic = Regex.IsMatch(uuid.Replace("-", ""), "^[a-fA-F0-9]+$");

        var isNewTopic =
          !string.IsNullOrEmpty(currentUuid) && uuid != currentUuid;

        if (isNewTopic)
          WritingOutMarkup(ref markup, ref viewpoint, ref snapshot,
            currentUuid, ref markups);

        currentUuid = uuid;

        // Parsing BCF files
        if (entry.IsBcf()) {
          var document = await XDocument.LoadAsync(
            entry.Open(),
            LoadOptions.None,
            CancellationToken.None);
          markup = document.BcfObject<TMarkup>();
        }

        // Parsing the viewpoint file
        else if (entry.IsBcfViewpoint()) {
          if (viewpoint != null)
            // TODO: No support for multiple viewpoints!
            Console.WriteLine("No support for multiple viewpoints!");
          //continue;
          var document = await XDocument.LoadAsync(
            entry.Open(),
            LoadOptions.None,
            CancellationToken.None);
          viewpoint = document.BcfObject<TVisualizationInfo>();
        }

        // Parsing the snapshot
        else if (entry.IsSnapshot()) {
          if (snapshot != null)
            // TODO: No support for multiple snapshots!
            Console.WriteLine("No support for multiple snapshots!");
          //continue;
          snapshot = entry.Snapshot();
        }

        if (isLastTopicEntry)
          WritingOutMarkup(ref markup, ref viewpoint, ref snapshot,
            currentUuid, ref markups);
      }

      return markups;
    });
  }

  private static void WritingOutMarkup<TMarkup, TVisualizationInfo>(
    ref TMarkup? markup, ref TVisualizationInfo? viewpoint,
    ref string? snapshot, string currentUuid,
    ref ConcurrentBag<TMarkup> markups)
    where TMarkup : IMarkup
    where TVisualizationInfo : IVisualizationInfo {
    // This is a new subfolder, writing out Markup.
    if (markup != null) {
      var firstViewPoint = markup.GetFirstViewPoint();

      if (firstViewPoint != null) {
        firstViewPoint.SetVisualizationInfo(viewpoint);
        firstViewPoint.SnapshotData = snapshot;
      }

      markups.Add(markup);

      // Null-ing external references
      markup = default;
      viewpoint = default;
      snapshot = null;
    }
    else {
      throw new InvalidDataException(
        "Markup not found in BCF " + currentUuid);
    }
  }

  /// <summary>
  ///   The method unzips the BCFzip at the specified path into the memory,
  ///   and parses the `extensions.xml` file within to create an in memory
  ///   representation of the data.
  ///   This is a required in the BCF archive.
  ///   HISTORY: New file in BCF 3.0.
  ///   An XML file defining the extensions of a project.
  /// </summary>
  /// <param name="path">The path to the BCFzip.</param>
  /// <returns>Returns a Task with an `Extensions` model.</returns>
  public static Task<TExtensions> ParseExtensions<TExtensions>(string path) {
    return ParseRequired<TExtensions>(path, entry => entry.IsExtensions());
  }

  /// <summary>
  ///   The method unzips the BCFzip at the specified path into the memory,
  ///   and parses the `project.bcfp` file within to create an in memory
  ///   representation of the data.
  ///   This is an optional file in the BCF archive.
  ///   HISTORY: From BCF 2.0.
  ///   The project file contains reference information about the project
  ///   the topics belong to.
  /// </summary>
  /// <param name="path">The path to the BCFzip.</param>
  /// <returns>Returns a Task with an `ProjectInfo` model.</returns>
  public static Task<TProjectInfo?> ParseProject<TProjectInfo>(string path) {
    return ParseOptional<TProjectInfo>(path, entry => entry.IsProject());
  }

  /// <summary>
  ///   The method unzips the BCFzip at the specified path into the memory,
  ///   and parses the `documents.xml` file within to create an in memory
  ///   representation of the data.
  ///   An XML file defining the documents in a project.
  ///   This is an optional file in the BCF archive.
  ///   HISTORY: New file in BCF 3.0.
  /// </summary>
  /// <param name="path">The path to the BCFzip.</param>
  /// <returns>Returns a Task with an `DocumentInfo` model.</returns>
  public static Task<TDocumentInfo?>
    ParseDocuments<TDocumentInfo>(string path) {
    return ParseOptional<TDocumentInfo>(path, entry => entry.IsDocuments());
  }

  private static Task<T> ParseRequired<T>(string path,
    Func<ZipArchiveEntry, bool> filterFn) {
    return ParseObject<T>(path, filterFn, true)!;
  }

  private static Task<T?> ParseOptional<T>(string path,
    Func<ZipArchiveEntry, bool> filterFn) {
    return ParseObject<T>(path, filterFn);
  }

  /// <summary>
  ///   The method unzips the BCFzip at the specified path into the memory,
  ///   and looks for the desired file by the filter then parses to given
  ///   object type. If the file is required it throws an exception if it
  ///   is missing.
  /// </summary>
  /// <param name="path">The path to the BCFzip.</param>
  /// <param name="filterFn">The filter function which is used for filter the given file.</param>
  /// <param name="isRequired">The file is required or not.</param>
  /// <typeparam name="T">Generic type parameter, to which we want to parse.</typeparam>
  /// <returns>Returns a Task with an `T` model.</returns>
  /// <exception cref="InvalidDataException">In case the file is required, but missing.</exception>
  private static Task<T?> ParseObject<T>(string path,
    Func<ZipArchiveEntry, bool> filterFn, bool isRequired = false) {
    return Task.Run(async () => {
      if (string.IsNullOrEmpty(path) || !File.Exists(path))
        throw new ArgumentException("Source file is not existing.");

      var objType = typeof(T);
      Console.WriteLine($"\nProcessing {objType.Name}\n", path);

      var obj = default(T);

      // Unzipping the bcfzip
      using var archive = ZipFile.OpenRead(path);

      foreach (var entry in archive.Entries) {
        if (!filterFn(entry)) continue;

        Console.WriteLine(entry.FullName);

        var document = await XDocument.LoadAsync(
          entry.Open(),
          LoadOptions.None,
          CancellationToken.None);
        obj = document.BcfObject<T>();
      }

      if (isRequired && obj == null)
        throw new InvalidDataException($"{objType} is not found in BCF.");
      return obj;
    });
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
  /// <param name="targetFile">The target file name of the BCFzip.</param>
  /// <param name="markups">Array of `Markup` objects.</param>
  /// <param name="root">The `Root` object of the BCF, it contains all the root info.</param>
  /// <typeparam name="TMarkup">`Markup` type parameter.</typeparam>
  /// /// <typeparam name="TVisualizationInfo">`VisualizationInfo` type parameter.</typeparam>
  /// <typeparam name="TRoot">`Root` type parameter.</typeparam>
  /// <typeparam name="TVersion">`Version` type parameter.</typeparam>
  /// <returns></returns>
  /// <exception cref="ApplicationException"></exception>
  public static Task WriteBcf<TMarkup, TVisualizationInfo, TRoot, TVersion>(string targetFile,
    ConcurrentBag<TMarkup> markups, TRoot root)
    where TMarkup : IMarkup
    where TVisualizationInfo : IVisualizationInfo
    where TRoot : IRoot
    where TVersion : new() {
    return Task.Run(async () => {
      var targetFolder = Path.GetDirectoryName(targetFile);
      if (targetFolder == null)
        throw new ApplicationException(
          $"Target folder not found ${targetFolder}");

      // Will create a tmp folder for the intermediate files.
      var tmpFolder = $"{targetFolder}/tmp";
      if (Directory.Exists(tmpFolder)) Directory.Delete(tmpFolder, true);
      Directory.CreateDirectory(tmpFolder);

      var tasks = new List<Task>();

      // Creating the version file
      var version = new TVersion();
      tasks.Add(WriteBcfFile(tmpFolder, "bcf.version", version));

      // Writing markup folders and files
      foreach (var markup in markups) {
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
        tasks.Add(WriteBcfFile(topicFolder, "markup.bcf", markup));

        // Viewpoint
        var visInfo =
          (TVisualizationInfo)markup.GetFirstViewPoint()?.GetVisualizationInfo()!;
        tasks.Add(WriteBcfFile(topicFolder, "viewpoint.bcfv",
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

      // Writing root files
      tasks.Add(root.WriteBcf(tmpFolder));

      // Waiting for all the file writing
      await Task.WhenAll(tasks);

      // zip shit
      Console.WriteLine($"Zipping the output: {targetFile}");
      if (File.Exists(targetFile)) File.Delete(targetFile);
      ZipFile.CreateFromDirectory(tmpFolder, targetFile);
      Directory.Delete(tmpFolder, true);
    });
  }

  /// <summary>
  ///   The method serializes and writes the specified type object into a BCF file.
  /// </summary>
  /// <param name="folder">The target folder.</param>
  /// <param name="file">The target file name.</param>
  /// <param name="obj">The object which will be written.</param>
  /// <typeparam name="T">Generic type parameter.</typeparam>
  /// <returns></returns>
  public static Task WriteBcfFile<T>(string folder, string file, T? obj) {
    return Task.Run(async () => {
      if (obj != null) {
        await using var writer =
          File.CreateText($"{folder}/{file}");
        new XmlSerializer(typeof(T)).Serialize(writer, obj);
      }
    });
  }
}