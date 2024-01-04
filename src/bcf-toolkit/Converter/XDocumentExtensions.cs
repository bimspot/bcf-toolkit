using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using RecursiveDataAnnotationsValidation;

namespace BcfToolkit.Converter;

/// <summary>
///   A list of convenience extension methods on the XDocument class.
///   The BCF version specific parsing logic is defined here.
/// </summary>
public static class XDocumentExtensions {
  public static T BcfObject<T>(this XDocument document) {
    var s = new XmlSerializer(typeof(T));
    var deserialized = (T)s.Deserialize(document.CreateReader())!;
    var validator = new RecursiveDataAnnotationValidator();
    var validationErrors = new List<ValidationResult>();
    if (validator.TryValidateObjectRecursive(deserialized, validationErrors))
      return deserialized;
    var errors = string.Join(
      "\n",
      validationErrors.Select(r =>
        $"Validation failed for members: '{string.Join((string?)",", (IEnumerable<string?>)r.MemberNames)}' with the error: '{r.ErrorMessage}'."
      ));
    throw new ArgumentException($"Validation failed:\n{errors}");
  }
}