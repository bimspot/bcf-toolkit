using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30.Interfaces;

public interface
  IDocumentReferenceBuilder<out TBuilder> : IBuilder<DocumentReference> {
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