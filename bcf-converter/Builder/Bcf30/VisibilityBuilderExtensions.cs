using System;
using bcf.bcf30;

namespace bcf.Builder.Bcf30; 

public partial class VisibilityBuilder : IVisibilityBuilderExtension<VisibilityBuilder, ViewSetupHintsBuilder> {
  public VisibilityBuilder AddViewSetupHints(Action<ViewSetupHintsBuilder> builder) {
    var viewSetupHints =
      (ViewSetupHints)BuilderUtils
        .BuildItem<ViewSetupHintsBuilder, IViewSetupHints>(builder);
    _visibility.ViewSetupHints = viewSetupHints;
    return this;
  }
}