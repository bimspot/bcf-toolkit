using System;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Bcf21;

public partial class ViewPointBuilder :
  IViewPointBuilderExtension<ViewPointBuilder, ViewSetupHintsBuilder> {
  public ViewPointBuilder SetViewSetupHints(
    Action<ViewSetupHintsBuilder> builder) {
    var viewSetupHints =
      (ViewSetupHints)BuilderUtils
        .BuildItem<ViewSetupHintsBuilder, IViewSetupHints>(builder);
    _visualizationInfo.Components.ViewSetupHints = viewSetupHints;
    return this;
  }
}