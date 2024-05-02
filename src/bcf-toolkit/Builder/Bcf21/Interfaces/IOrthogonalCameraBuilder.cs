using System;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Bcf21.Interfaces;

public interface
  IOrthogonalCameraBuilder<
    out TBuilder,
    out TCameraBuilder> :
    IBuilder<OrthogonalCamera> {
  /// <summary>
  ///   Returns the builder object extended with `CameraViewPoint`,
  ///   `CameraDirection` and `CameraUpVector`.
  /// </summary>
  /// <param name="builder">The builder for camera objects.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetCamera(Action<TCameraBuilder> builder);
  /// <summary>
  ///   Returns the builder object extended with `ViewToWorldScale`.
  /// </summary>
  /// <param name="scale">Scaling from view to world.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetViewToWorldScale(double scale);
}