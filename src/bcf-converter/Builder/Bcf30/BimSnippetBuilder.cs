
using BcfConverter.Model;
using BcfConverter.Model.Bcf30;

namespace BcfConverter.Builder.Bcf30;

public class BimSnippetBuilder : IBimSnippetBuilder<BimSnippetBuilder> {
  private readonly BimSnippet _snippet = new();
  public BimSnippetBuilder AddSnippetType(string type) {
    _snippet.SnippetType = type;
    return this;
  }

  public BimSnippetBuilder AddIsExternal(bool isExternal) {
    _snippet.IsExternal = isExternal;
    return this;
  }

  public BimSnippetBuilder AddReference(string reference) {
    _snippet.Reference = reference;
    return this;
  }

  public BimSnippetBuilder AddReferenceSchema(string schema) {
    _snippet.ReferenceSchema = schema;
    return this;
  }

  public IBimSnippet Build() {
    return BuilderUtils.ValidateItem(_snippet);
  }
}