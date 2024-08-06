using System;
using BcfToolkit.Builder.Bcf21.Interfaces;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf21;
using BcfToolkit.Model.Interfaces;

namespace BcfToolkit.Builder.Bcf21;

public partial class ComponentColoringColorBuilder :
  IComponentColoringColorBuilder<ComponentColoringColorBuilder, ComponentBuilder>,
  IDefaultBuilder<ComponentColoringColorBuilder> {
  private readonly ComponentColoringColor _color = new();

  public ComponentColoringColorBuilder SetColor(string color) {
    _color.Color = color;
    return this;
  }

  public ComponentColoringColorBuilder AddComponent(Action<ComponentBuilder> builder) {
    var component =
      (Component)BuilderUtils.BuildItem<ComponentBuilder, IComponent>(builder);
    _color.Component.Add(component);
    return this;
  }

  public ComponentColoringColorBuilder WithDefaults() {
    this
      .SetColor("40E0D0")
      .AddComponent(comp => comp.WithDefaults());
    return this;
  }

  public ComponentColoringColor Build() {
    return BuilderUtils.ValidateItem(_color);
  }
}