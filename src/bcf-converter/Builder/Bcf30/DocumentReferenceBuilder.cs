
using BcfConverter.Model;
using BcfConverter.Model.Bcf30;

namespace BcfConverter.Builder.Bcf30;

public partial class DocumentReferenceBuilder :
  IDocumentReferenceBuilder<DocumentReferenceBuilder> {
  private readonly DocumentReference _documentReference = new();
  public DocumentReferenceBuilder AddGuid(string guid) {
    _documentReference.Guid = guid;
    return this;
  }

  public DocumentReferenceBuilder AddDescription(string description) {
    _documentReference.Description = description;
    return this;
  }

  public IDocReference Build() {
    return _documentReference;
  }
}