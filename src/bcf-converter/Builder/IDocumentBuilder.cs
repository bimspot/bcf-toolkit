namespace bcf.Builder;

public interface IDocumentBuilder<out TBuilder> : IBuilder<IDocument> {
  TBuilder AddGuid(string guid);
  TBuilder AddFileName(string name);
  TBuilder AddDescription(string description);
}