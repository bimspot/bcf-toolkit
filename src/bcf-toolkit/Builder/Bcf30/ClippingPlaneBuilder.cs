using BcfToolkit.Builder.Bcf30.Interfaces;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

public class ClippingPlaneBuilder :
  IClippingPlaneBuilder<ClippingPlaneBuilder>,
  IDefaultBuilder<ClippingPlaneBuilder> {
  private readonly ClippingPlane _clippingPlane = new();

  public ClippingPlaneBuilder SetLocation(double x, double y, double z) {
    _clippingPlane.Location = new Point {
      X = x,
      Y = y,
      Z = z
    };
    return this;
  }

  public ClippingPlaneBuilder SetDirection(double x, double y, double z) {
    _clippingPlane.Direction = new Direction {
      X = x,
      Y = y,
      Z = z
    };
    return this;
  }

  public ClippingPlaneBuilder WithDefaults() {
    this
      .SetLocation(0.0, 0.0, 0.0)
      .SetDirection(1.0, 0.0, 0.0);
    return this;
  }

  public ClippingPlane Build() {
    return BuilderUtils.ValidateItem(_clippingPlane);
  }
}