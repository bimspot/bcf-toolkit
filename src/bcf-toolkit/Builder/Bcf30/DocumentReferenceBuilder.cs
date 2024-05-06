using System;
using BcfToolkit.Builder.Bcf30.Interfaces;
using BcfToolkit.Builder.Interfaces;
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

  public DocumentReferenceBuilder SetDocumentGuid(string? guid) {
    if (guid != null)
      _documentReference.DocumentGuid = guid;
    return this;
  }

  public DocumentReferenceBuilder SetUrl(string? url) {
    if (url != null)
      _documentReference.Url = url;
    return this;
  }

  public DocumentReferenceBuilder WithDefaults() {
    this.SetGuid(Guid.NewGuid().ToString());
    return this;
  }

  public DocumentReference Build() {
    return BuilderUtils.ValidateItem(_documentReference);
  }
}