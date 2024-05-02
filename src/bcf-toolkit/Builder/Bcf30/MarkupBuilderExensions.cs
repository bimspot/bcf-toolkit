using System.Collections.Generic;
using BcfToolkit.Builder.Bcf30.Interfaces;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

public partial class MarkupBuilder : IMarkupBuilderExtension<MarkupBuilder> {
  public MarkupBuilder SetServerAssignedId(string id) {
    _markup.Topic.ServerAssignedId = id;
    return this;
  }
  
  public MarkupBuilder AddDocumentReferences(
    List<DocumentReference> documentReferences) {
    documentReferences.ForEach(_markup.Topic.DocumentReferences.Add);
    return this;
  }
  
  public MarkupBuilder AddDocumentReferencesWithInfo(
    List<DocumentReference> documentReferences) {
    documentReferences.ForEach(documentReference => {
      _markup.Topic.DocumentReferences.Add(documentReference);
      
      
    });
    return this;
  }
}