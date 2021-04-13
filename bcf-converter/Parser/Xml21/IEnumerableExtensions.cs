using System.Collections.Generic;
using System.Xml.Linq;
using bcf_converter.Model;

namespace bcf_converter.Parser.Xml21 {
  /// <summary>
  ///   A list of convenience extension methods on the IEnumerable interface
  ///   where the items in the list are of `XElement` class.
  ///   The BCF version specific parsing logic is defined here.
  /// </summary>
  public static class IEnumerableExtensions {
    /// <summary>
    ///   Creates a list of Components using the data in the list of XElements.
    /// </summary>
    /// <param name="items">
    ///   A list of XElement's with the component elements in XML.
    /// </param>
    /// <returns>Returns a list of Component instances.</returns>
    public static List<Component> ComponentList(
      this IEnumerable<XElement> items) {
      var list = new List<Component>();
      foreach (var xElement in items)
        list.Add(new Component {
          IfcGuid = xElement.Attribute("IfcGuid")?.Value,
          AuthoringToolId = xElement.Element("AuthoringToolId")?.Value,
          OriginatingSystem = xElement.Element("OriginatingSystem")?.Value
        });
      return list;
    }
  }
}