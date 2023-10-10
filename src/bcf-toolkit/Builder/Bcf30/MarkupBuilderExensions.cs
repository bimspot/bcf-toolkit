namespace BcfToolkit.Builder.Bcf30;

public partial class MarkupBuilder : IMarkupBuilderExtension<MarkupBuilder> {
  public MarkupBuilder AddServerAssignedId(string id) {
    _markup.Topic.ServerAssignedId = id;
    return this;
  }
}