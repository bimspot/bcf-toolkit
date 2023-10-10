using System;
using BcfToolkit.Model;

namespace BcfToolkit.Builder;

public interface ICommentBuilder<out TBuilder> : IBuilder<IComment> {
  /// <summary>
  ///   Returns the builder object extended with `Date`.
  /// </summary>
  /// <param name="date">Date of the comment.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddDate(DateTime date);
  /// <summary>
  ///   Returns the builder object extended with `Author`.
  /// </summary>
  /// <param name="user">Comment author.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddAuthor(string user);
  /// <summary>
  ///   Returns the builder object extended with `Comment`.
  /// </summary>
  /// <param name="comment">The comment text.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddComment(string comment);
  /// <summary>
  ///   Returns the builder object extended with `ViewPoint`.
  /// </summary>
  /// <param name="guid">Back reference to the viewpoint GUID.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddViewPoint(string guid);
  /// <summary>
  ///   Returns the builder object extended with `ModifiedDate`.
  /// </summary>
  /// <param name="date">The date when comment was modified.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddModifiedDate(DateTime date);
  /// <summary>
  ///   Returns the builder object extended with `ModifiedAuthor`.
  /// </summary>
  /// <param name="user">The author who modified the comment.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddModifiedAuthor(string user);
}