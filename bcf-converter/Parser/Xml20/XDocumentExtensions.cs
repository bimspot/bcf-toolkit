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
    }
  }
}