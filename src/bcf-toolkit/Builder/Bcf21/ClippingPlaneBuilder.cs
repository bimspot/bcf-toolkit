using BcfToolkit.Builder.Bcf21.Interfaces;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Bcf21;

public class ClippingPlaneBuilder :
  IClippingPlaneBuilder<ClippingPlaneBuilder>,
  IDefaultBuilder<ClippingPlaneBuilder> {
  private readonly ClippingPlane _clippingPlane = new();

  public ClippingPlaneBuilder SetLocation(double x, double y, double z) {
    _clippingPlane.Location.X = x;
    _clippingPlane.Location.Y = y;
    _clippingPlane.Location.Z = z;
    return this;
  }

  public ClippingPlaneBuilder SetDirection(double x, double y, double z) {
    _clippingPlane.Direction.X = x;
    _clippingPlane.Direction.Y = y;
    _clippingPlane.Direction.Z = z;
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