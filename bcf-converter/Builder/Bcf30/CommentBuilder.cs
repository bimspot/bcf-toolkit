using System;
using bcf.bcf30;

namespace bcf.Builder.Bcf30;

public class CommentBuilder : ICommentBuilder<CommentBuilder> {
  private readonly Comment _comment = new();
  public CommentBuilder AddDate(DateTime date) {
    _comment.Date = date;
    return this;
  }

  public CommentBuilder AddAuthor(string user) {
    _comment.Author = user;
    return this;
  }

  public CommentBuilder AddComment(string comment) {
    _comment.CommentProperty = comment;
    return this;
  }

  public CommentBuilder AddViewPoint(string guid) {
    _comment.Viewpoint.Guid = guid;
    return this;
  }

  public CommentBuilder AddModifiedDate(DateTime date) {
    _comment.ModifiedDate = date;
    return this;
  }

  public CommentBuilder AddModifiedAuthor(string user) {
    _comment.ModifiedAuthor = user;
    return this;
  }

  public IComment Build() {
    return _comment;
  }
}