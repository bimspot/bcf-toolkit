using BcfToolkit.Model.Bcf30;
namespace BcfToolkit.Builder.Bcf30;

public partial class PerspectiveCameraBuilder {
  public PerspectiveCameraBuilder SetCameraViewPoint(double x, double y, double z) {
    _camera.CameraViewPoint = new Point {
      X = x,
      Y = y,
      Z = z
    };
    return this;
  }

  public PerspectiveCameraBuilder SetCameraDirection(double x, double y, double z) {
    _camera.CameraDirection = new Direction {
      X = x,
      Y = y,
      Z = z
    };
    return this;
  }

  public PerspectiveCameraBuilder SetCameraUpVector(double x, double y, double z) {
    _camera.CameraUpVector = new Direction {
      X = x,
      Y = y,
      Z = z
    };
    return this;
  }
}