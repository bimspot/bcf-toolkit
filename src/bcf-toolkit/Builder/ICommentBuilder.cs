using System;
using BcfToolkit.Model;

namespace BcfToolkit.Builder;

public interface ICommentBuilder<out TBuilder> : IBuilder<IComment> {
  /// <summary>
  ///   Returns the builder object set with the `Guid`.
  /// </summary>
  /// <param name="guid">Guid of the comment.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetGuid(string guid);
  /// <summary>
  ///   Returns the builder object set with the `Date`.
  /// </summary>
  /// <param name="date">Date of the comment.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetDate(DateTime date);
  /// <summary>
  ///   Returns the builder object set with the `Author`.
  /// </summary>
  /// <param name="user">Comment author.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetAuthor(string user);
  /// <summary>
  ///   Returns the builder object set with the `Comment`.
  /// </summary>
  /// <param name="comment">The comment text.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetComment(string comment);
  /// <summary>
  ///   Returns the builder object set with the `ViewPoint`.
  /// </summary>
  /// <param name="guid">Back reference to the viewpoint GUID.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetViewPoint(string guid);
  /// <summary>
  ///   Returns the builder object set with the `ModifiedDate`.
  /// </summary>
  /// <param name="date">The date when comment was modified.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetModifiedDate(DateTime date);
  /// <summary>
  ///   Returns the builder object set with the `ModifiedAuthor`.
  /// </summary>
  /// <param name="user">The author who modified the comment.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetModifiedAuthor(string user);
}