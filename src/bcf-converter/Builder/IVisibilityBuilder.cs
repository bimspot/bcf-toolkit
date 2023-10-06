using System;
using BcfConverter.Model;

namespace BcfConverter.Builder;

public interface IVisibilityBuilder<
    out TBuilder,
    out TComponentBuilder> :
    IBuilder<IVisibility> {
  TBuilder AddDefaultVisibility(bool visibility);
  TBuilder AddExtension(Action<TComponentBuilder> builder);
}