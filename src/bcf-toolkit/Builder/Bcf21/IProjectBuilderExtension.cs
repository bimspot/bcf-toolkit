namespace BcfToolkit.Builder.Bcf21;

public interface IProjectBuilderExtension<out TBuilder> {
  TBuilder AddExtensionSchema(string schema);
}