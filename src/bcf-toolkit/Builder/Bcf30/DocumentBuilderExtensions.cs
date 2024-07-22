using BcfToolkit.Model;

namespace BcfToolkit.Builder.Bcf30;

public partial class DocumentBuilder {
  public DocumentBuilder SetDocumentData(FileData data) {
    _document.DocumentData = data;
    return this;
  }
}