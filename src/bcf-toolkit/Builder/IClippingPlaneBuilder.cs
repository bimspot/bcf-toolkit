using BcfToolkit.Model;

namespace BcfToolkit.Builder;

public interface IClippingPlaneBuilder<out TBuilder> :
  IBuilder<IClippingPlane> {
  /// <summary>
  ///   Returns the builder object extended with `Location`.
  /// </summary>
  /// <param name="x">X coordinate of the location.</param>
  /// <param name="y">Y coordinate of the location.</param>
  /// <param name="z">Z coordinate of the location.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetLocation(double x, double y, double z);
  /// <summary>
  ///   Returns the builder object set with the `Direction`.
  /// </summary>
  /// <param name="x">X coordinate of the direction.</param>
  /// <param name="y">Y coordinate of the direction.</param>
  /// <param name="z">Z coordinate of the direction.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetDirection(double x, double y, double z);
}