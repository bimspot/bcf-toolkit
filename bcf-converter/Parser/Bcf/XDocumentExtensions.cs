using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using bcf.bcf21;

namespace bcf.Parser; 

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

  public static T BcfObject<T>(this XDocument document) {
    var s = new XmlSerializer(typeof(T));
    return (T)s.Deserialize(document.CreateReader())!;
  }
}