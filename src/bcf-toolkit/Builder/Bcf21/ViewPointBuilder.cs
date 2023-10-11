using System;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Bcf21;

public partial class ViewPointBuilder :
  IViewPointBuilder<
    ViewPointBuilder,
    ComponentBuilder,
    VisibilityBuilder,
    ColorBuilder,
    OrthogonalCameraBuilder,
    PerspectiveCameraBuilder,
    LineBuilder,
    ClippingPlaneBuilder,
    BitmapBuilder>,
  IDefaultBuilder<ViewPointBuilder> {
  private readonly VisualizationInfo _visualizationInfo = new();

  public ViewPointBuilder SetGuid(string guid) {
    _visualizationInfo.Guid = guid;
    return this;
  }

  public ViewPointBuilder AddSelection(Action<ComponentBuilder> builder) {
    var selection =
      (Component)BuilderUtils.BuildItem<ComponentBuilder, IComponent>(builder);
    _visualizationInfo.Components.Selection.Add(selection);
    return this;
  }

  public ViewPointBuilder SetVisibility(Action<VisibilityBuilder> builder) {
    var visibility =
      (ComponentVisibility)BuilderUtils
        .BuildItem<VisibilityBuilder, IVisibility>(builder);
    _visualizationInfo.Components.Visibility = visibility;
    return this;
  }

  public ViewPointBuilder AddColoring(Action<ColorBuilder> builder) {
    var color =
      (ComponentColoringColor)BuilderUtils.BuildItem<ColorBuilder, IColor>(
        builder);
    _visualizationInfo.Components.Coloring.Add(color);
    return this;
  }

  public ViewPointBuilder SetOrthogonalCamera(
    Action<OrthogonalCameraBuilder> builder) {
    var camera = (OrthogonalCamera)BuilderUtils
      .BuildItem<OrthogonalCameraBuilder, IOrthogonalCamera>(builder);
    _visualizationInfo.OrthogonalCamera = camera;
    return this;
  }

  public ViewPointBuilder SetPerspectiveCamera(
    Action<PerspectiveCameraBuilder> builder) {
    var camera = (PerspectiveCamera)BuilderUtils
      .BuildItem<PerspectiveCameraBuilder, IPerspectiveCamera>(builder);
    _visualizationInfo.PerspectiveCamera = camera;
    return this;
  }

  public ViewPointBuilder AddLine(Action<LineBuilder> builder) {
    var line = (Line)BuilderUtils.BuildItem<LineBuilder, ILine>(builder);
    _visualizationInfo.Lines.Add(line);
    return this;
  }

  public ViewPointBuilder
    AddClippingPlane(Action<ClippingPlaneBuilder> builder) {
    var clippingPlane =
      (ClippingPlane)BuilderUtils
        .BuildItem<ClippingPlaneBuilder, IClippingPlane>(builder);
    _visualizationInfo.ClippingPlanes.Add(clippingPlane);
    return this;
  }

  public ViewPointBuilder AddBitmap(Action<BitmapBuilder> builder) {
    var bitmap =
      (VisualizationInfoBitmap)BuilderUtils
        .BuildItem<BitmapBuilder, IBitmap>(builder);
    _visualizationInfo.Bitmap.Add(bitmap);
    return this;
  }

  public ViewPointBuilder WithDefaults() {
    this.SetGuid(Guid.NewGuid().ToString());
    return this;
  }

  public IVisualizationInfo Build() {
    return BuilderUtils.ValidateItem(_visualizationInfo);
  }
}