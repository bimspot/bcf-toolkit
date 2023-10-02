namespace bcf.Builder;

public interface IDocumentReferenceBuilder<out TBuilder> : IBuilder<IDocReference> {
  TBuilder AddGuid(string guid);
  TBuilder AddDescription(string description);
}