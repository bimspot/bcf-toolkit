using System;
using System.Xml.Linq;
using bcf_converter.Model;

namespace bcf_converter.Parser.Xml20 {
  /// <summary>
  ///   A list of convenience extension methods on the XElementExtensions class.
  ///   The BCF version specific parsing logic is defined here.
  /// </summary>
  public static class XElementExtensions {
    /// <summary>
    ///   Creates a Vector3 instance using the sub-elements of the XElement of
    ///   the provided name.
    /// </summary>
    /// <param name="item">The XElement instance.</param>
    /// <param name="element">
    ///   The name of the sub-element to be parsed into Vector3.
    /// </param>
    /// <returns>A Vector3 instance.</returns>
    public static Vector3 vector3(this XElement item, string element) {
      return new Vector3 {
        x = Convert.ToSingle(item.Element(element)?.Element("X")?.Value),
        y = Convert.ToSingle(item.Element(element)?.Element("Y")?.Value),
        z = Convert.ToSingle(item.Element(element)?.Element("Z")?.Value)
      };
    }
  }
}