using System;
using System.Collections.Concurrent;
using System.IO.Compression;
using System.Linq;
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
    ///   and parses the xml files within to create a Topic representation of
    ///   the data.
    /// </summary>
    /// <param name="path">The absolute path to the bcfzip.</param>
    /// <returns>Returns a Task with a List of Topic models.</returns>
    public Task<ConcurrentBag<Topic>> parse(string path) {
      return Task.Run(async () => {
        Console.WriteLine("'\nProcessing bcfzip at {0} \n", path);

        // A thread-safe storage for the parsed topics.
        var topics = new ConcurrentBag<Topic>();

        // Unzipping the bcfzip
        using (var archive = ZipFile.OpenRead(path)) {
          // In order to instantiate the Viewpoints struct in the Topic, 
          // the data from the viewpoint and the snapshot files have to be
          // ready. They are collected there and are null-ed after the Topic
          // is created.
          Viewpoint? viewpoint = null;
          Snapshot? snapshot = null;

          // Iterating through the files
          foreach (var entry in archive.Entries) {
            Console.WriteLine(entry.FullName);

            // TODO: read bcf.version and decide on the parser version

            // This sets the folder context
            var uuid = entry.FullName.Split("/")[0];

            // Parsing BCF files
            if (entry.isBcf()) {
              var document = await XDocument.LoadAsync(
                entry.Open(),
                LoadOptions.None,
                CancellationToken.None);
              var topic = document.topic();
              topics.Add(topic);
            }

            // Parsing the viewpoint file
            else if (entry.isBcfViewpoint()) {
              var document = await XDocument.LoadAsync(
                entry.Open(),
                LoadOptions.None,
                CancellationToken.None);
              viewpoint = document.viewpoint();
            }

            // Parsing the
            else if (entry.isSnapshot()) {
              snapshot = entry.snapshot();
            }

            // Once all: topic, viewpoint and snapshot for the uuid is
            // available, the object can be created and returned
            if (viewpoint.HasValue && snapshot.HasValue) {
              var topic = topics.Single(t => t.guid.Equals(uuid));
              topic.viewpoints.Add(new Viewpoints {
                viewpoint = viewpoint.Value,
                snapshot = snapshot.Value
              });

              // Null-ing external references
              viewpoint = null;
              snapshot = null;
            }
          }
        }

        return topics;
      });
    }
  }
}