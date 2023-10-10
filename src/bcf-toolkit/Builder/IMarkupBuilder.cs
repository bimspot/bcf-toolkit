using System;
using BcfToolkit.Model;

namespace BcfToolkit.Builder;

public interface IMarkupBuilder<
  out TBuilder,
  out THeaderFileBuilder,
  out TBimSnippetBuilder,
  out TDocumentReferenceBuilder,
  out TCommentBuilder,
  out TViewPointBuilder> :
  IBuilder<IMarkup> {
  /// <summary>
  ///   Returns the builder object extended with `Guid`.
  /// </summary>
  /// <param name="guid">Guid of the topic, in lowercase.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddGuid(string guid);
  /// <summary>
  ///   Returns the builder object extended with `TopicType`.
  /// </summary>
  /// <param name="type">Type of the topic.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddTopicType(string type);
  /// <summary>
  ///   Returns the builder object extended with `TopicStatus`.
  /// </summary>
  /// <param name="status">Status of the topic.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddTopicStatus(string status);
  /// <summary>
  ///   Returns the builder object extended with new `HeaderFile` in header.
  /// </summary>
  /// <param name="builder">Builder of the header file.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddHeaderFile(Action<THeaderFileBuilder> builder);
  /// <summary>
  ///   Returns the builder object extended with `ReferenceLink`.
  /// </summary>
  /// <param name="link">List of references to the topic.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddReferenceLink(string link);
  /// <summary>
  ///   Returns the builder object extended with `Title`.
  /// </summary>
  /// <param name="title">Title of the topic.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddTitle(string title);
  /// <summary>
  ///   Returns the builder object extended with `Priority`.
  /// </summary>
  /// <param name="priority">Topic priority .</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddPriority(string priority);
  /// <summary>
  ///   Returns the builder object extended with `Index`.
  /// </summary>
  /// <param name="inx">Number to maintain the order of the topics.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddIndex(int inx);
  /// <summary>
  ///   Returns the builder object extended with `Label`.
  /// </summary>
  /// <param name="label">Tags for grouping Topics.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddLabel(string label);
  /// <summary>
  ///   Returns the builder object extended with `CreationDate`.
  /// </summary>
  /// <param name="date">Date when the topic was created..</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddCreationDate(DateTime date);
  /// <summary>
  ///   Returns the builder object extended with `CreationAuthor`.
  /// </summary>
  /// <param name="user">User who created the topic.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddCreationAuthor(string user);
  /// <summary>
  ///   Returns the builder object extended with `ModifiedDate`.
  /// </summary>
  /// <param name="date">Date when the topic was last modified.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddModifiedDate(DateTime date);
  /// <summary>
  ///   Returns the builder object extended with `ModifiedAuthor`.
  /// </summary>
  /// <param name="user">User who modified the topic.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddModifiedAuthor(string user);
  /// <summary>
  ///   Returns the builder object extended with `DueDate`.
  /// </summary>
  /// <param name="date">
  ///   Date until when the topics issue needs to be resolved.
  /// </param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddDueDate(DateTime date);
  /// <summary>
  ///   Returns the builder object extended with `AssignedTo`.
  /// </summary>
  /// <param name="user">The user to whom this topic is assigned to.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddAssignedTo(string user);
  /// <summary>
  ///   Returns the builder object extended with `Description`.
  /// </summary>
  /// <param name="description">Description of the topic.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddDescription(string description);
  /// <summary>
  ///   Returns the builder object extended with `Stage`.
  /// </summary>
  /// <param name="stage">Stage this topic is part of.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddStage(string stage);
  /// <summary>
  ///   Returns the builder object extended with `BimSnippet`.
  /// </summary>
  /// <param name="builder">The builder for `BimSnippet`.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddBimSnippet(Action<TBimSnippetBuilder> builder);
  /// <summary>
  ///   Returns the builder object extended with `DocumentReference`.
  /// </summary>
  /// <param name="builder">The builder for `DocumentReference`.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddDocumentReference(Action<TDocumentReferenceBuilder> builder);
  /// <summary>
  ///   Returns the builder object extended with `Comment`.
  /// </summary>
  /// <param name="builder">The builder for `Comment`.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddComment(Action<TCommentBuilder> builder);
  /// <summary>
  ///   Returns the builder object extended with `ViewPoint`.
  /// </summary>
  /// <param name="builder">The builder for `ViewPoint`.</param>
  /// <param name="snapshot">Snapshot data.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddViewPoint(
    Action<TViewPointBuilder> builder,
    string snapshot);
  /// <summary>
  ///   Returns the builder object extended with `RelatedTopic`.
  /// </summary>
  /// <param name="relatedTopicGuid">The Guid of the related topic.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddRelatedTopic(string relatedTopicGuid);
}