using System;
using BcfToolkit.Builder.Bcf30.Interfaces;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

public partial class DocumentInfoBuilder :
  IDocumentInfoBuilder<DocumentInfoBuilder, DocumentBuilder> {
  private readonly DocumentInfo _documentInfo = new();

  public DocumentInfoBuilder AddDocument(Action<DocumentBuilder> builder) {
    var document =
      (Document)BuilderUtils.BuildItem<DocumentBuilder, IDocument>(builder);
    _documentInfo.Documents.Add(document);
    return this;
  }

  public DocumentInfo Build() {
    return BuilderUtils.ValidateItem(_documentInfo);
  }
}