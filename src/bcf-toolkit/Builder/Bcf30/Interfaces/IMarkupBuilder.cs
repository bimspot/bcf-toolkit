using System;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30.Interfaces;

public interface IMarkupBuilder<
  out TBuilder,
  out THeaderFileBuilder,
  out TBimSnippetBuilder,
  out TDocumentReferenceBuilder,
  out TCommentBuilder,
  out TVisualizationInfoBuilder,
  out TViewPointBuilder> :
  IBuilder<Markup> {
  /// <summary>
  ///   Returns the builder object set with the `Guid`.
  /// </summary>
  /// <param name="guid">Guid of the topic, in lowercase.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetGuid(string guid);

  /// <summary>
  ///   Returns the builder object set with the `TopicType`.
  /// </summary>
  /// <param name="type">Type of the topic.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetTopicType(string type);

  /// <summary>
  ///   Returns the builder object set with the `TopicStatus`.
  /// </summary>
  /// <param name="status">Status of the topic.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetTopicStatus(string status);

  /// <summary>
  ///   Returns the builder object extended with `HeaderFile` in header.
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
  ///   Returns the builder object set with the `Title`.
  /// </summary>
  /// <param name="title">Title of the topic.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetTitle(string title);

  /// <summary>
  ///   Returns the builder object set with the `Priority`.
  /// </summary>
  /// <param name="priority">Topic priority .</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetPriority(string priority);

  /// <summary>
  ///   Returns the builder object set with the `Index`.
  /// </summary>
  /// <param name="index">Number to maintain the order of the topics.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetIndex(int? index);

  /// <summary>
  ///   Returns the builder object extended with `Label`.
  /// </summary>
  /// <param name="label">Tags for grouping Topics.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddLabel(string label);

  /// <summary>
  ///   Returns the builder object set with the `CreationDate`.
  /// </summary>
  /// <param name="date">Date when the topic was created..</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetCreationDate(DateTime date);

  /// <summary>
  ///   Returns the builder object set with the `CreationAuthor`.
  /// </summary>
  /// <param name="user">User who created the topic.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetCreationAuthor(string user);

  /// <summary>
  ///   Returns the builder object set with the `ModifiedDate`.
  /// </summary>
  /// <param name="date">Date when the topic was last modified.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetModifiedDate(DateTime? date);

  /// <summary>
  ///   Returns the builder object set with the `ModifiedAuthor`.
  /// </summary>
  /// <param name="user">User who modified the topic.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetModifiedAuthor(string user);

  /// <summary>
  ///   Returns the builder object set with the `DueDate`.
  /// </summary>
  /// <param name="date">
  ///   Date until when the topics issue needs to be resolved.
  /// </param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetDueDate(DateTime? date);

  /// <summary>
  ///   Returns the builder object set with the `AssignedTo`.
  /// </summary>
  /// <param name="user">The user to whom this topic is assigned to.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetAssignedTo(string user);

  /// <summary>
  ///   Returns the builder object set with the `Description`.
  /// </summary>
  /// <param name="description">Description of the topic.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetDescription(string description);

  /// <summary>
  ///   Returns the builder object set with the `Stage`.
  /// </summary>
  /// <param name="stage">Stage this topic is part of.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetStage(string stage);

  /// <summary>
  ///   Returns the builder object set with the `BimSnippet`.
  /// </summary>
  /// <param name="builder">The builder for `BimSnippet`.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetBimSnippet(Action<TBimSnippetBuilder> builder);

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
  ///   WARNING: This function is deprecated, please use `AddViewPoint` with
  ///   `ViewPointBuilder` argument instead.
  /// </summary>
  /// <param name="viewpoint">Viewpoint file name.</param>
  /// <param name="snapshot">Snapshot file name.</param>
  /// <param name="snapshotData">Base64 string of snapshot data.</param>
  /// <param name="index">Index parameter for sorting.</param>
  /// <param name="guid">Guid of the viewpoint.</param>
  /// <param name="builder">The builder for `VisualizationInfo`.</param>
  /// <returns>Returns the builder object.</returns>
  [Obsolete(
    "This function is deprecated, please use `AddViewPoint` with " +
    "`ViewPointBuilder` argument instead.")]
  TBuilder AddViewPoint(
    string viewpoint,
    string snapshot,
    FileData snapshotData,
    int index,
    string guid,
    Action<TVisualizationInfoBuilder> builder);

  /// <summary>
  ///   Returns the builder object extended with a new `ViewPoint`.
  ///   The markup file can contain multiple viewpoints related to one or
  ///   more comments. A viewpoint has also the Guid attribute for identifying
  ///   it uniquely.
  /// </summary>
  /// <param name="builder">The builder for `ViewPoint`.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddViewPoint(Action<TViewPointBuilder> builder);

  /// <summary>
  ///   Returns the builder object extended with `RelatedTopic`.
  /// </summary>
  /// <param name="relatedTopicGuid">The Guid of the related topic.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddRelatedTopic(string relatedTopicGuid);

  /// <summary>
  ///   Returns the builder object set with the `ServerAssignedId`.
  /// </summary>
  /// <param name="id">
  ///   A server controlled, user friendly and project-unique issue identifier.
  /// </param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetServerAssignedId(string id);
}