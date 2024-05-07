using System;
using BcfToolkit.Builder.Bcf21.Interfaces;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Bcf21;

public partial class VisualizationInfoBuilder :
  IVisualizationInfoBuilder<
    VisualizationInfoBuilder,
    ComponentBuilder,
    VisibilityBuilder,
    ComponentColoringColorBuilder,
    OrthogonalCameraBuilder,
    PerspectiveCameraBuilder,
    LineBuilder,
    ClippingPlaneBuilder,
    BitmapBuilder>,
  IDefaultBuilder<VisualizationInfoBuilder> {
  private readonly VisualizationInfo _visualizationInfo = new();

  public VisualizationInfoBuilder SetGuid(string guid) {
    _visualizationInfo.Guid = guid;
    return this;
  }

  public VisualizationInfoBuilder
    AddSelection(Action<ComponentBuilder> builder) {
    var selection =
      (Component)BuilderUtils.BuildItem<ComponentBuilder, IComponent>(builder);
    _visualizationInfo.Components.Selection.Add(selection);
    return this;
  }

  public VisualizationInfoBuilder SetVisibility(
    Action<VisibilityBuilder> builder) {
    var visibility =
      (ComponentVisibility)BuilderUtils
        .BuildItem<VisibilityBuilder, IVisibility>(builder);
    _visualizationInfo.Components.Visibility = visibility;
    return this;
  }

  public VisualizationInfoBuilder AddColoring(Action<ComponentColoringColorBuilder> builder) {
    var color =
      (ComponentColoringColor)BuilderUtils.BuildItem<ComponentColoringColorBuilder, IColor>(
        builder);
    _visualizationInfo.Components.Coloring.Add(color);
    return this;
  }

  public VisualizationInfoBuilder SetOrthogonalCamera(
    Action<OrthogonalCameraBuilder> builder) {
    var camera = (OrthogonalCamera)BuilderUtils
      .BuildItem<OrthogonalCameraBuilder, IOrthogonalCamera>(builder);
    _visualizationInfo.OrthogonalCamera = camera;
    return this;
  }

  public VisualizationInfoBuilder SetPerspectiveCamera(
    Action<PerspectiveCameraBuilder> builder) {
    var camera = (PerspectiveCamera)BuilderUtils
      .BuildItem<PerspectiveCameraBuilder, IPerspectiveCamera>(builder);
    _visualizationInfo.PerspectiveCamera = camera;
    return this;
  }

  public VisualizationInfoBuilder AddLine(Action<LineBuilder> builder) {
    var line = (Line)BuilderUtils.BuildItem<LineBuilder, ILine>(builder);
    _visualizationInfo.Lines.Add(line);
    return this;
  }

  public VisualizationInfoBuilder
    AddClippingPlane(Action<ClippingPlaneBuilder> builder) {
    var clippingPlane =
      (ClippingPlane)BuilderUtils
        .BuildItem<ClippingPlaneBuilder, IClippingPlane>(builder);
    _visualizationInfo.ClippingPlanes.Add(clippingPlane);
    return this;
  }

  public VisualizationInfoBuilder AddBitmap(Action<BitmapBuilder> builder) {
    var bitmap =
      (VisualizationInfoBitmap)BuilderUtils
        .BuildItem<BitmapBuilder, IBitmap>(builder);
    _visualizationInfo.Bitmap.Add(bitmap);
    return this;
  }

  public VisualizationInfoBuilder WithDefaults() {
    this.SetGuid(Guid.NewGuid().ToString());
    return this;
  }

  public VisualizationInfo Build() {
    return BuilderUtils.ValidateItem(_visualizationInfo);
  }
}