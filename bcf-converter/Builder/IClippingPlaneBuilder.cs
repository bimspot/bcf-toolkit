namespace bcf.Builder;

public interface IClippingPlaneBuilder<out TBuilder> : 
  IBuilder<IClippingPlane> {
  TBuilder AddLocation(double x, double y, double z);
  TBuilder AddDirection(double x, double y, double z);
}