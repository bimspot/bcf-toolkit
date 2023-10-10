using System;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

public class DocumentInfoBuilder : IDocumentInfoBuilder<DocumentInfoBuilder, DocumentBuilder> {
  private readonly DocumentInfo _documentInfo = new();

  public DocumentInfoBuilder AddDocument(Action<DocumentBuilder> builder) {
    var document =
      (Document)BuilderUtils.BuildItem<DocumentBuilder, IDocument>(builder);
    _documentInfo.Documents.Add(document);
    return this;
  }

  public IDocumentInfo Build() {
    return BuilderUtils.ValidateItem(_documentInfo);
  }
}