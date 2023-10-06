using System;
using BcfConverter.Model;

namespace BcfConverter.Builder;

public interface IDocumentInfoBuilder<out TBuilder, out TDocumentBuilder> : IBuilder<IDocumentInfo> {
  TBuilder AddDocument(Action<TDocumentBuilder> builder);
}