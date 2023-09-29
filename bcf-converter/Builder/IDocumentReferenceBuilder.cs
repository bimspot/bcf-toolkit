namespace bcf.Builder;

public interface IDocumentReferenceBuilder : IBuilder<IDocReference> {
  IDocumentReferenceBuilder AddGuid(string guid);
  IDocumentReferenceBuilder AddIsExternal(bool isExternal);
  IDocumentReferenceBuilder AddReference(string reference);
  IDocumentReferenceBuilder AddDescription(string description);
}