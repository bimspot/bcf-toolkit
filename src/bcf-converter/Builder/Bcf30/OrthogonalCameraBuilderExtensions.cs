namespace BcfConverter.Builder.Bcf30;

public partial class OrthogonalCameraBuilder :
  IOrthogonalCameraBuilderExtension<OrthogonalCameraBuilder> {
  public OrthogonalCameraBuilder AddAspectRatio(double ratio) {
    _camera.AspectRatio = ratio;
    return this;
  }
}