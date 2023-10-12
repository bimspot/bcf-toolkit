using System;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

public partial class DocumentReferenceBuilder :
  IDocumentReferenceBuilder<DocumentReferenceBuilder>,
  IDefaultBuilder<DocumentReferenceBuilder> {
  private readonly DocumentReference _documentReference = new();
  public DocumentReferenceBuilder SetGuid(string guid) {
    _documentReference.Guid = guid;
    return this;
  }

  public DocumentReferenceBuilder SetDescription(string description) {
    _documentReference.Description = description;
    return this;
  }
  
  public DocumentReferenceBuilder WithDefaults() {
    this
      .SetGuid(Guid.NewGuid().ToString());
    return this;
  }

  public IDocReference Build() {
    return BuilderUtils.ValidateItem(_documentReference);
  }
}