using System;
using BcfConverter.Model;

namespace BcfConverter.Builder;

public interface
  IColorBuilder<out TBuilder, out TComponentBuilder> : IBuilder<IColor> {
  TBuilder AddColor(string color);
  TBuilder AddComponent(Action<TComponentBuilder> builder);
}