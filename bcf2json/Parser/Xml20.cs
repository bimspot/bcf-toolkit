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
  /// <summary>
  ///   The `Xml20` parser unzips and parses BCF zips of version 2.0 and
  ///   serialises their contents into the Topic model.
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
          // In order to instanciate the Viewpoints struct in the Topic, 
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
              var topic = await topicUsing(entry);
              topics.Add(topic);
            }

            // Parsing the viewpoint file
            else if (entry.isBcfViewpoint()) {
              viewpoint = await viewpointUsing(entry);
            }

            // Parsing the
            else if (entry.isSnapshot()) {
              snapshot = await snapshotUsing(entry);
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

    /// <summary>
    ///   Loads the zipped entry as an XML and uses LINQ to extract all
    ///   information from the elements in order to create a Topic instance.
    /// </summary>
    /// <param name="entry">The ZipArchiveEntry containing a bcf file.</param>
    /// <returns>Returns a Task with the future Topic to be created.</returns>
    private Task<Topic> topicUsing(ZipArchiveEntry entry) {
      return Task.Run(async () => {
        var document = await XDocument.LoadAsync(
          entry.Open(),
          LoadOptions.None,
          CancellationToken.None);

        return (from item in document.Descendants("Topic")
          select new Topic {
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
            description = item.Element("Description")?.Value,
            viewpoints = new List<Viewpoints>()
          }).Single();
      });
    }

    /// <summary>
    ///   Loads the zipped entry as an XML and uses LINQ to extract all
    ///   information from the elements in order to create a Viewpoint instance.
    /// </summary>
    /// <param name="entry">The ZipArchiveEntry containing a bcf file.</param>
    /// <returns>
    ///   Returns a Task with the future Viewpoint to be created.
    /// </returns>
    private Task<Viewpoint> viewpointUsing(ZipArchiveEntry entry) {
      return Task.Run(async () => {
        var document = await XDocument.LoadAsync(
          entry.Open(),
          LoadOptions.None,
          CancellationToken.None);

        Func<XElement, string, Vector3> makeVector = (item, element) => {
          return new Vector3 {
            x = Convert.ToSingle(
              item.Element("CameraViewPoint")?.Element("X")?.Value),
            y = Convert.ToSingle(
              item.Element("CameraViewPoint")?.Element("Y")?.Value),
            z = Convert.ToSingle(
              item.Element("CameraViewPoint")?.Element("Z")?.Value)
          };
        };

        var pCam =
          (from item in document.Descendants("PerspectiveCamera")
            select new PerspectiveCamera {
              cameraViewPoint = makeVector(item, "CameraViewPoint"),
              cameraDirection = makeVector(item, "CameraDirection"),
              cameraUpVector = makeVector(item, "CameraUpVector"),
              fieldOfView = Convert.ToSingle(item.Element("FieldOfView")?.Value)
            }).SingleOrDefault();

        var oCam =
          (from item in document.Descendants("OrthogonalCamera")
            select new OrthogonalCamera() {
              cameraViewPoint = makeVector(item, "CameraViewPoint"),
              cameraDirection = makeVector(item, "CameraDirection"),
              cameraUpVector = makeVector(item, "CameraUpVector"),
              viewToWorldScale =
                Convert.ToSingle(item.Element("ViewToWorldScale")?.Value)
            }).FirstOrDefault()

        var selections = document
          .Descendants("Components")
          .Elements("Component")
          .Where(e =>
            bool.Parse(e.Attribute("Selected")?.Value ?? "false").Equals
              (true));

        var colors = document
          .Descendants("Components")
          .Elements("Component")
          .GroupBy(i => i.Attribute("Color")?.Value ?? "FFFFFFFF", (key, result) => {
            return new Coloring {
              color = key,
              components = makeComponentList(result)
            };
          });

        // Note: Optimization Rules

        // If the list of hidden components is smaller than the list of visible
        // components: set default_visibility to true and put the hidden
        // components in exceptions.

        // If the list of visible components is smaller or equals the list of
        // hidden components: set default_visibility to false and put the
        // visible components in exceptions.

        var numberOfVisibleElements = selections.Count();
        var totalElements = document
          .Descendants("Components")
          .Elements("Component")
          .Count();
        var numberOfHiddenElements = totalElements - numberOfVisibleElements;
        var defaultVisibility =
          numberOfHiddenElements < numberOfVisibleElements;

        return new Viewpoint {
          perspectiveCamera = pCam,
          orthogonalCamera = oCam,
          components = new Components {
            selection = makeComponentList(selections),
            coloring = colors.ToList(),
            visibility = new Visibility {
              defaultVisibility = defaultVisibility
            }
          }
        };
      });
    }

    /// <summary>
    ///   A private utility method creating a list of Components using
    ///   the data in the XML.
    /// </summary>
    /// <param name="items">
    ///   A list of XElement's with the component elements in XML.
    /// </param>
    /// <returns></returns>
    private List<Component> makeComponentList(IEnumerable<XElement> items) {
      var list = new List<Component>();
      foreach (var xElement in items)
        list.Add(new Component {
          ifcGuid = xElement.Attribute("IfcGuid")?.Value,
          authoringToolId = xElement.Element("AuthoringToolId")?.Value,
          originatingSystem = xElement.Element("OriginatingSystem")?.Value
        });

      return list;
    }

    /// <summary>
    ///   The private method extracts the contents of the zipped image and
    ///   converts it into a base64 string. Returns the created Snapshot struct.
    /// </summary>
    /// <param name="entry">The ZipArchiveEntry containing the image.</param>
    /// <returns>Returns a Task of the promised Snapshot.</returns>
    private Task<Snapshot> snapshotUsing(ZipArchiveEntry entry) {
      return Task.Run(async () => {
        var extension = entry.FullName.Split(".").Last();
        var mime = $"data:image/{extension};base64";
        var type = (Snapshot.Type) Enum.Parse(typeof(Snapshot.Type), extension);
        var buffer = new byte[entry.Length];
        var image = await entry
          .Open()
          .ReadAsync(buffer, 0, buffer.Length);
        var base64String = Convert.ToBase64String(buffer.ToArray());
        return new Snapshot {
          snapshotType = type,
//          snapshotData = $"{mime},{base64String}"
        };
      });
    }
  }
}