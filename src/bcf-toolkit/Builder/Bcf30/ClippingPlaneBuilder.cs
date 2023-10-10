using BcfToolkit.Model;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

public class ClippingPlaneBuilder :
  IClippingPlaneBuilder<ClippingPlaneBuilder> {
  private readonly ClippingPlane _clippingPlane = new();

  public ClippingPlaneBuilder AddLocation(double x, double y, double z) {
    _clippingPlane.Location.X = x;
    _clippingPlane.Location.Y = y;
    _clippingPlane.Location.Z = z;
    return this;
  }

  public ClippingPlaneBuilder AddDirection(double x, double y, double z) {
    _clippingPlane.Direction.X = x;
    _clippingPlane.Direction.Y = y;
    _clippingPlane.Direction.Z = z;
    return this;
  }

  public IClippingPlane Build() {
    return BuilderUtils.ValidateItem(_clippingPlane);
  }
}