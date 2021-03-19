using System;
using System.Collections.Concurrent;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using bcf_converter.Model;

namespace bcf_converter.Parser.Xml20 {
  /// <summary>
  ///   The `Xml20` parser unzips and parses BCF zips of version 2.0 and
  ///   puts their contents into the Topic model.
  /// </summary>
  public class Xml20 : BCFParser {
    /// <summary>
    ///   The method unzips the bcfzip at the specified path into the memory,
    ///   and parses the xml files within to create an in memory representation
    ///   of the data.
    /// </summary>
    /// <param name="path">The path to the bcfzip.</param>
    /// <returns>Returns a Task with a List of Markup models.</returns>
    public Task<ConcurrentBag<Markup>> parse(string path) {
      return Task.Run(async () => {
        Console.WriteLine("'\nProcessing bcfzip at {0} \n", path);

        // A thread-safe storage for the parsed topics.
        var markups = new ConcurrentBag<Markup>();

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
        string currentUuid = "";
        Header? header = null;
        Topic? topic = null;
        Viewpoint? viewpoint = null;
        Snapshot? snapshot = null;

        for (int i = 0; i < archive.Entries.Count; i++) {
          var entry = archive.Entries[i];
          var isFirstEntry = (i == 0);
          var isLastEntry = (i == archive.Entries.Count - 1);

          Console.WriteLine(entry.FullName);

          // This sets the folder context
          var uuid = entry.FullName.Split("/")[0];

          if (isFirstEntry) {
            currentUuid = uuid;
          }

          if ((currentUuid != "" && uuid != currentUuid) || isLastEntry) {
            // This is a new subfolder, writing out Markup.
            if (topic.HasValue && header.HasValue) {
              var viewpoints = new Viewpoints(viewpoint, snapshot);
              markups.Add(new Markup(header.Value, topic.Value, viewpoints));

              // Null-ing external references
              header = null;
              topic = null;
              viewpoint = null;
              snapshot = null;
              currentUuid = uuid;
            }
            else {
              throw new InvalidDataException(
                "Header or Topic not found in BCF " + currentUuid);
            }
          }

          // Parsing BCF files
          if (entry.isBcf()) {
            var document = await XDocument.LoadAsync(
              entry.Open(),
              LoadOptions.None,
              CancellationToken.None);
            header = document.header();
            topic = document.topic();
          }

          // Parsing the viewpoint file
          else if (entry.isBcfViewpoint()) {
            if (viewpoint.HasValue) {
              // TODO: No support for multiple viewpoints!
              Console.WriteLine("No support for multiple viewpoints!");
              continue;
            }

            var document = await XDocument.LoadAsync(
              entry.Open(),
              LoadOptions.None,
              CancellationToken.None);
            viewpoint = document.viewpoint();
          }

          // Parsing the
          else if (entry.isSnapshot()) {
            if (snapshot.HasValue) {
              // TODO: No support for multiple snapshots!
              Console.WriteLine("No support for multiple snapshots!");
              continue;
            }

            snapshot = entry.snapshot();
          }
        }

        // // The last item in the 
        // if (topic.HasValue && header.HasValue) {
        //   var viewpoints = new Viewpoints(viewpoint, snapshot);
        //   markups.Add(new Markup(header.Value, topic.Value, viewpoints));
        //
        //   // Null-ing external references
        //   header = null;
        //   topic = null;
        //   viewpoint = null;
        //   snapshot = null;
        // }

        return markups;
      });
    }
  }
}