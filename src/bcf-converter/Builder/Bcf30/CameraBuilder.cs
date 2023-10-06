using bcf.bcf30;

namespace bcf.Builder.Bcf30;

public class CameraBuilder : ICameraBuilder {
  private readonly Direction _direction = new();
  private readonly Direction _upVector = new();
  private readonly Point _viewPoint = new();

  public ICameraBuilder AddViewPoint(double x, double y, double z) {
    _viewPoint.X = x;
    _viewPoint.Y = y;
    _viewPoint.Z = z;
    return this;
  }

  public ICameraBuilder AddDirection(double x, double y, double z) {
    _direction.X = x;
    _direction.Y = y;
    _direction.Z = z;
    return this;
  }

  public ICameraBuilder AddUpVector(double x, double y, double z) {
    _upVector.X = x;
    _upVector.Y = y;
    _upVector.Z = z;
    return this;
  }

  public object Build() {
    return new {
      viewPoint = _viewPoint,
      direction = _direction,
      upVector = _upVector
    };
  }
}