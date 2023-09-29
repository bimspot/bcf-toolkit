using System;
using System.Collections.Generic;

namespace bcf.Builder.Bcf30;

public class
  ViewPointBuilder : IViewPointBuilder, IBuilder<IVisualizationInfo> {
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
    throw new NotImplementedException();
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
    throw new NotImplementedException();
  }
}