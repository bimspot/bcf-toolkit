namespace bcf.Builder.Bcf30;

public interface IPerspectiveCameraBuilderExtension<out TBuilder> {
  TBuilder AddAspectRatio(double ratio);
}