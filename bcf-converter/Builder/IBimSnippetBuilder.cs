namespace bcf.Builder;

public interface IBimSnippetBuilder : IBuilder<IBimSnippet> {
  IBimSnippetBuilder AddType(string type);
  IBimSnippetBuilder AddIsExternal(bool isExternal);
  IBimSnippetBuilder AddIsReference(string reference);
  IBimSnippetBuilder AddIsSchema(string schema);
}