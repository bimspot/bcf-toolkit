using System.Collections.Generic;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

public partial class VisualizationInfoBuilder {
  public VisualizationInfoBuilder AddSelections(List<Component>? selections) {
    _visualizationInfo.Components ??= new Components();
    selections?.ForEach(_visualizationInfo.Components.Selection.Add);
    return this;
  }

  public VisualizationInfoBuilder AddColorings(
    List<ComponentColoringColor>? colors) {
    _visualizationInfo.Components ??= new Components();
    colors?.ForEach(_visualizationInfo.Components.Coloring.Add);
    return this;
  }

  public VisualizationInfoBuilder AddLines(List<Line>? lines) {
    lines?.ForEach(_visualizationInfo.Lines.Add);
    return this;
  }

  public VisualizationInfoBuilder AddClippingPlanes(
    List<ClippingPlane>? clippingPlanes) {
    clippingPlanes?.ForEach(_visualizationInfo.ClippingPlanes.Add);
    return this;
  }

  public VisualizationInfoBuilder AddBitmaps(List<Bitmap>? bitmaps) {
    bitmaps?.ForEach(_visualizationInfo.Bitmaps.Add);
    return this;
  }
}