using System;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

public class CommentBuilder :
  ICommentBuilder<CommentBuilder>,
  IDefaultBuilder<CommentBuilder> {
  private readonly Comment _comment = new();

  public CommentBuilder SetGuid(string guid) {
    _comment.Guid = guid;
    return this;
  }

  public CommentBuilder SetDate(DateTime date) {
    _comment.Date = date;
    return this;
  }

  public CommentBuilder SetAuthor(string user) {
    _comment.Author = user;
    return this;
  }

  public CommentBuilder SetComment(string comment) {
    _comment.CommentProperty = comment;
    return this;
  }

  public CommentBuilder SetViewPoint(string guid) {
    _comment.Viewpoint.Guid = guid;
    return this;
  }

  public CommentBuilder SetModifiedDate(DateTime date) {
    _comment.ModifiedDate = date;
    return this;
  }

  public CommentBuilder SetModifiedAuthor(string user) {
    _comment.ModifiedAuthor = user;
    return this;
  }

  public CommentBuilder WithDefaults() {
    this
      .SetDate(DateTime.Now)
      .SetAuthor("Default user")
      .SetComment("Default comment.")
      .SetGuid(Guid.NewGuid().ToString());
    return this;
  }

  public IComment Build() {
    return BuilderUtils.ValidateItem(_comment);
  }
}