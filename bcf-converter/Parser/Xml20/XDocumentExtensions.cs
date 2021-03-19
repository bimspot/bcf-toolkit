using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
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
    public static OrthogonalCamera? OrthogonalCamera(this XDocument document) {
      return (from item in document.Descendants("OrthogonalCamera")
        select new OrthogonalCamera {
          CameraViewPoint = item.Point("CameraViewPoint"),
          CameraDirection = item.Direction("CameraDirection"),
          CameraUpVector = item.Direction("CameraUpVector"),
          ViewToWorldScale =
            Convert.ToSingle(item.Element("ViewToWorldScale")?.Value)
        }).SingleOrDefault();
    }

    /// <summary>
    ///   Creates an PerspectiveCamera instance of the XDocument.
    /// </summary>
    /// <param name="document">The XDocument instance.</param>
    /// <returns>An PerspectiveCamera instance.</returns>
    public static PerspectiveCamera? PerspectiveCamera(this XDocument document) {
      return (from item in document.Descendants("PerspectiveCamera")
        select new PerspectiveCamera {
          CameraViewPoint = item.Point("CameraViewPoint"),
          CameraDirection = item.Direction("CameraDirection"),
          CameraUpVector = item.Direction("CameraUpVector"),
          FieldOfView = Convert.ToSingle(item.Element("FieldOfView")?.Value)
        }).SingleOrDefault();
    }
    
    /// <summary>
    ///   Uses LINQ to extract all information for the Header of the Markup.
    /// </summary>
    /// <param name="document"></param>
    /// <returns>Returns the Header document</returns>
    public static Header Header(this XDocument document) {
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
    public static Topic? Topic(this XDocument document) {
      var s = new XmlSerializer(typeof(Topic));
      return (Topic)s.Deserialize(document.CreateReader());
      
      // return (from item in document.Descendants("Topic")
      //   select new Topic {
      //     Guid = item.Attribute("Guid")?.Value,
      //     TopicType = item.Attribute("TopicType")?.Value,
      //     TopicStatus = item.Attribute("TopicStatus")?.Value,
      //     Title = item.Element("Title")?.Value,
      //     Priority = item.Element("Priority")?.Value,
      //     Index = item.Element("Index")?.Value,
      //     Labels = item.Element("Labels")?.Value,
      //     CreationDate = item.Element("CreationDate")?.Value,
      //     CreationAuthor = item.Element("CreationAuthor")?.Value,
      //     ModifiedDate = item.Element("ModifiedDate")?.Value,
      //     ModifiedAuthor = item.Element("ModifiedAuthor")?.Value,
      //     DueDate = item.Element("DueDate")?.Value,
      //     AssignedTo = item.Element("AssignedTo")?.Value,
      //     Description = item.Element("Description")?.Value,
      //   }).Single();
    }

    /// <summary>
    ///   Uses LINQ to extract all information from the elements in order to
    ///   create a Viewpoint instance.
    /// </summary>
    /// <param name="document">
    ///   The XDocument containing a bcf file's content.
    /// </param>
    /// <returns>Returns a Topic created with the parsed data</returns>
    public static VisualizationInfo VisualizationInfo(this XDocument document) {
      
      var s = new XmlSerializer(typeof(VisualizationInfo));
      return (VisualizationInfo)s.Deserialize(document.CreateReader());
      
      // var visibleComponents = document
      //   .Descendants("Components")
      //   .Elements("Component")
      //   .Where(e =>
      //     bool.Parse(e.Attribute("Visible")?.Value ?? "false").Equals
      //       (true));
      //
      // var hiddenComponents = document
      //   .Descendants("Components")
      //   .Elements("Component")
      //   .Where(e =>
      //     bool.Parse(e.Attribute("Visible")?.Value ?? "false").Equals
      //       (false));
      //
      // var selectedComponents = document
      //   .Descendants("Components")
      //   .Elements("Component")
      //   .Where(e =>
      //     bool.Parse(e.Attribute("Selected")?.Value ?? "false").Equals
      //       (true));
      //
      // var colors = document
      //   .Descendants("Components")
      //   .Elements("Component")
      //   .GroupBy(i => i.Attribute("Color")?.Value ?? "FFFFFFFF",
      //     (key, result) => new Coloring {
      //       color = key,
      //       components = result.componentList()
      //     });
      //
      // // Note: Optimization Rules
      //
      // // If the list of hidden components is smaller than the list of visible
      // // components: set default_visibility to true and put the hidden
      // // components in exceptions.
      //
      // // If the list of visible components is smaller or equals the list of
      // // hidden components: set default_visibility to false and put the
      // // visible components in exceptions.
      //
      // var defaultVisibility =
      //   hiddenComponents.Count() < visibleComponents.Count();
      // var exceptionalComponents = defaultVisibility
      //   ? hiddenComponents.componentList()
      //   : visibleComponents.componentList();
      //
      // return new Viewpoint {
      //   perspectiveCamera = document.perspectiveCamera(),
      //   orthogonalCamera = document.orthogonalCamera(),
      //   components = new Components {
      //     selection = selectedComponents.componentList(),
      //     coloring = colors.ToList(),
      //     visibility = new Visibility {
      //       defaultVisibility = defaultVisibility,
      //       exceptions = exceptionalComponents
      //     }
      //   }
      // };
    }
  }
}