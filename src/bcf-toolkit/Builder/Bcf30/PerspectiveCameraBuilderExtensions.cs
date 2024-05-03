using BcfToolkit.Builder.Bcf30.Interfaces;

namespace BcfToolkit.Builder.Bcf30;

public partial class PerspectiveCameraBuilder :
  IPerspectiveCameraBuilderExtension<PerspectiveCameraBuilder> {
  public PerspectiveCameraBuilder SetAspectRatio(double ratio) {
    _camera.AspectRatio = ratio;
    return this;
  }
}