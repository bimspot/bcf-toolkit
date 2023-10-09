using BcfConverter.Model;

namespace BcfConverter.Builder;

public interface IDocumentBuilder<out TBuilder> : IBuilder<IDocument> {
  /// <summary>
  ///   Returns the builder object extended with `Guid`.
  /// </summary>
  /// <param name="guid">The Guid of the document.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddGuid(string guid);
  /// <summary>
  ///   Returns the builder object extended with `FileName`.
  /// </summary>
  /// <param name="name">The name of the document.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddFileName(string name);
  /// <summary>
  ///   Returns the builder object extended with `Description`.
  /// </summary>
  /// <param name="description">The description of the document.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddDescription(string description);
}