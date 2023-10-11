using System;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

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
  public IVisibility Build() {
    return BuilderUtils.ValidateItem(_visibility);
  }
}