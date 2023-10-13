namespace BcfToolkit.Builder.Bcf30;

public partial class DocumentReferenceBuilder :
  IDocumentReferenceBuilderExtension<DocumentReferenceBuilder> {
  public DocumentReferenceBuilder SetDocumentGuid(string guid) {
    _documentReference.DocumentGuid = guid;
    return this;
  }

  public DocumentReferenceBuilder SetUrl(string url) {
    _documentReference.Url = url;
    return this;
  }
}