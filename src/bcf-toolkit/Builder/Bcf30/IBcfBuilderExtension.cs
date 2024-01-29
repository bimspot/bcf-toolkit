using System;

namespace BcfToolkit.Builder.Bcf30;

public interface IBcfBuilderExtension<
  out TBuilder,
  out TExtensionsBuilder,
  out TDocumentInfoBuilder> {
  /// <summary>
  ///   Returns the builder object set with the `Extensions`.
  /// </summary>
  /// <param name="builder">Builder of the markup.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetExtensions(Action<TExtensionsBuilder> builder);
  /// <summary>
  ///   Returns the builder object set with the `DocumentInfo`.
  /// </summary>
  /// <param name="builder">Builder of the document info.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetDocumentInfo(Action<TDocumentInfoBuilder> builder);
}