using System.Collections.Generic;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

public partial class DocumentInfoBuilder {
  public DocumentInfoBuilder AddDocuments(List<Document> documents) {
    documents.ForEach(_documentInfo.Documents.Add);
    return this;
  }
}