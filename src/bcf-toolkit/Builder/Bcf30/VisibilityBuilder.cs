using System;
using System.Collections.Generic;
using BcfToolkit.Builder.Bcf30.Interfaces;
using BcfToolkit.Builder.Interfaces;
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
    var component =
      BuilderUtils.BuildItem<ComponentBuilder, Component>(builder);
    _visibility.Exceptions.Add(component);
    return this;
  }

  public VisibilityBuilder AddExceptions(List<Component> exceptions) {
    exceptions.ForEach(_visibility.Exceptions.Add);
    return this;
  }

  public ComponentVisibility Build() {
    return BuilderUtils.ValidateItem(_visibility);
  }
}