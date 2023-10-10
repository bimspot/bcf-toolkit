using BcfToolkit.Model;

namespace BcfToolkit.Builder;

public interface IBimSnippetBuilder<out TBuilder> : IBuilder<IBimSnippet> {
  /// <summary>
  ///   Returns the builder object extended with `SnippetType`.
  /// </summary>
  /// <param name="type">Type of the Snippet.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddSnippetType(string type);
  /// <summary>
  ///   Returns the builder object extended with `IsExternal`.
  /// </summary>
  /// <param name="isExternal">Is the BimSnippet external or within the bcfzip.
  /// </param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddIsExternal(bool isExternal);
  /// <summary>
  ///   Returns the builder object extended with `Reference`.
  /// </summary>
  /// <param name="reference">URI to BimSnippet.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddReference(string reference);
  /// <summary>
  ///   Returns the builder object extended with `ReferenceSchema`.
  /// </summary>
  /// <param name="schema">URI to BimSnippetSchema (always external)</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddReferenceSchema(string schema);
}