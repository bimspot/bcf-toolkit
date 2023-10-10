using System;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Bcf21;

public class ColorBuilder : IColorBuilder<ColorBuilder, ComponentBuilder> {
  private readonly ComponentColoringColor _color = new();

  public ColorBuilder AddColor(string color) {
    _color.Color = color;
    return this;
  }

  public ColorBuilder AddComponent(Action<ComponentBuilder> builder) {
    var component =
      (Component)BuilderUtils.BuildItem<ComponentBuilder, IComponent>(builder);
    _color.Component.Add(component);
    return this;
  }

  public IColor Build() {
    return BuilderUtils.ValidateItem(_color);
  }
}