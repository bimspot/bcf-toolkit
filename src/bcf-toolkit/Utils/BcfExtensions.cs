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
using BcfToolkit.Model;
using BcfToolkit.Model.Interfaces;

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
  ///   Notification: This function adjusts the stream position back to 0 in order to use it again.
  /// </summary>
  /// <param name="stream">The source stream of the BCFzip.</param>
  /// <returns>Returns a Task with a List of `Markup` models.</returns>
  public static async Task<ConcurrentBag<TMarkup>> ParseMarkups<
    TMarkup,
    TVisualizationInfo>(Stream stream)
    where TMarkup : IMarkup
    where TVisualizationInfo : IVisualizationInfo {
    if (stream == null || !stream.CanRead)
      throw new ArgumentException("Source stream is not readable.");

    var objType = typeof(TMarkup);
    Log.Debug($"\nProcessing {objType.Name}\n");

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
    Dictionary<string, TVisualizationInfo>? visInfos = null;
    Dictionary<string, FileData>? snapshots = null;

    var topicEntries = archive
      .Entries
      .OrderBy(entry => entry.FullName)
      .Where(entry =>
        Regex.IsMatch(entry.FullName.Split("/")[0].Replace("-", ""),
          "^[a-fA-F0-9]+$"))
      .ToList();

    foreach (var entry in topicEntries) {
      var isLastTopicEntry = entry == topicEntries.Last();

      Log.Debug(entry.FullName);

      // This sets the folder context
      var uuid = entry.FullName.Split("/")[0];
      //var isTopic = Regex.IsMatch(uuid.Replace("-", ""), "^[a-fA-F0-9]+$");

      var isNewTopic =
        !string.IsNullOrEmpty(currentUuid) && uuid != currentUuid;

      if (isNewTopic)
        WritingOutMarkup(
          ref markup,
          ref visInfos,
          ref snapshots,
          currentUuid,
          markups);

      currentUuid = uuid;

      // Parsing markup files
      if (entry.IsBcf()) {
        var document = await XDocument.LoadAsync(
          entry.Open(),
          LoadOptions.None,
          CancellationToken.None);
        markup = document.BcfObject<TMarkup>();
      }

      // Parsing the viewpoint file
      else if (entry.IsBcfViewpoint()) {
        visInfos ??= new Dictionary<string, TVisualizationInfo>();
        var document = await XDocument.LoadAsync(
          entry.Open(),
          LoadOptions.None,
          CancellationToken.None);
        visInfos.Add(entry.Name, document.BcfObject<TVisualizationInfo>());
      }

      // Parsing the snapshot
      else if (entry.IsSnapshot()) {
        snapshots ??= new Dictionary<string, FileData>();
        var snapshot = entry.FileData();
        snapshots.Add(snapshot.Key, snapshot.Value);
      }

      if (isLastTopicEntry)
        WritingOutMarkup(
          ref markup,
          ref visInfos,
          ref snapshots,
          currentUuid,
          markups);
    }

    // Stream must be positioned back to 0 in order to use it again
    stream.Position = 0;
    return markups;
  }

  private static void WritingOutMarkup<TMarkup, TVisualizationInfo>(
    ref TMarkup? markup,
    ref Dictionary<string, TVisualizationInfo>? visInfos,
    ref Dictionary<string, FileData>? snapshots,
    string currentUuid,
    ConcurrentBag<TMarkup> markups)
    where TMarkup : IMarkup
    where TVisualizationInfo : IVisualizationInfo {
    // This is a new subfolder, writing out Markup.
    if (markup != null) {
      markup.SetViewPoints(visInfos, snapshots);
      markups.Add(markup);

      // Null-ing external references
      markup = default;
      visInfos = null;
      snapshots = null;
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
    return ParseOptional<TProjectInfo>(stream, entry => entry.IsBcfProject());
  }

  /// <summary>
  ///   The method unzips the BCFzip from a file stream,
  ///   and parses the `documents.xml` file within to create an in memory
  ///   representation of the data.
  ///   It is possible to store additional files in the BCF container as
  ///   documents. The documents must be located in a folder called Documents in
  ///   the root directory, and must be referenced by the documents.xml file.
  ///   For uniqueness, the filename of a document in the BCF must be the
  ///   document guid. The actual filename is stored in the documents.xml.
  ///   
  ///   The `documents.xml` and documents folder are optional in the BCF archive.
  ///
  ///   HISTORY: New in BCF 3.0.
  /// </summary>
  /// <param name="stream">The stream of to the BCFzip.</param>
  /// <returns>Returns a Task with an `DocumentInfo` model.</returns>
  public static async Task<TDocumentInfo?>
    ParseDocuments<TDocumentInfo>(Stream stream)
    where TDocumentInfo : IDocumentInfo {
    if (stream is null || !stream.CanRead)
      throw new ArgumentException("Source stream is not readable.");

    var objType = typeof(TDocumentInfo);
    Log.Debug($"\nProcessing {objType.Name}\n");

    var documentInfo = default(TDocumentInfo);
    Dictionary<string, string>? documents = null;

    using var archive = new ZipArchive(stream, ZipArchiveMode.Read, true);

    // Without the document info file (documents.xml) further operations are
    // meaningless
    var documentInfoEntry = archive
      .Entries
      .FirstOrDefault(entry => entry.IsDocuments());
    if (documentInfoEntry is null) return documentInfo;

    var document = await XDocument.LoadAsync(
      documentInfoEntry.Open(),
      LoadOptions.None,
      CancellationToken.None);
    documentInfo = document.BcfObject<TDocumentInfo>();

    var documentEntries = archive
      .Entries
      .OrderBy(entry => entry.FullName)
      .Where(entry => entry.IsDocumentsFolder())
      .ToList();

    foreach (var entry in documentEntries) {
      Log.Debug(entry.FullName);
      documentInfo.SetDocumentData(entry);
    }

    // Stream must be positioned back to 0 in order to use it again
    stream.Position = 0;
    return documentInfo;
  }

  private static Task<T> ParseRequired<T>(
    Stream stream,
    Func<ZipArchiveEntry, bool> filterFn) {
    var obj = ParseObject<T>(stream, filterFn);
    if (obj is null)
      throw new InvalidDataException($"{typeof(T)} is not found in BCF.");
    return obj;
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
  ///   Notification: This function adjusts the stream position back to 0 in order to use it again.
  /// </summary>
  /// <param name="stream">The stream containing the BCFzip data.</param>
  /// <param name="filterFn">The filter function used to identify the desired file.</param>
  /// <typeparam name="T">The generic type parameter representing the desired object type.</typeparam>
  /// <returns>Returns a Task with a model of type `T`.</returns>
  private static async Task<T?> ParseObject<T>(
    Stream stream,
    Func<ZipArchiveEntry, bool> filterFn) {
    if (stream is null || !stream.CanRead)
      throw new ArgumentException("Source stream is not readable.");

    var objType = typeof(T);
    Log.Debug($"\nProcessing {objType.Name}\n");

    var obj = default(T);

    // Unzipping the bcfzip
    using var archive = new ZipArchive(stream, ZipArchiveMode.Read, true);

    foreach (var entry in archive.Entries) {
      if (!filterFn(entry)) continue;

      Log.Debug(entry.FullName);

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
  ///   The method serializes and writes the specified type object into a file.
  /// </summary>
  /// <param name="folder">The target folder where the file is written in.</param>
  /// <param name="file">The target file name.</param>
  /// <param name="obj">The object which will be written.</param>
  /// <typeparam name="T">Generic type parameter.</typeparam>
  /// <returns></returns>
  public static Task SerializeAndWriteXmlFile<T>(string folder, string file,
    T? obj) {
    return Task.Run(async () => {
      if (obj != null) {
        await using var writer = File.CreateText($"{folder}/{file}");
        new XmlSerializer(typeof(T)).Serialize(writer, obj);
      }
    });
  }

  /// <summary>
  ///   The method opens the archive stream and returns the BCF version.
  /// </summary>
  /// <param name="stream">
  ///   TODO: needs seekable stream
  /// </param>
  /// <exception cref="ArgumentException">
  ///   Throws an exception if the provided stream is not seekable.
  /// </exception>
  /// <returns>Returns the BcfVersionEnum enum.</returns>
  public static async Task<BcfVersionEnum?> GetVersionFromStreamArchive(
    Stream stream) {

    if (!stream.CanRead || !stream.CanSeek) {
      throw new ArgumentException("Stream is not Readable or Seekable");
    }

    using var archive = new ZipArchive(stream, ZipArchiveMode.Read, true);
    BcfVersionEnum? version = null;

    foreach (var entry in archive.Entries) {
      if (!entry.IsVersion()) continue;

      Log.Debug(entry.FullName);

      var document = await XDocument.LoadAsync(
        entry.Open(),
        LoadOptions.None,
        CancellationToken.None);
      version =
        BcfVersion.TryParse(document.Root?.Attribute("VersionId")?.Value);
      stream.Position = 0;
      return version;
    }

    return version;
  }
}