using System;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Bcf21;

public class VisibilityBuilder :
  IVisibilityBuilder<VisibilityBuilder, ComponentBuilder> {
  private readonly ComponentVisibility _visibility = new();

  public VisibilityBuilder AddDefaultVisibility(bool visibility) {
    _visibility.DefaultVisibility = visibility;
    return this;
  }

  public VisibilityBuilder AddExtension(Action<ComponentBuilder> builder) {
    var exception =
      (Component)BuilderUtils.BuildItem<ComponentBuilder, IComponent>(builder);
    _visibility.Exceptions.Add(exception);
    return this;
  }
  public IVisibility Build() {
    return BuilderUtils.ValidateItem(_visibility);
  }
}