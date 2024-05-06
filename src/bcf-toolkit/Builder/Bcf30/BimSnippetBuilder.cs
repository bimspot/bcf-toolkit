using BcfToolkit.Builder.Bcf30.Interfaces;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

public class BimSnippetBuilder :
  IBimSnippetBuilder<BimSnippetBuilder>,
  IDefaultBuilder<BimSnippetBuilder> {
  private readonly BimSnippet _snippet = new();

  public BimSnippetBuilder SetSnippetType(string type) {
    _snippet.SnippetType = type;
    return this;
  }

  public BimSnippetBuilder SetIsExternal(bool isExternal) {
    _snippet.IsExternal = isExternal;
    return this;
  }

  public BimSnippetBuilder SetReference(string reference) {
    _snippet.Reference = reference;
    return this;
  }

  public BimSnippetBuilder SetReferenceSchema(string schema) {
    _snippet.ReferenceSchema = schema;
    return this;
  }
  
  public BimSnippetBuilder WithDefaults() {
    this
      .SetSnippetType("JSON")
      .SetReference("https://.../default.json")
      .SetReferenceSchema("http://json-schema.org")
      .SetIsExternal(true);
    return this;
  }

  public BimSnippet Build() {
    return BuilderUtils.ValidateItem(_snippet);
  }
}