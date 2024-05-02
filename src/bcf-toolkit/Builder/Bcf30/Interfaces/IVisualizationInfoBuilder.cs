using System;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30.Interfaces;

public interface IVisualizationInfoBuilder<
  out TBuilder,
  out TComponentBuilder,
  out TVisibilityBuilder,
  out TColorBuilder,
  out TOrthogonalCameraBuilder,
  out TPerspectiveCameraBuilder,
  out TLineBuilder,
  out TClippingPlaneBuilder,
  out TBitmapBuilder> :
  IBuilder<VisualizationInfo> {
  /// <summary>
  ///   Returns the builder object set with the `Guid`.
  /// </summary>
  /// <param name="guid">The GUID of the viewpoint.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetGuid(string guid);
  /// <summary>
  ///   Returns the builder object extended with a new `Selection`.
  /// </summary>
  /// <param name="builder">The builder of the selection.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddSelection(Action<TComponentBuilder> builder);
  /// <summary>
  ///   Returns the builder object set with the `Visibility`.
  /// </summary>
  /// <param name="builder">The builder of the visibility.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetVisibility(Action<TVisibilityBuilder> builder);
  /// <summary>
  ///   Returns the builder object extended with a new `Coloring`.
  /// </summary>
  /// <param name="builder">The builder of the coloring.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddColoring(Action<TColorBuilder> builder);
  /// <summary>
  ///   Returns the builder object set with the `OrthogonalCamera`.
  /// </summary>
  /// <param name="builder">The builder of the camera.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetOrthogonalCamera(Action<TOrthogonalCameraBuilder> builder);
  /// <summary>
  ///   Returns the builder object set with the `PerspectiveCamera`.
  /// </summary>
  /// <param name="builder">The builder of the camera.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetPerspectiveCamera(Action<TPerspectiveCameraBuilder> builder);
  /// <summary>
  ///   Returns the builder object extended with a new `Line`.
  /// </summary>
  /// <param name="builder">The builder of the line.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddLine(Action<TLineBuilder> builder);
  /// <summary>
  ///   Returns the builder object extended with a new `ClippingPlane`.
  /// </summary>
  /// <param name="builder">The builder of the clipping plane.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddClippingPlane(Action<TClippingPlaneBuilder> builder);
  /// <summary>
  ///   Returns the builder object extended with a new `Bitmap`.
  /// </summary>
  /// <param name="builder">The builder of the bitmap.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddBitmap(Action<TBitmapBuilder> builder);
}