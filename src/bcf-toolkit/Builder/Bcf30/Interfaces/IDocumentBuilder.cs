using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30.Interfaces;

public interface IDocumentBuilder<out TBuilder> : IBuilder<Document> {
  /// <summary>
  ///   Returns the builder object set with the `Guid`.
  /// </summary>
  /// <param name="guid">The Guid of the document.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetGuid(string guid);

  /// <summary>
  ///   Returns the builder object set with the `FileName`.
  /// </summary>
  /// <param name="name">The name of the document.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetFileName(string name);

  /// <summary>
  ///   Returns the builder object set with the `Description`.
  /// </summary>
  /// <param name="description">The description of the document.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetDescription(string description);
}