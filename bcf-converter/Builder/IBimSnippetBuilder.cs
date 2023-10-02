namespace bcf.Builder;

public interface IBimSnippetBuilder<out TBuilder> : IBuilder<IBimSnippet> {
  TBuilder AddSnippetType(string type);
  TBuilder AddIsExternal(bool isExternal);
  TBuilder AddReference(string reference);
  TBuilder AddReferenceSchema(string schema);
}