using System;
using BcfConverter.Model;

namespace BcfConverter.Builder;

public interface IViewPointBuilder<
  out TBuilder,
  out TComponentBuilder,
  out TVisibilityBuilder,
  out TColorBuilder,
  out TOrthogonalCameraBuilder,
  out TPerspectiveCameraBuilder,
  out TLineBuilder,
  out TClippingPlaneBuilder,
  out TBitmapBuilder> :
  IBuilder<IVisualizationInfo> {
  /// <summary>
  ///   Returns the builder object extended with `Guid`.
  /// </summary>
  /// <param name="guid">The version id.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddGuid(string guid);
  TBuilder AddSelection(Action<TComponentBuilder> builder);
  TBuilder AddVisibility(Action<TVisibilityBuilder> builder);
  TBuilder AddColoring(Action<TColorBuilder> builder);
  TBuilder AddOrthogonalCamera(Action<TOrthogonalCameraBuilder> builder);
  TBuilder AddPerspectiveCamera(Action<TPerspectiveCameraBuilder> builder);
  TBuilder AddLine(Action<TLineBuilder> builder);
  TBuilder AddClippingPlane(Action<TClippingPlaneBuilder> builder);
  TBuilder AddBitmap(Action<TBitmapBuilder> builder);
}