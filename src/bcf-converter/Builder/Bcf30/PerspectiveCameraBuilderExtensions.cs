namespace bcf.Builder.Bcf30;

public partial class PerspectiveCameraBuilder :
  IPerspectiveCameraBuilderExtension<PerspectiveCameraBuilder> {
  public PerspectiveCameraBuilder AddAspectRatio(double ratio) {
    _camera.AspectRatio = ratio;
    return this;
  }
}