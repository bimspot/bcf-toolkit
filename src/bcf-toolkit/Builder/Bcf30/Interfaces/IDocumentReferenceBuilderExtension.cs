namespace BcfToolkit.Builder.Bcf30.Interfaces;

public interface IDocumentReferenceBuilderExtension<out TBuilder> {
  /// <summary>
  ///   Returns the builder object set with the `DocumentGuid`.
  /// </summary>
  /// <param name="guid">Guid of the referenced document.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetDocumentGuid(string guid);
  /// <summary>
  ///   Returns the builder object set with the `DocumentGuid`.
  /// </summary>
  /// <param name="url">Url of an external document.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetUrl(string url);
}