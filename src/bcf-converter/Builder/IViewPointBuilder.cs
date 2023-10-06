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
  TBuilder AddGuid(string guid);

  TBuilder AddSelection(Action<TComponentBuilder> builder);
  TBuilder AddVisibility(Action<TVisibilityBuilder> builder);
  TBuilder AddColoring(Action<TColorBuilder> builder);

  TBuilder AddOrthogonalCamera(Action<TOrthogonalCameraBuilder> builder);

  // TODO 3.0
  // TBuilder AddOrthogonalCamera(
  //   Action<ICameraBuilder> cameraBuilder,
  //   double viewToWorldScale, 
  //   double aspectRatio);
  TBuilder AddPerspectiveCamera(Action<TPerspectiveCameraBuilder> builder);

  // TODO 3.0
  // TBuilder AddPerspectiveCamera(
  //   Action<ICameraBuilder> cameraBuilder,
  //   double fieldOfView,
  //   doubel aspectRatio);
  TBuilder AddLine(Action<TLineBuilder> builder);
  TBuilder AddClippingPlane(Action<TClippingPlaneBuilder> builder);
  TBuilder AddBitmap(Action<TBitmapBuilder> builder);
}