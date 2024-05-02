using System;
using BcfToolkit.Builder.Bcf21.Interfaces;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Bcf21;

public partial class VisualizationInfoBuilder :
  IViewPointBuilderExtension<VisualizationInfoBuilder, ViewSetupHintsBuilder> {
  public VisualizationInfoBuilder SetViewSetupHints(
    Action<ViewSetupHintsBuilder> builder) {
    var viewSetupHints =
      (ViewSetupHints)BuilderUtils
        .BuildItem<ViewSetupHintsBuilder, IViewSetupHints>(builder);
    _visualizationInfo.Components.ViewSetupHints = viewSetupHints;
    return this;
  }
}