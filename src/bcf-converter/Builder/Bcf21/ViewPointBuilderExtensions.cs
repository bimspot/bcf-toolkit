using System;
using bcf.bcf21;

namespace bcf.Builder.Bcf21;

public partial class ViewPointBuilder :
  IViewPointBuilderExtension<ViewPointBuilder, ViewSetupHintsBuilder> {
  public ViewPointBuilder AddViewSetupHints(
    Action<ViewSetupHintsBuilder> builder) {
    var viewSetupHints =
      (ViewSetupHints)BuilderUtils
        .BuildItem<ViewSetupHintsBuilder, IViewSetupHints>(builder);
    _visualizationInfo.Components.ViewSetupHints = viewSetupHints;
    return this;
  }
}