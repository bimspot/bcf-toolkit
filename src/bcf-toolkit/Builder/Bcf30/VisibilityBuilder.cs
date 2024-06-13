using System;
using BcfToolkit.Builder.Bcf30.Interfaces;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

public partial class VisibilityBuilder :
  IVisibilityBuilder<VisibilityBuilder, ComponentBuilder, ViewSetupHintsBuilder> {
  private readonly ComponentVisibility _visibility = new();

  public VisibilityBuilder SetDefaultVisibility(bool? visibility) {
    _visibility.DefaultVisibility = visibility.GetValueOrDefault();
    return this;
  }

  public VisibilityBuilder AddException(Action<ComponentBuilder> builder) {
    var component =
      BuilderUtils.BuildItem<ComponentBuilder, Component>(builder);
    _visibility.Exceptions.Add(component);
    return this;
  }

  public VisibilityBuilder SetViewSetupHints(
    Action<ViewSetupHintsBuilder> builder) {
    var viewSetupHints =
      (ViewSetupHints)BuilderUtils
        .BuildItem<ViewSetupHintsBuilder, IViewSetupHints>(builder);
    _visibility.ViewSetupHints = viewSetupHints;
    return this;
  }

  public ComponentVisibility Build() {
    return BuilderUtils.ValidateItem(_visibility);
  }
}