namespace BcfToolkit.Builder.Bcf21.Interfaces;

public interface IDocumentReferenceBuilderExtension<out TBuilder> {
  /// <summary>
  ///   Returns the builder object set with the `IsExternal`.
  /// </summary>
  /// <param name="isExternal">
  ///   Is the Document external or within the bcfzip.
  /// </param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetIsExternal(bool isExternal);
  /// <summary>
  ///   Returns the builder object set with the `ReferencedDocument`.
  /// </summary>
  /// <param name="reference">URI to document.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetReferencedDocument(string reference);
}