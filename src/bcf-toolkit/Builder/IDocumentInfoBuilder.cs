using System;
using BcfToolkit.Model;

namespace BcfToolkit.Builder;

public interface IDocumentInfoBuilder<
  out TBuilder,
  out TDocumentBuilder> :
  IBuilder<IDocumentInfo> {
  /// <summary>
  ///   Returns the builder object extended with a new document.
  /// </summary>
  /// <param name="builder">The builder of the document.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddDocument(Action<TDocumentBuilder> builder);
}