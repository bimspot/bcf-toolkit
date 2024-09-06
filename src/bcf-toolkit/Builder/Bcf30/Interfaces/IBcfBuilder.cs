using System;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30.Interfaces;

public interface IBcfBuilder<
  out TBuilder,
  out TMarkupBuilder,
  out TProjectBuilder,
  out TExtensionsBuilder,
  out TDocumentInfoBuilder> :
  IBuilder<Bcf>,
  IFromStreamBuilder<Bcf> {

  /// <summary>
  ///   Returns the builder object extended with a new `Markup`.
  /// </summary>
  /// <param name="builder">Builder of the markup.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddMarkup(Action<TMarkupBuilder> builder);

  /// <summary>
  ///   Returns the builder object set with the `ProjectInfo`.
  /// </summary>
  /// <param name="builder">Builder of the project info.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetProject(Action<TProjectBuilder> builder);

  /// <summary>
  ///   Returns the builder object set with the `Extensions`.
  /// </summary>
  /// <param name="builder">Builder of the extensions.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetExtensions(Action<TExtensionsBuilder> builder);

  /// <summary>
  ///   Returns the builder object set with the `DocumentInfo`.
  /// </summary>
  /// <param name="builder">Builder of the document info.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetDocument(Action<TDocumentInfoBuilder> builder);
}