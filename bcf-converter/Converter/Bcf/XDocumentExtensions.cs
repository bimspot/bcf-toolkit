using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using bcf.bcf21;
using RecursiveDataAnnotationsValidation;

namespace bcf.Converter;

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
    var deserialized = (T)s.Deserialize(document.CreateReader())!;
    var validator = new RecursiveDataAnnotationValidator();
    var validationErrors = new List<ValidationResult>();
    if (validator.TryValidateObjectRecursive(deserialized, validationErrors))
      return deserialized;
    var errors = string.Join(
      "\n", 
      validationErrors.Select(e => e.ErrorMessage));
    throw new ArgumentException($"Missing required fields(s):\n {errors}");
  }
}