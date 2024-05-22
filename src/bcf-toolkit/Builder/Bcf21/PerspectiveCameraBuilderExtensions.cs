using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Bcf21;

public partial class PerspectiveCameraBuilder {
  public PerspectiveCameraBuilder SetCameraViewPoint(double x, double y, double z) {
    _camera.CameraViewPoint ??= new Point();
    _camera.CameraViewPoint.X = x;
    _camera.CameraViewPoint.Y = y;
    _camera.CameraViewPoint.Z = z;
    return this;
  }

  public PerspectiveCameraBuilder SetCameraDirection(double x, double y, double z) {
    _camera.CameraDirection ??= new Direction();
    _camera.CameraDirection.X = x;
    _camera.CameraDirection.Y = y;
    _camera.CameraDirection.Z = z;
    return this;
  }

  public PerspectiveCameraBuilder SetCameraUpVector(double x, double y, double z) {
    _camera.CameraUpVector ??= new Direction();
    _camera.CameraUpVector.X = x;
    _camera.CameraUpVector.Y = y;
    _camera.CameraUpVector.Z = z;
    return this;
  }

}