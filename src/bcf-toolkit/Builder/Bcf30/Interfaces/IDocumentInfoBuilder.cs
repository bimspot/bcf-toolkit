using System;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30.Interfaces;

public interface IDocumentInfoBuilder<
  out TBuilder,
  out TDocumentBuilder> :
  IBuilder<DocumentInfo> {
  /// <summary>
  ///   Returns the builder object extended with a new document.
  /// </summary>
  /// <param name="builder">The builder of the document.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddDocument(Action<TDocumentBuilder> builder);
}