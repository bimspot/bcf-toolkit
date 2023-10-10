using BcfToolkit.Model;

namespace BcfToolkit.Builder;

public interface IClippingPlaneBuilder<out TBuilder> :
  IBuilder<IClippingPlane> {
  /// <summary>
  ///   Returns the builder object extended with `Location`.
  /// </summary>
  /// <param name="x">X</param>
  /// <param name="y">Y</param>
  /// <param name="z">Z</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddLocation(double x, double y, double z);
  /// <summary>
  ///   Returns the builder object extended with `Direction`.
  /// </summary>
  /// <param name="x">X</param>
  /// <param name="y">Y</param>
  /// <param name="z">Z</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddDirection(double x, double y, double z);
}