using System;
using System.Collections.Generic;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Bcf21;

public partial class VisualizationInfoBuilder{
  
  public VisualizationInfoBuilder AddSelections(List<Component> components) {
    components.ForEach(_visualizationInfo.Components.Selection.Add);
    return this;
  }
  
  public VisualizationInfoBuilder SetVisibility(
    ComponentVisibility componentVisibility) {
    _visualizationInfo.Components.Visibility = componentVisibility;
    return this;
  }
  
  public VisualizationInfoBuilder AddBitmaps(List<VisualizationInfoBitmap> bitmaps) {
    bitmaps.ForEach( _visualizationInfo.Bitmap.Add);
    return this;
  }
  
  public VisualizationInfoBuilder AddColorings(List<ComponentColoringColor> colorings) {
    colorings.ForEach(_visualizationInfo.Components.Coloring.Add);
    return this;
  }
  
  public VisualizationInfoBuilder AddLines(List<Line> lines) {
    lines.ForEach(_visualizationInfo.Lines.Add);
    return this;
  }
  
  public VisualizationInfoBuilder
    AddClippingPlanes(List<ClippingPlane> clippingPlanes) {
    clippingPlanes.ForEach(_visualizationInfo.ClippingPlanes.Add);
    return this;
  }
  
  public VisualizationInfoBuilder SetOrthogonalCamera(
    OrthogonalCamera orthogonalCamera) {
    _visualizationInfo.OrthogonalCamera = orthogonalCamera;
    return this;
  }
  
  public VisualizationInfoBuilder SetPerspectiveCamera(
    PerspectiveCamera perspectiveCamera) {
    _visualizationInfo.PerspectiveCamera = perspectiveCamera;
    return this;
  }
  

  
}