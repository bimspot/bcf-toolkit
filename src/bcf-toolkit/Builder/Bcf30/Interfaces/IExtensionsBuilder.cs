using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30.Interfaces;

public interface IExtensionsBuilder<out TBuilder> : IBuilder<Extensions> {
  /// <summary>
  ///   Returns the builder object extended with a new `TopicType`.
  /// </summary>
  /// <param name="type">Type of the topic.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddTopicType(string type);
  /// <summary>
  ///   Returns the builder object extended with a new `TopicStatus`.
  /// </summary>
  /// <param name="status">Status of the topic.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddTopicStatus(string status);
  /// <summary>
  ///   Returns the builder object extended with a new `TopicPriority`.
  /// </summary>
  /// <param name="priority">Priority of the topic.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddPriority(string priority);
  /// <summary>
  ///   Returns the builder object extended with a new `TopicLabel`.
  /// </summary>
  /// <param name="label">Tag for grouping Topics.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddTopicLabel(string label);
  /// <summary>
  ///   Returns the builder object extended with a new `User`.
  /// </summary>
  /// <param name="user">User of the topic.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddUser(string user);
  /// <summary>
  ///   Returns the builder object extended with a new `SnippetType`.
  /// </summary>
  /// <param name="type">Snippet type.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddSnippetType(string type);
  /// <summary>
  ///   Returns the builder object extended with `Stage`.
  /// </summary>
  /// <param name="stage">Stage of the topic.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddStage(string stage);
}