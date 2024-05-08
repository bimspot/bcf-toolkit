using BcfToolkit.Builder.Interfaces;

namespace BcfToolkit.Builder.Bcf21.Interfaces;

public interface ICameraBuilder : IBuilder<object> {
  /// <summary>
  ///   Returns the builder object set with the `ViewPoint` of the camera.
  /// </summary>
  /// <param name="x">X coordinate of the camera viewpoint.</param>
  /// <param name="y">Y coordinate of the camera viewpoint.</param>
  /// <param name="z">Z coordinate of the camera viewpoint.</param>
  /// <returns>Returns the builder object.</returns>
  ICameraBuilder SetViewPoint(double x, double y, double z);

  /// <summary>
  ///   Returns the builder object set with the `Direction` of the camera.
  /// </summary>
  /// <param name="x">X coordinate of the camera direction.</param>
  /// <param name="y">Y coordinate of the camera direction.</param>
  /// <param name="z">Z coordinate of the camera direction.</param>
  /// <returns>Returns the builder object.</returns>
  ICameraBuilder SetDirection(double x, double y, double z);

  /// <summary>
  ///   Returns the builder object set with the `UpVector` of the camera.
  /// </summary>
  /// <param name="x">X coordinate of the camera up vector.</param>
  /// <param name="y">Y coordinate of the camera up vector.</param>
  /// <param name="z">Z coordinate of the camera up vector.</param>
  /// <returns>Returns the builder object.</returns>
  ICameraBuilder SetUpVector(double x, double y, double z);
}