namespace bcf.Builder.Bcf30; 

public interface IDocumentReferenceBuilderExtension<out TBuilder> {
  TBuilder AddDocumentGuid(string guid);
  TBuilder AddUrl(string url);
}