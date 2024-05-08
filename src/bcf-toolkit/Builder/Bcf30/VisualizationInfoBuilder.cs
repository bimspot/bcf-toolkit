using System;
using BcfToolkit.Builder.Bcf30.Interfaces;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

public partial class VisualizationInfoBuilder :
  IVisualizationInfoBuilder<
    VisualizationInfoBuilder,
    ComponentBuilder,
    VisibilityBuilder,
    ComponentColoringComponentColoringColorBuilder,
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
      BuilderUtils.BuildItem<ComponentBuilder, Component>(builder);
    _visualizationInfo.Components.Selection.Add(selection);
    return this;
  }

  public VisualizationInfoBuilder SetVisibility(
    Action<VisibilityBuilder> builder) {
    var visibility =
      BuilderUtils
        .BuildItem<VisibilityBuilder, ComponentVisibility>(builder);
    _visualizationInfo.Components.Visibility = visibility;
    return this;
  }

  public VisualizationInfoBuilder AddColoring(Action<ComponentColoringComponentColoringColorBuilder> builder) {
    var color =
      BuilderUtils.BuildItem<ComponentColoringComponentColoringColorBuilder, ComponentColoringColor>(
        builder);
    _visualizationInfo.Components.Coloring.Add(color);
    return this;
  }

  public VisualizationInfoBuilder SetOrthogonalCamera(
    Action<OrthogonalCameraBuilder> builder) {
    var camera = BuilderUtils
      .BuildItem<OrthogonalCameraBuilder, OrthogonalCamera>(builder);
    _visualizationInfo.OrthogonalCamera = camera;
    return this;
  }

  public VisualizationInfoBuilder SetPerspectiveCamera(
    Action<PerspectiveCameraBuilder> builder) {
    var camera = BuilderUtils
      .BuildItem<PerspectiveCameraBuilder, PerspectiveCamera>(builder);
    _visualizationInfo.PerspectiveCamera = camera;
    return this;
  }

  public VisualizationInfoBuilder AddLine(Action<LineBuilder> builder) {
    var line = BuilderUtils.BuildItem<LineBuilder, Line>(builder);
    _visualizationInfo.Lines.Add(line);
    return this;
  }

  public VisualizationInfoBuilder AddClippingPlane(
    Action<ClippingPlaneBuilder> builder) {
    var clippingPlane =
      BuilderUtils
        .BuildItem<ClippingPlaneBuilder, ClippingPlane>(builder);
    _visualizationInfo.ClippingPlanes.Add(clippingPlane);
    return this;
  }

  public VisualizationInfoBuilder AddBitmap(Action<BitmapBuilder> builder) {
    var bitmap =
      BuilderUtils
        .BuildItem<BitmapBuilder, Bitmap>(builder);
    _visualizationInfo.Bitmaps.Add(bitmap);
    return this;
  }

  public VisualizationInfoBuilder WithDefaults() {
    SetGuid(Guid.NewGuid().ToString());
    return this;
  }

  public VisualizationInfo Build() {
    return BuilderUtils.ValidateItem(_visualizationInfo);
  }
}