using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Bcf21;

public partial class OrthogonalCameraBuilder {

  public OrthogonalCameraBuilder SetCameraViewPoint(double x, double y, double z) {
    _camera.CameraViewPoint = new Point {
      X = x,
      Y = y,
      Z = z
    };
    return this;
  }

  public OrthogonalCameraBuilder SetCameraDirection(double x, double y, double z) {
    _camera.CameraDirection = new Direction {
      X = x,
      Y = y,
      Z = z
    };
    return this;
  }

  public OrthogonalCameraBuilder SetCameraUpVector(double x, double y, double z) {
    _camera.CameraUpVector = new Direction {
      X = x,
      Y = y,
      Z = z
    };
    return this;
  }

}