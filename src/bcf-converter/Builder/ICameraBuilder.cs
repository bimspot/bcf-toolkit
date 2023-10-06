namespace BcfConverter.Builder;

public interface ICameraBuilder : IBuilder<object> {
  /// <summary>
  ///   Returns the builder object extended with `ViewPoint` of the camera.
  /// </summary>
  /// <param name="x">X</param>
  /// <param name="y">Y</param>
  /// <param name="z">Z</param>
  /// <returns>Returns the builder object.</returns>
  ICameraBuilder AddViewPoint(double x, double y, double z);
  /// <summary>
  ///   Returns the builder object extended with `Direction` of the camera.
  /// </summary>
  /// <param name="x">X</param>
  /// <param name="y">Y</param>
  /// <param name="z">Z</param>
  /// <returns>Returns the builder object.</returns>
  ICameraBuilder AddDirection(double x, double y, double z);
  /// <summary>
  ///   Returns the builder object extended with `UpVector` of the camera.
  /// </summary>
  /// <param name="x">X</param>
  /// <param name="y">Y</param>
  /// <param name="z">Z</param>
  /// <returns>Returns the builder object.</returns>
  ICameraBuilder AddUpVector(double x, double y, double z);
}