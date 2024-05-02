using System;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30.Interfaces;

public interface
  IPerspectiveCameraBuilder<
    out TBuilder,
    out TCameraBuilder> :
    IBuilder<PerspectiveCamera> {
  /// <summary>
  ///   Returns the builder object set with the `CameraViewPoint`,
  ///   `CameraDirection` and `CameraUpVector`.
  /// </summary>
  /// <param name="builder">The builder for camera objects.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetCamera(Action<TCameraBuilder> builder);
  /// <summary>
  ///   Returns the builder object set with the `FieldOfView`.
  /// </summary>
  /// <param name="angle">Camera’s field of view angle in degrees.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetFieldOfView(double angle);
}