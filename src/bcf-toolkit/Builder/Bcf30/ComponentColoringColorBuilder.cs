using System;
using BcfToolkit.Builder.Bcf30.Interfaces;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

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
    _color.Components.Add(component);
    return this;
  }

  public ComponentColoringColor Build() {
    return BuilderUtils.ValidateItem(_color);
  }

  public ComponentColoringColorBuilder WithDefaults() {
    this
      .SetColor("40E0D0")
      .AddComponent(comp => comp.WithDefaults());
    return this;
  }
}