using System;
using BcfToolkit.Builder.Bcf30.Interfaces;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

public partial class DocumentBuilder :
  IDocumentBuilder<DocumentBuilder>,
  IDefaultBuilder<DocumentBuilder> {
  private readonly Document _document = new() {
    DocumentData = new FileData()
  };

  public DocumentBuilder SetGuid(string guid) {
    _document.Guid = guid;
    return this;
  }

  public DocumentBuilder SetFileName(string name) {
    _document.Filename = name;
    return this;
  }

  public DocumentBuilder SetDescription(string description) {
    _document.Description = description;
    return this;
  }

  public DocumentBuilder WithDefaults() {
    this
      .SetFileName("Default file name")
      .SetGuid(Guid.NewGuid().ToString());
    return this;
  }

  public Document Build() {
    return BuilderUtils.ValidateItem(_document);
  }
}