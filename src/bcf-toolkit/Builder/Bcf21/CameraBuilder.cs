using BcfToolkit.Builder.Bcf21.Interfaces;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Bcf21;

public class CameraBuilder : ICameraBuilder, IDefaultBuilder<CameraBuilder> {
  private readonly Direction _direction = new();
  private readonly Direction _upVector = new();
  private readonly Point _viewPoint = new();

  public ICameraBuilder SetViewPoint(double x, double y, double z) {
    _viewPoint.X = x;
    _viewPoint.Y = y;
    _viewPoint.Z = z;
    return this;
  }

  public ICameraBuilder SetDirection(double x, double y, double z) {
    _direction.X = x;
    _direction.Y = y;
    _direction.Z = z;
    return this;
  }

  public ICameraBuilder SetUpVector(double x, double y, double z) {
    _upVector.X = x;
    _upVector.Y = y;
    _upVector.Z = z;
    return this;
  }

  public CameraBuilder WithDefaults() {
    this
      .SetViewPoint(0.0, 0.0, 0.0)
      .SetDirection(1.0, 0.0, 0.0)
      .SetUpVector(0.0, 0.0, 1.0);
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