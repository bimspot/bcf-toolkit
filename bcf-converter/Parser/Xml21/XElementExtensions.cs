using System;
using System.Xml.Linq;
using bcf.bcf21;

namespace bcf_converter.Parser.Xml21 {
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
    public static Direction Direction(this XElement item, string element) {
      return new Direction {
        X = Convert.ToSingle(item.Element(element)?.Element("X")?.Value),
        Y = Convert.ToSingle(item.Element(element)?.Element("Y")?.Value),
        Z = Convert.ToSingle(item.Element(element)?.Element("Z")?.Value)
      };
    }

    public static Point Point(this XElement item, string element) {
      return new Point {
        X = Convert.ToSingle(item.Element(element)?.Element("X")?.Value),
        Y = Convert.ToSingle(item.Element(element)?.Element("Y")?.Value),
        Z = Convert.ToSingle(item.Element(element)?.Element("Z")?.Value)
      };
    }
  }
}