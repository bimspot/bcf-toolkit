using System;
using bcf.bcf21;

namespace bcf.Builder.Bcf21;

public class PerspectiveCameraBuilder : 
  IPerspectiveCameraBuilder<PerspectiveCameraBuilder, CameraBuilder> {
  private readonly PerspectiveCamera _camera = new();

  public PerspectiveCameraBuilder AddCamera(Action<CameraBuilder> builder) {
    var b = new CameraBuilder();
    builder(b);
    var camera = b.Build();
    _camera.CameraViewPoint = (Point)camera.GetType().GetProperty("viewPoint")
      ?.GetValue(camera, null)!;
    _camera.CameraDirection = (Direction)camera.GetType()
      .GetProperty("direction")
      ?.GetValue(camera, null)!;
    _camera.CameraUpVector = (Direction)camera.GetType().GetProperty("upVector")
      ?.GetValue(camera, null)!;
    return this;
  }

  public PerspectiveCameraBuilder AddFieldOfView(double angle) {
    _camera.FieldOfView = angle;
    return this;
  }

  public IPerspectiveCamera Build() {
    return _camera;
  }
}