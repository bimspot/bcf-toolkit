namespace BcfConverter.Builder.Bcf30;

public interface IOrthogonalCameraBuilderExtension<out TBuilder> {
  TBuilder AddAspectRatio(double ratio);
}