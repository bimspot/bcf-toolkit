namespace BcfToolkit.Builder.Bcf21.Interfaces;

public interface IProjectBuilderExtension<out TBuilder> {
  TBuilder AddExtensionSchema(string schema);
}