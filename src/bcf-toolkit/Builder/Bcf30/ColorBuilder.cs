using System;
using BcfToolkit.Builder.Bcf30.Interfaces;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

public partial class ColorBuilder :
  IColorBuilder<ColorBuilder, ComponentBuilder>,
  IDefaultBuilder<ColorBuilder> {
  private readonly ComponentColoringColor _color = new();

  public ColorBuilder SetColor(string color) {
    _color.Color = color;
    return this;
  }

  public ColorBuilder AddComponent(Action<ComponentBuilder> builder) {
    var component =
      (Component)BuilderUtils.BuildItem<ComponentBuilder, IComponent>(builder);
    _color.Components.Add(component);
    return this;
  }

  public ComponentColoringColor Build() {
    return BuilderUtils.ValidateItem(_color);
  }

  public ColorBuilder WithDefaults() {
    this
      .SetColor("40E0D0")
      .AddComponent(comp => comp.WithDefaults());
    return this;
  }
}