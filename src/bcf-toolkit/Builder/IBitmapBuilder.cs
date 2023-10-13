using BcfToolkit.Model;

namespace BcfToolkit.Builder;

public interface IBitmapBuilder<out TBuilder> : IBuilder<IBitmap> {
  /// <summary>
  ///   Returns the builder object set with the `Format`.
  /// </summary>
  /// <param name="format">Format of the bitmap (PNG/JPG)</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetFormat(string format);
  /// <summary>
  ///   Returns the builder object set with the `Reference`.
  /// </summary>
  /// <param name="reference">
  ///   Name of the bitmap file in the topic folder.
  /// </param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetReference(string reference);
  /// <summary>
  ///   Returns the builder object set with the `Location` of the center of
  ///   the bitmap in world coordinates.
  /// </summary>
  /// <param name="x">X coordinate of the normal location.</param>
  /// <param name="y">Y coordinate of the normal location.</param>
  /// <param name="z">Z coordinate of the normal location.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetLocation(double x, double y, double z);
  /// <summary>
  ///   Returns the builder object set with the `Normal` vector of the bitmap.
  /// </summary>
  /// <param name="x">X coordinate of the normal vector.</param>
  /// <param name="y">Y coordinate of the normal vector.</param>
  /// <param name="z">Z coordinate of the normal vector.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetNormal(double x, double y, double z);
  /// <summary>
  ///   Returns the builder object set with the `Up` vector of the bitmap.
  /// </summary>
  /// <param name="x">X coordinate of the up vector.</param>
  /// <param name="y">Y coordinate of the up vector.</param>
  /// <param name="z">Z coordinate of the up vector.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetUp(double x, double y, double z);
  /// <summary>
  ///   Returns the builder object set with the `Height`.
  /// </summary>
  /// <param name="height">
  ///   The height of the bitmap in the model, in meters.
  /// </param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetHeight(double height);
}