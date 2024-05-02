using BcfToolkit.Builder.Bcf30.Interfaces;

namespace BcfToolkit.Builder.Bcf30;

public partial class PerspectiveCameraBuilder :
  IPerspectiveCameraBuilderExtension<PerspectiveCameraBuilder> {
  public PerspectiveCameraBuilder AddAspectRatio(double ratio) {
    _camera.AspectRatio = ratio;
    return this;
  }
}