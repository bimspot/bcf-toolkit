using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Bcf21.Interfaces;

public interface
  IDocumentReferenceBuilder<out TBuilder> : IBuilder<TopicDocumentReference> {
  /// <summary>
  ///   Returns the builder object set with the `Guid`.
  /// </summary>
  /// <param name="guid">
  ///   Guid attribute for identifying the reference uniquely.
  /// </param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetGuid(string guid);
  /// <summary>
  ///   Returns the builder object set with the `Description`.
  /// </summary>
  /// <param name="description">
  ///   Human readable description of the document reference.
  /// </param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetDescription(string description);
}