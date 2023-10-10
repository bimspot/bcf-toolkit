using System;
using BcfToolkit.Model;

namespace BcfToolkit.Builder;

public interface IVisibilityBuilder<
    out TBuilder,
    out TComponentBuilder> :
    IBuilder<IVisibility> {
  TBuilder AddDefaultVisibility(bool visibility);
  TBuilder AddExtension(Action<TComponentBuilder> builder);
}