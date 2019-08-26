using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using bcf2json.Model;

namespace bcf2json.Parser {
  public class Xml20 : BCFParser {
    public Task<List<Topic>> jsonFrom(string path) {
      return Task.Run(async () => {
        Console.WriteLine("Processing at {0}", path);


        var topics = new ConcurrentBag<Topic>();

        // Unzipping the bcfzip
        using (var archive = ZipFile.OpenRead(path)) {
          // Iterating through the files
          foreach (var entry in archive.Entries) {
//            Console.WriteLine("  {0}", entry);
            // TODO: read bcf.version and decide on the parser version

            // Parsing BCF files
            if (entry.FullName.EndsWith(".bcf",
              StringComparison.OrdinalIgnoreCase)) {
              var folder = entry.FullName.Split("/")[0];

              // Loading the XML into memory
              var document = await XDocument.LoadAsync(
                entry.Open(),
                LoadOptions.None,
                CancellationToken.None);

              var topic = (from item in document.Descendants("Topic")
                select new Topic() {
                  guid = item.Attribute("Guid")?.Value,
                  type = item.Attribute("TopicType")?.Value,
                  status = item.Attribute("TopicStatus")?.Value,
                  title = item.Element("Title")?.Value,
                  priority = item.Element("Priority")?.Value,
                  index = item.Element("Index")?.Value,
                  labels = item.Element("Labels")?.Value,
                  creationDate = item.Element("CreationDate")?.Value,
                  creationAuthor = item.Element("CreationAuthor")?.Value,
                  modifiedDate = item.Element("ModifiedDate")?.Value,
                  modifiedAuthor = item.Element("ModifiedAuthor")?.Value,
                  dueDate = item.Element("DueDate")?.Value,
                  assignedTo = item.Element("AssignedTo")?.Value,
                  description = item.Element("Description")?.Value
                }).Single();
              topics.Add(topic);
            }
          }
          Console.WriteLine(topics);
        }
        
        // Loop through folders
        // Load markup.bcf
        // Parse topic information:
        //   Guid, TopicType, TopicStatus
        //   Title, CreationDate, CreationAuthor, Description

        // Parse viewpoints
        //  load referenced snapshot into Base64
        // snapshot_type, snapshot_data
        //  parse referenced viewpoint.bcfv 2.0

        // Components
        // Collect all visible ("selection")
        // ifc_guid, originating_system, authoring_tool_id

        // Collect all selected ("visibility")
        // default_visibility, exceptions, view_setup_hints

        // Collect all colours ("coloring")
        // color, components
        // PerspectiveCamera

        return new List<Topic>();
      });
    }
  }
}