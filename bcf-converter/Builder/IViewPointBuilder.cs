using System;
using System.Collections.Generic;

namespace bcf.Builder;

public interface IViewPointBuilder : IBuilder<IVisualizationInfo> {
  IViewPointBuilder AddViewSetupHints(
    bool spaceVisible,
    bool spaceBoundariesVisible,
    bool openingVisible);

  IViewPointBuilder AddSelection(List<string> components);

  IViewPointBuilder AddVisibility(
    bool defaultVisibility,
    List<string> exceptions);

  IViewPointBuilder AddColoring(string color, List<string> components);

  IViewPointBuilder AddOrthogonalCamera(Action<ICameraBuilder> cameraBuilder,
    double viewToWorldScale);

  IViewPointBuilder AddOrthogonalCamera(Action<ICameraBuilder> cameraBuilder,
    double viewToWorldScale, double aspectRatio);

  IViewPointBuilder AddPerspectiveCamera(Action<ICameraBuilder> cameraBuilder,
    double fieldOfView);

  IViewPointBuilder AddLine();
  IViewPointBuilder AddClippingPlane();
  IViewPointBuilder AddBitmap();
}