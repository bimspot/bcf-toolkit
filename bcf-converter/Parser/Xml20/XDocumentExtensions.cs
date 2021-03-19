using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using bcf_converter.Model;

namespace bcf_converter.Parser.Xml20 {
  /// <summary>
  ///   A list of convenience extension methods on the XDocument class.
  ///   The BCF version specific parsing logic is defined here.
  /// </summary>
  public static class XDocumentExtensions {
    /// <summary>
    ///   Creates an OrthogonalCamera instance of the XDocument.
    /// </summary>
    /// <param name="document">The XDocument instance.</param>
    /// <returns>An OrthogonalCamera instance.</returns>
    public static OrthogonalCamera orthogonalCamera(this XDocument document) {
      return (from item in document.Descendants("OrthogonalCamera")
        select new OrthogonalCamera {
          cameraViewPoint = item.vector3("CameraViewPoint"),
          cameraDirection = item.vector3("CameraDirection"),
          cameraUpVector = item.vector3("CameraUpVector"),
          viewToWorldScale =
            Convert.ToSingle(item.Element("ViewToWorldScale")?.Value)
        }).SingleOrDefault();
    }

    /// <summary>
    ///   Creates an PerspectiveCamera instance of the XDocument.
    /// </summary>
    /// <param name="document">The XDocument instance.</param>
    /// <returns>An PerspectiveCamera instance.</returns>
    public static PerspectiveCamera perspectiveCamera(this XDocument document) {
      return (from item in document.Descendants("PerspectiveCamera")
        select new PerspectiveCamera {
          cameraViewPoint = item.vector3("CameraViewPoint"),
          cameraDirection = item.vector3("CameraDirection"),
          cameraUpVector = item.vector3("CameraUpVector"),
          fieldOfView = Convert.ToSingle(item.Element("FieldOfView")?.Value)
        }).SingleOrDefault();
    }
    
    /// <summary>
    ///   Uses LINQ to extract all information for the Header of the Markup.
    /// </summary>
    /// <param name="document"></param>
    /// <returns>Returns the Header document</returns>
    public static Header header(this XDocument document) {
      // TODO: finish this
      return new Header();
    }

    /// <summary>
    ///   Uses LINQ to extract all information from the elements in order to
    ///   create a Topic instance.
    /// </summary>
    /// <param name="document">
    ///   The XDocument containing a bcf file's content.
    /// </param>
    /// <returns>Returns a Topic created with the parsed data</returns>
    public static Topic topic(this XDocument document) {
      return (from item in document.Descendants("Topic")
        select new Topic {
          guid = item.Attribute("Guid")?.Value,
          topicType = item.Attribute("TopicType")?.Value,
          topicStatus = item.Attribute("TopicStatus")?.Value,
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
        }).Single();
    }

    /// <summary>
    ///   Uses LINQ to extract all information from the elements in order to
    ///   create a Viewpoint instance.
    /// </summary>
    /// <param name="document">
    ///   The XDocument containing a bcf file's content.
    /// </param>
    /// <returns>Returns a Topic created with the parsed data</returns>
    public static Viewpoint viewpoint(this XDocument document) {
      var visibleComponents = document
        .Descendants("Components")
        .Elements("Component")
        .Where(e =>
          bool.Parse(e.Attribute("Visible")?.Value ?? "false").Equals
            (true));

      var hiddenComponents = document
        .Descendants("Components")
        .Elements("Component")
        .Where(e =>
          bool.Parse(e.Attribute("Visible")?.Value ?? "false").Equals
            (false));

      var selectedComponents = document
        .Descendants("Components")
        .Elements("Component")
        .Where(e =>
          bool.Parse(e.Attribute("Selected")?.Value ?? "false").Equals
            (true));

      var colors = document
        .Descendants("Components")
        .Elements("Component")
        .GroupBy(i => i.Attribute("Color")?.Value ?? "FFFFFFFF",
          (key, result) => new Coloring {
            color = key,
            components = result.componentList()
          });

      // Note: Optimization Rules

      // If the list of hidden components is smaller than the list of visible
      // components: set default_visibility to true and put the hidden
      // components in exceptions.

      // If the list of visible components is smaller or equals the list of
      // hidden components: set default_visibility to false and put the
      // visible components in exceptions.

      var defaultVisibility =
        hiddenComponents.Count() < visibleComponents.Count();
      var exceptionalComponents = defaultVisibility
        ? hiddenComponents.componentList()
        : visibleComponents.componentList();

      return new Viewpoint {
        perspectiveCamera = document.perspectiveCamera(),
        orthogonalCamera = document.orthogonalCamera(),
        components = new Components {
          selection = selectedComponents.componentList(),
          coloring = colors.ToList(),
          visibility = new Visibility {
            defaultVisibility = defaultVisibility,
            exceptions = exceptionalComponents
          }
        }
      };
    }
  }
}