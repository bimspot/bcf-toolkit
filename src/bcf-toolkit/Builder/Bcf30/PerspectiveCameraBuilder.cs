using System;
using BcfToolkit.Builder.Bcf30.Interfaces;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

public partial class PerspectiveCameraBuilder :
  IPerspectiveCameraBuilder<PerspectiveCameraBuilder, CameraBuilder>,
  IDefaultBuilder<PerspectiveCameraBuilder> {
  private readonly PerspectiveCamera _camera = new();

  public PerspectiveCameraBuilder SetCamera(Action<CameraBuilder> builder) {
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

  public PerspectiveCameraBuilder SetFieldOfView(double angle) {
    _camera.FieldOfView = angle;
    return this;
  }

  public PerspectiveCameraBuilder WithDefaults() {
    this
      .SetCamera(cam => cam.WithDefaults())
      .SetFieldOfView(0.0)
      .SetAspectRatio(1.0);
    return this;
  }

  public PerspectiveCamera Build() {
    return BuilderUtils.ValidateItem(_camera);
  }
}