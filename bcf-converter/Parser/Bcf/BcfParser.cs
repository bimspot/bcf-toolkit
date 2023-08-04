using System;
using System.Collections.Concurrent;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace bcf.Parser;

/// <summary>
///   The `BcfParser` parser unzips and parses BCF zips and
///   puts their contents into the Topic model.
/// </summary>
public class BcfParser {
  /// <summary>
  ///   The method unzips the BCFzip at the specified path into the memory,
  ///   and parses the markup xml files within to create an in memory
  ///   representation of the data.
  ///
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
  public static Task<ConcurrentBag<TMarkup>> ParseMarkups<TMarkup, TVisualizationInfo>(string path)
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

      for (var i = 0; i < archive.Entries.Count; i++) {
        var entry = archive.Entries[i];
        var isLastEntry = i == archive.Entries.Count - 1;

        Console.WriteLine(entry.FullName);

        // This sets the folder context
        var uuid = entry.FullName.Split("/")[0];
        var isTopic = Regex.IsMatch(uuid.Replace("-", ""), "^[a-fA-F0-9]+$");

        if (isTopic) {
          currentUuid = uuid;
        }

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
          if (viewpoint != null) {
            // TODO: No support for multiple viewpoints!
            Console.WriteLine("No support for multiple viewpoints!");
            continue;
          }

          var document = await XDocument.LoadAsync(
            entry.Open(),
            LoadOptions.None,
            CancellationToken.None);
          viewpoint = document.BcfObject<TVisualizationInfo>();
        }

        // Parsing the snapshot
        else if (entry.IsSnapshot()) {
          if (snapshot != null) {
            // TODO: No support for multiple snapshots!
            Console.WriteLine("No support for multiple snapshots!");
            continue;
          }

          snapshot = entry.Snapshot();
        }

        if ((currentUuid == "" || uuid == currentUuid) && !isLastEntry) continue;
        // This is a new subfolder, writing out Markup.
        if (markup != null) {
          var firstViewPoint = markup.GetFirstViewPoint();

          if (firstViewPoint != null) {
            firstViewPoint.VisualizationInfo = viewpoint;
            firstViewPoint.SnapshotData = snapshot;
          }

          markups.Add(markup);

          // Null-ing external references
          markup = default;
          viewpoint = default;
          snapshot = null;
          currentUuid = uuid;
        }
        else {
          throw new InvalidDataException(
            "Markup not found in BCF " + currentUuid);
        }
      }

      return markups;
    });
  }

  /// <summary>
  ///   The method unzips the BCFzip at the specified path into the memory,
  ///   and parses the `extensions.xml` file within to create an in memory
  ///   representation of the data.
  ///   This is a required in the BCF archive.
  ///
  ///   HISTORY: New file in BCF 3.0.
  /// 
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
  ///
  ///   HISTORY: From BCF 2.0.
  ///
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
  ///
  ///   HISTORY: New file in BCF 3.0.
  /// </summary>
  /// <param name="path">The path to the BCFzip.</param>
  /// <returns>Returns a Task with an `DocumentInfo` model.</returns>
  public static Task<TDocumentInfo?> ParseDocuments<TDocumentInfo>(string path) {
    return ParseOptional<TDocumentInfo>(path, entry => entry.IsDocuments());
  }
  
  private static Task<T> ParseRequired<T>(string path, Func<ZipArchiveEntry, bool> filterFn) {
    return ParseObject<T>(path, filterFn, true)!;
  }
  private static Task<T?> ParseOptional<T>(string path, Func<ZipArchiveEntry, bool> filterFn) {
    return ParseObject<T>(path, filterFn);
  }
  
  /// <summary>
  ///   The method unzips the BCFzip at the specified path into the memory,
  ///   and looks for the desired file by the filter then parses to given
  ///   object type. If the file is required it throws an exception if it
  ///   is missing.
  ///   
  /// </summary>
  /// <param name="path">The path to the BCFzip.</param>
  /// <param name="filterFn">The filter function which is used for filter the given file.</param>
  /// <param name="isRequired">The file is required or not.</param>
  /// <typeparam name="T">Generic type parameter, to which we want to parse.</typeparam>
  /// <returns>Returns a Task with an `T` model.</returns>
  /// <exception cref="InvalidDataException">In case the file is required, but missing.</exception>
  private static Task<T?> ParseObject<T>(string path, Func<ZipArchiveEntry, bool> filterFn, bool isRequired=false) {
    return Task.Run(async () => {

      var objType = typeof(T);
      Console.WriteLine($"\nProcessing {objType.Name}\n", path);

      var obj = default(T);
      
      // Unzipping the bcfzip
      using var archive = ZipFile.OpenRead(path);

      foreach (var entry in archive.Entries)
      {
        if(!filterFn(entry)) continue;
        
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
}