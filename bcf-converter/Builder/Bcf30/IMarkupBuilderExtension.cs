namespace bcf.Builder.Bcf30; 

public interface IMarkupBuilderExtension<out TBuilder> {
  TBuilder AddServerAssignedId(string id);
}