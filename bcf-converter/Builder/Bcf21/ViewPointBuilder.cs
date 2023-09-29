using System;
using System.Collections.Generic;
using bcf.bcf21;

namespace bcf.Builder.Bcf21;

public class ViewPointBuilder : IViewPointBuilder {
  private readonly VisualizationInfo _visualizationInfo = new();

  public IViewPointBuilder AddViewSetupHints(bool spaceVisible,
    bool spaceBoundariesVisible, bool openingVisible) {
    throw new NotImplementedException();
  }

  public IViewPointBuilder AddSelection(List<string> components) {
    throw new NotImplementedException();
  }

  public IViewPointBuilder AddVisibility(bool defaultVisibility,
    List<string> exceptions) {
    throw new NotImplementedException();
  }

  public IViewPointBuilder AddColoring(string color, List<string> components) {
    throw new NotImplementedException();
  }

  public IViewPointBuilder AddOrthogonalCamera(
    Action<ICameraBuilder> cameraBuilder,
    double viewToWorldScale) {
    var builder = new CameraBuilder();
    cameraBuilder(builder);
    var camera = builder.Build();
    _visualizationInfo.OrthogonalCamera = new OrthogonalCamera {
      CameraViewPoint = (Point)camera.GetType().GetProperty("viewPoint")
        ?.GetValue(camera, null)!,
      CameraDirection = (Direction)camera.GetType().GetProperty("direction")
        ?.GetValue(camera, null)!,
      CameraUpVector = (Direction)camera.GetType().GetProperty("upVector")
        ?.GetValue(camera, null)!,
      ViewToWorldScale = viewToWorldScale
    };
    return this;
  }

  public IViewPointBuilder AddOrthogonalCamera(
    Action<ICameraBuilder> cameraBuilder,
    double viewToWorldScale, double aspectRatio) {
    throw new NotImplementedException();
  }

  public IViewPointBuilder
    AddPerspectiveCamera(Action<ICameraBuilder> cameraBuilder,
      double fieldOfView) {
    throw new NotImplementedException();
  }

  public IViewPointBuilder AddLine() {
    throw new NotImplementedException();
  }

  public IViewPointBuilder AddClippingPlane() {
    throw new NotImplementedException();
  }

  public IViewPointBuilder AddBitmap() {
    throw new NotImplementedException();
  }

  public IVisualizationInfo Build() {
    return _visualizationInfo;
  }
}