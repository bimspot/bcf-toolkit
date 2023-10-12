using BcfToolkit.Model;

namespace BcfToolkit.Builder;

public interface IBimSnippetBuilder<out TBuilder> : IBuilder<IBimSnippet> {
  /// <summary>
  ///   Returns the builder object set with the `SnippetType`.
  /// </summary>
  /// <param name="type">Type of the Snippet.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetSnippetType(string type);
  /// <summary>
  ///   Returns the builder object set with the `IsExternal`.
  /// </summary>
  /// <param name="isExternal">
  ///   Is the BimSnippet external or within the bcfzip.
  /// </param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetIsExternal(bool isExternal);
  /// <summary>
  ///   Returns the builder object set with the `Reference`.
  /// </summary>
  /// <param name="reference">URI to BimSnippet.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetReference(string reference);
  /// <summary>
  ///   Returns the builder object set with the `ReferenceSchema`.
  /// </summary>
  /// <param name="schema">URI to BimSnippetSchema (always external).</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetReferenceSchema(string schema);
}