namespace BcfToolkit.Builder.Bcf30.Interfaces;

public interface IMarkupBuilderExtension<out TBuilder> {
  /// <summary>
  ///   Returns the builder object set with the `ServerAssignedId`.
  /// </summary>
  /// <param name="id">
  ///   A server controlled, user friendly and project-unique issue identifier.
  /// </param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetServerAssignedId(string id);
}