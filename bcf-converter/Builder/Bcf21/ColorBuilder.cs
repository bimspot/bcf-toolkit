using System;
using bcf.bcf21;

namespace bcf.Builder.Bcf21; 

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
    return _color;
  }
}