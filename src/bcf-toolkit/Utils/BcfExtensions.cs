using System;
using System.Collections.Concurrent;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using BcfToolkit.Model;

namespace BcfToolkit.Utils;

/// <summary>
///   The `BcfExtensions` static class unzips and parses BCF zips and
///   puts their contents into the BCF models. It also writes the in
///   memory BCF models into BCFzip.
/// </summary>
public static class BcfExtensions {
  /// <summary>
  ///   The method unzips the BCFzip from a stream,
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
  ///
  ///   Notification: This function adjusts the stream position back to 0 in order to use it again.
  /// </summary>
  /// <param name="stream">The source stream of the BCFzip.</param>
  /// <returns>Returns a Task with a List of `Markup` models.</returns>
  public static async Task<ConcurrentBag<TMarkup>> ParseMarkups<TMarkup,
    TVisualizationInfo>(Stream stream)
    where TMarkup : IMarkup
    where TVisualizationInfo : IVisualizationInfo {

    if (stream == null || !stream.CanRead)
      throw new ArgumentException("Source stream is not readable.");

    var objType = typeof(TMarkup);
    Console.WriteLine($"\nProcessing {objType.Name}\n");

    // A thread-safe storage for the parsed topics.
    var markups = new ConcurrentBag<TMarkup>();

    // Unzipping the bcfzip
    using var archive = new ZipArchive(stream, ZipArchiveMode.Read, true);

    // This iterates through the archive file-by-file and the sub-folders
    // being just in the names of the entries.
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
        Regex.IsMatch((string)entry.FullName.Split("/")[0].Replace("-", ""),
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

    // Stream must be positioned back to 0 in order to use it again
    stream.Position = 0;
    return markups;
  }

  private static void WritingOutMarkup<TMarkup, TVisualizationInfo>(
    ref TMarkup? markup,
    ref TVisualizationInfo? viewpoint,
    ref string? snapshot,
    string currentUuid,
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
  ///   The method unzips the BCFzip from a file stream,
  ///   and parses the `extensions.xml` file within to create an in memory
  ///   representation of the data.
  ///   This is a required in the BCF archive.
  ///   HISTORY: New file in BCF 3.0.
  ///   An XML file defining the extensions of a project.
  /// </summary>
  /// <param name="stream">The file stream of the BCFzip.</param>
  /// <returns>Returns a Task with an `Extensions` model.</returns>
  public static Task<TExtensions> ParseExtensions<TExtensions>(Stream stream) {
    return ParseRequired<TExtensions>(stream, entry => entry.IsExtensions());
  }

  /// <summary>
  ///   The method unzips the BCFzip from a file stream,
  ///   and parses the `project.bcfp` file within to create an in memory
  ///   representation of the data.
  ///   This is an optional file in the BCF archive.
  ///   HISTORY: From BCF 2.0.
  ///   The project file contains reference information about the project
  ///   the topics belong to.
  /// </summary>
  /// <param name="stream">The stream of the BCFzip.</param>
  /// <returns>Returns a Task with an `ProjectInfo` model.</returns>
  public static Task<TProjectInfo?> ParseProject<TProjectInfo>(Stream stream) {
    return ParseOptional<TProjectInfo>(stream, entry => entry.IsProject());
  }

  /// <summary>
  ///   The method unzips the BCFzip from a file stream,
  ///   and parses the `documents.xml` file within to create an in memory
  ///   representation of the data.
  ///   An XML file defining the documents in a project.
  ///   This is an optional file in the BCF archive.
  ///   HISTORY: New file in BCF 3.0.
  /// </summary>
  /// <param name="stream">The stream of to the BCFzip.</param>
  /// <returns>Returns a Task with an `DocumentInfo` model.</returns>
  public static Task<TDocumentInfo?>
    ParseDocuments<TDocumentInfo>(Stream stream) {
    return ParseOptional<TDocumentInfo>(stream, entry => entry.IsDocuments());
  }

  private static Task<T> ParseRequired<T>(
    Stream stream,
    Func<ZipArchiveEntry, bool> filterFn) {
    var obj = ParseObject<T>(stream, filterFn, true);
    if (obj == null)
      throw new InvalidDataException($"{typeof(T)} is not found in BCF.");
    return obj!;
  }

  private static Task<T?> ParseOptional<T>(
    Stream stream,
    Func<ZipArchiveEntry, bool> filterFn) {
    return ParseObject<T>(stream, filterFn);
  }

  /// <summary>
  ///   This method reads a BCFzip from the specified stream into memory,
  ///   filters and searches for the desired file using the provided filter function,
  ///   and parses it into the specified object type. If the file is marked as required,
  ///   an exception is thrown if it is missing.
  ///
  ///   Notification: This function adjusts the stream position back to 0 in order to use it again.
  /// </summary>
  /// <param name="stream">The stream containing the BCFzip data.</param>
  /// <param name="filterFn">The filter function used to identify the desired file.</param>
  /// <param name="isRequired">Specifies whether the file is required or optional.</param>
  /// <typeparam name="T">The generic type parameter representing the desired object type.</typeparam>
  /// <returns>Returns a Task with a model of type `T`.</returns>
  /// <exception cref="InvalidDataException">Thrown if the file is marked as required but is missing.</exception>
  private static async Task<T?> ParseObject<T>(
    Stream stream,
    Func<ZipArchiveEntry, bool> filterFn,
    bool isRequired = false) {

    if (stream == null || !stream.CanRead)
      throw new ArgumentException("Source stream is not readable.");

    var objType = typeof(T);
    Console.WriteLine($"\nProcessing {objType.Name}\n");

    var obj = default(T);

    // Unzipping the bcfzip
    using var archive = new ZipArchive(stream, ZipArchiveMode.Read, true);

    foreach (var entry in archive.Entries) {
      if (!filterFn(entry)) continue;

      Console.WriteLine(entry.FullName);

      var document = await XDocument.LoadAsync(
        entry.Open(),
        LoadOptions.None,
        CancellationToken.None);
      obj = document.BcfObject<T>();
    }

    // Stream must be positioned back to 0 in order to use it again
    stream.Position = 0;
    return obj;
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