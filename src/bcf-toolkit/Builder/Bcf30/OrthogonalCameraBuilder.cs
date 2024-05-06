using System;
using BcfToolkit.Builder.Bcf30.Interfaces;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

public partial class OrthogonalCameraBuilder :
  IOrthogonalCameraBuilder<OrthogonalCameraBuilder, CameraBuilder>,
  IDefaultBuilder<OrthogonalCameraBuilder> {
  private readonly OrthogonalCamera _camera = new();

  public OrthogonalCameraBuilder SetCamera(Action<CameraBuilder> builder) {
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

  public OrthogonalCameraBuilder SetViewToWorldScale(double scale) {
    _camera.ViewToWorldScale = scale;
    return this;
  }
  
  public OrthogonalCameraBuilder SetAspectRatio(double ratio) {
    _camera.AspectRatio = ratio;
    return this;
  }

  public OrthogonalCameraBuilder WithDefaults() {
    this
      .SetCamera(cam => cam.WithDefaults())
      .SetViewToWorldScale(0.0)
      .SetAspectRatio(1.0);
    return this;
  }
  
  public OrthogonalCamera Build() {
    return BuilderUtils.ValidateItem(_camera);
  }
}