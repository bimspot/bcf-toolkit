using System;

namespace bcf.Builder;

public interface ICommentBuilder<out TBuilder> : IBuilder<IComment> {
  TBuilder AddDate(DateTime date);
  TBuilder AddAuthor(string user);
  TBuilder AddComment(string comment);
  TBuilder AddViewPoint(string guid);
  TBuilder AddModifiedDate(DateTime date);
  TBuilder AddModifiedAuthor(string user);
}