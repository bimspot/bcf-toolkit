using bcf.bcf21;

namespace bcf.Builder.Bcf21;

public partial class DocumentReferenceBuilder : IDocumentReferenceBuilder<DocumentReferenceBuilder> {
  private readonly TopicDocumentReference _documentReference= new ();
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