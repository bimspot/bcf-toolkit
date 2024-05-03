using BcfToolkit.Builder.Bcf30.Interfaces;

namespace BcfToolkit.Builder.Bcf30;

public partial class OrthogonalCameraBuilder :
  IOrthogonalCameraBuilderExtension<OrthogonalCameraBuilder> {
  public OrthogonalCameraBuilder SetAspectRatio(double ratio) {
    _camera.AspectRatio = ratio;
    return this;
  }
}