namespace bcf.Builder.Bcf21;

public interface IProjectBuilderExtension<out TBuilder> {
  TBuilder AddExtensionSchema(string schema);
}