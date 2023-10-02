namespace bcf.Builder.Bcf21; 

public interface IDocumentReferenceBuilderExtension<out TBuilder> {
  TBuilder AddIsExternal(bool isExternal);
  TBuilder AddReferencedDocument(string reference);
}