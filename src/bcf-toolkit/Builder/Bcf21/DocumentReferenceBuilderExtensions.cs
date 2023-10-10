namespace BcfToolkit.Builder.Bcf21;

public partial class DocumentReferenceBuilder :
  IDocumentReferenceBuilderExtension<DocumentReferenceBuilder> {
  public DocumentReferenceBuilder AddIsExternal(bool isExternal) {
    _documentReference.IsExternal = isExternal;
    return this;
  }

  public DocumentReferenceBuilder AddReferencedDocument(string reference) {
    _documentReference.ReferencedDocument = reference;
    return this;
  }
}