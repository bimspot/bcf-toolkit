using System;

namespace bcf.Builder;

public interface IDocumentInfoBuilder<out TBuilder, out TDocumentBuilder> : IBuilder<IDocumentInfo> {
  TBuilder AddDocument(Action<TDocumentBuilder> builder);
}