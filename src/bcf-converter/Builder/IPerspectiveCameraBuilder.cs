using System;
using BcfConverter.Model;

namespace BcfConverter.Builder;

public interface
  IPerspectiveCameraBuilder<
    out TBuilder,
    out TCameraBuilder> :
    IBuilder<IPerspectiveCamera> {
  TBuilder AddCamera(Action<TCameraBuilder> builder);
  TBuilder AddFieldOfView(double angle);
}