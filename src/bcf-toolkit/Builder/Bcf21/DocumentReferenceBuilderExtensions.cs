using BcfToolkit.Builder.Bcf21.Interfaces;

namespace BcfToolkit.Builder.Bcf21;

public partial class DocumentReferenceBuilder :
  IDocumentReferenceBuilderExtension<DocumentReferenceBuilder> {
  public DocumentReferenceBuilder SetIsExternal(bool isExternal) {
    _documentReference.IsExternal = isExternal;
    return this;
  }

  public DocumentReferenceBuilder SetReferencedDocument(string reference) {
    _documentReference.ReferencedDocument = reference;
    return this;
  }
}