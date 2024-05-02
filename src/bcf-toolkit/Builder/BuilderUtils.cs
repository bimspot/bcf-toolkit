using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BcfToolkit.Builder.Interfaces;
using RecursiveDataAnnotationsValidation;

namespace BcfToolkit.Builder;

/// <summary>
///   Set of functions to help the builder classes.
/// </summary>
public static class BuilderUtils {
  /// <summary>
  ///   Method is a generic solution for handling the build process. Creates a
  ///   new builder, proceeds the action which is specified as a delegate.
  ///   Then it builds up the item which is specified as a type parameter.
  /// </summary>
  /// <param name="itemBuilder">Item builder delegate.</param>
  /// <typeparam name="TBuilder">The builder type parameter.</typeparam>
  /// <typeparam name="TItem">The item type parameter, which will be built.
  /// </typeparam>
  /// <returns>Returns the specified built type.</returns>
  public static TItem BuildItem<TBuilder, TItem>(Action<TBuilder> itemBuilder)
    where TBuilder : IBuilder<TItem>, new() {
    var builder = new TBuilder();
    itemBuilder(builder);
    return builder.Build();
  }

  public static TItem ValidateItem<TItem>(TItem item) {
    var validator = new RecursiveDataAnnotationValidator();
    var validationErrors = new List<ValidationResult>();
    if (validator.TryValidateObjectRecursive(item, validationErrors))
      return item;
    var errors = string.Join(
      "\n",
      validationErrors.Select(e => e.ErrorMessage));
    throw new ArgumentException($"Missing required fields(s):\n {errors}");
  }
}