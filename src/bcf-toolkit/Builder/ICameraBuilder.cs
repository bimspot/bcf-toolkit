namespace BcfToolkit.Builder;

public interface ICameraBuilder : IBuilder<object> {
  /// <summary>
  ///   Returns the builder object set with the `ViewPoint` of the camera.
  /// </summary>
  /// <param name="x">X</param>
  /// <param name="y">Y</param>
  /// <param name="z">Z</param>
  /// <returns>Returns the builder object.</returns>
  ICameraBuilder SetViewPoint(double x, double y, double z);
  /// <summary>
  ///   Returns the builder object set with the `Direction` of the camera.
  /// </summary>
  /// <param name="x">X</param>
  /// <param name="y">Y</param>
  /// <param name="z">Z</param>
  /// <returns>Returns the builder object.</returns>
  ICameraBuilder SetDirection(double x, double y, double z);
  /// <summary>
  ///   Returns the builder object set with the `UpVector` of the camera.
  /// </summary>
  /// <param name="x">X</param>
  /// <param name="y">Y</param>
  /// <param name="z">Z</param>
  /// <returns>Returns the builder object.</returns>
  ICameraBuilder SetUpVector(double x, double y, double z);
}