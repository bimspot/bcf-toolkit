using System;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

public partial class VisibilityBuilder :
  IVisibilityBuilderExtension<VisibilityBuilder, ViewSetupHintsBuilder> {
  public VisibilityBuilder SetViewSetupHints(
    Action<ViewSetupHintsBuilder> builder) {
    var viewSetupHints =
      (ViewSetupHints)BuilderUtils
        .BuildItem<ViewSetupHintsBuilder, IViewSetupHints>(builder);
    _visibility.ViewSetupHints = viewSetupHints;
    return this;
  }
}