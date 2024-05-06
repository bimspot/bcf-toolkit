using BcfToolkit.Builder.Bcf21.Interfaces;
using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Bcf21;

public class DocumentReferenceBuilder :
  IDocumentReferenceBuilder<DocumentReferenceBuilder> {
  private readonly TopicDocumentReference _documentReference = new();

  public DocumentReferenceBuilder SetGuid(string guid) {
    _documentReference.Guid = guid;
    return this;
  }

  public DocumentReferenceBuilder SetDescription(string description) {
    _documentReference.Description = description;
    return this;
  }
  
  public DocumentReferenceBuilder SetIsExternal(bool isExternal) {
    _documentReference.IsExternal = isExternal;
    return this;
  }

  public DocumentReferenceBuilder SetReferencedDocument(string reference) {
    _documentReference.ReferencedDocument = reference;
    return this;
  }

  public TopicDocumentReference Build() {
    return BuilderUtils.ValidateItem(_documentReference);
  }
}