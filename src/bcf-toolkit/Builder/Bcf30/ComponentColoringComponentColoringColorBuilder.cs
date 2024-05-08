using System;
using BcfToolkit.Builder.Bcf30.Interfaces;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

public partial class ComponentColoringComponentColoringColorBuilder :
  IComponentColoringColorBuilder<ComponentColoringComponentColoringColorBuilder, ComponentBuilder>,
  IDefaultBuilder<ComponentColoringComponentColoringColorBuilder> {
  private readonly ComponentColoringColor _color = new();

  public ComponentColoringComponentColoringColorBuilder SetColor(string color) {
    _color.Color = color;
    return this;
  }

  public ComponentColoringComponentColoringColorBuilder AddComponent(Action<ComponentBuilder> builder) {
    var component =
      (Component)BuilderUtils.BuildItem<ComponentBuilder, IComponent>(builder);
    _color.Components.Add(component);
    return this;
  }

  public ComponentColoringColor Build() {
    return BuilderUtils.ValidateItem(_color);
  }

  public ComponentColoringComponentColoringColorBuilder WithDefaults() {
    this
      .SetColor("40E0D0")
      .AddComponent(comp => comp.WithDefaults());
    return this;
  }
}