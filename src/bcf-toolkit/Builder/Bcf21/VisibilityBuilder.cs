using System;
using BcfToolkit.Builder.Bcf21.Interfaces;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf21;
using BcfToolkit.Model.Interfaces;

namespace BcfToolkit.Builder.Bcf21;

public partial class VisibilityBuilder :
  IVisibilityBuilder<VisibilityBuilder, ComponentBuilder> {
  private readonly ComponentVisibility _visibility = new();

  public VisibilityBuilder SetDefaultVisibility(bool visibility) {
    _visibility.DefaultVisibility = visibility;
    return this;
  }

  public VisibilityBuilder AddException(Action<ComponentBuilder> builder) {
    var exception =
      (Component)BuilderUtils.BuildItem<ComponentBuilder, IComponent>(builder);
    _visibility.Exceptions.Add(exception);
    return this;
  }

  public ComponentVisibility Build() {
    return BuilderUtils.ValidateItem(_visibility);
  }
}