using BcfConverter.Model;

namespace BcfConverter.Builder;

public interface IBitmapBuilder<out TBuilder> : IBuilder<IBitmap> {
  /// <summary>
  ///   Returns the builder object extended with `Format`.
  /// </summary>
  /// <param name="format">Format of the bitmap (PNG/JPG)</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddFormat(string format);
  /// <summary>
  ///   Returns the builder object extended with `Reference`.
  /// </summary>
  /// <param name="reference">Name of the bitmap file in the topic folder
  /// </param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddReference(string reference);
  /// <summary>
  ///   Returns the builder object extended with `Location` of the center of
  ///   the bitmap in world coordinates.
  /// </summary>
  /// <param name="x">X</param>
  /// <param name="y">Y</param>
  /// <param name="z">Z</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddLocation(double x, double y, double z);
  /// <summary>
  ///   Returns the builder object extended with `Normal` vector of the bitmap.
  /// </summary>
  /// <param name="x">X</param>
  /// <param name="y">Y</param>
  /// <param name="z">Z</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddNormal(double x, double y, double z);
  /// <summary>
  ///   Returns the builder object extended with `Up` vector of the bitmap.
  /// </summary>
  /// <param name="x">X</param>
  /// <param name="y">Y</param>
  /// <param name="z">Z</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddUp(double x, double y, double z);
  /// <summary>
  ///   Returns the builder object extended with `Height`.
  /// </summary>
  /// <param name="height">Format of the bitmap (PNG/JPG)</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddHeight(double height);
}