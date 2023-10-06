using System;
using BcfConverter.Model;
using BcfConverter.Model.Bcf30;

namespace BcfConverter.Builder.Bcf30;

public partial class OrthogonalCameraBuilder :
  IOrthogonalCameraBuilder<OrthogonalCameraBuilder, CameraBuilder> {
  private readonly OrthogonalCamera _camera = new();

  public OrthogonalCameraBuilder AddCamera(Action<CameraBuilder> builder) {
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

  public OrthogonalCameraBuilder AddViewToWorldScale(double scale) {
    _camera.ViewToWorldScale = scale;
    return this;
  }

  public IOrthogonalCamera Build() {
    return _camera;
  }
}