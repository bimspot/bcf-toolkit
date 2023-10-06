namespace BcfConverter.Builder;

public interface ICameraBuilder : IBuilder<object> {
  ICameraBuilder AddViewPoint(double x, double y, double z);
  ICameraBuilder AddDirection(double x, double y, double z);
  ICameraBuilder AddUpVector(double x, double y, double z);
}