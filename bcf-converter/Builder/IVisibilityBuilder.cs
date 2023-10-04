using System;

namespace bcf.Builder;

public interface
  IVisibilityBuilder<out TBuilder,
    out TComponentBuilder> : IBuilder<IVisibility> {
  TBuilder AddDefaultVisibility(bool visibility);
  TBuilder AddExtension(Action<TComponentBuilder> builder);
}