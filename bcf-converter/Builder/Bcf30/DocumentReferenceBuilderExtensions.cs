namespace bcf.Builder.Bcf30;

public partial class DocumentReferenceBuilder : 
  IDocumentReferenceBuilderExtension<DocumentReferenceBuilder> {
  public DocumentReferenceBuilder AddDocumentGuid(string guid) {
    _documentReference.DocumentGuid = guid;
    return this;
  }

  public DocumentReferenceBuilder AddUrl(string url) {
    _documentReference.Url = url;
    return this;
  }
}