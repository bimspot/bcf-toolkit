using System;
using BcfToolkit.Model;

namespace BcfToolkit.Builder;

public interface
  IOrthogonalCameraBuilder<
    out TBuilder,
    out TCameraBuilder> :
    IBuilder<IOrthogonalCamera> {
  /// <summary>
  ///   Returns the builder object extended with `CameraViewPoint`,
  ///   `CameraDirection` and `CameraUpVector`.
  /// </summary>
  /// <param name="builder">The builder for camera objects.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddCamera(Action<TCameraBuilder> builder);
  /// <summary>
  ///   Returns the builder object extended with `ViewToWorldScale`.
  /// </summary>
  /// <param name="scale">Scaling from view to world.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddViewToWorldScale(double scale);
}