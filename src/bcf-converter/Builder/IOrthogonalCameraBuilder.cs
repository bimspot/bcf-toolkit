using System;
using BcfConverter.Model;

namespace BcfConverter.Builder;

public interface
  IOrthogonalCameraBuilder<
    out TBuilder,
    out TCameraBuilder> :
    IBuilder<IOrthogonalCamera> {
  TBuilder AddCamera(Action<TCameraBuilder> builder);
  TBuilder AddViewToWorldScale(double scale);
}