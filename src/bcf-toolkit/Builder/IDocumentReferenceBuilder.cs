using BcfToolkit.Model;

namespace BcfToolkit.Builder;

public interface
  IDocumentReferenceBuilder<out TBuilder> : IBuilder<IDocReference> {
  /// <summary>
  ///   Returns the builder object extended with `Guid`.
  /// </summary>
  /// <param name="guid">Guid attribute for identifying the reference uniquely.
  /// </param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddGuid(string guid);
  /// <summary>
  ///   Returns the builder object extended with `Description`.
  /// </summary>
  /// <param name="description">Human readable description of the document
  /// reference.
  /// </param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddDescription(string description);
}