using BcfConverter.Model;
using BcfConverter.Model.Bcf30;

namespace BcfConverter.Builder.Bcf30;

public class DocumentBuilder : IDocumentBuilder<DocumentBuilder> {
  private readonly Document _document = new();

  public DocumentBuilder AddGuid(string guid) {
    _document.Guid = guid;
    return this;
  }

  public DocumentBuilder AddFileName(string name) {
    _document.Filename = name;
    return this;
  }

  public DocumentBuilder AddDescription(string description) {
    _document.Description = description;
    return this;
  }

  public IDocument Build() {
    return BuilderUtils.ValidateItem(_document);
  }
}