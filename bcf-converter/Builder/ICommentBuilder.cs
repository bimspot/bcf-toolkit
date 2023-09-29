using System;

namespace bcf.Builder;

public interface ICommentBuilder : IBuilder<IComment> {
  ICommentBuilder AddDate(DateTime date);
  ICommentBuilder AddAuthor(string user);
  ICommentBuilder AddComment(string comment);
  ICommentBuilder AddViewPoint(string viewpoint);
  ICommentBuilder AddModifiedDate(DateTime modifiedDate);
  ICommentBuilder AddModifiedAuthor(string user);
}