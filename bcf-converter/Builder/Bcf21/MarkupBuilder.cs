using System;
using bcf.bcf21;

namespace bcf.Builder.Bcf21;

public class MarkupBuilder : IMarkupBuilder {
  private readonly Markup _markup = new();

  public MarkupBuilder() {
    _markup.Topic = new Topic();
  }

  public IMarkupBuilder AddHeaderFile(Action<IHeaderFileBuilder> builder) {
    var file =
      (HeaderFile)BuilderUtils.BuildItem<HeaderFileBuilder, IHeaderFile>(
        builder);
    _markup.Header.Add(file);
    return this;
  }

  public IMarkupBuilder AddReferenceLink(string link) {
    _markup.Topic.ReferenceLink.Add(link);
    return this;
  }

  IMarkupBuilder IMarkupBuilder.AddTitle(string title) {
    _markup.Topic.Title = title;
    return this;
  }

  public IMarkupBuilder AddPriority(string priority) {
    _markup.Topic.Priority = priority;
    return this;
  }

  public IMarkupBuilder AddIndex(int inx) {
    _markup.Topic.Index = inx;
    return this;
  }

  public IMarkupBuilder AddLabel(string label) {
    _markup.Topic.Labels.Add(label);
    return this;
  }

  public IMarkupBuilder AddCreationDate(DateTime date) {
    _markup.Topic.CreationDate = date;
    return this;
  }

  public IMarkupBuilder AddCreationAuthor(string user) {
    _markup.Topic.CreationAuthor = user;
    return this;
  }

  public IMarkupBuilder AddModifiedDate(DateTime date) {
    _markup.Topic.ModifiedDate = date;
    return this;
  }

  public IMarkupBuilder AddModifiedAuthor(string user) {
    _markup.Topic.ModifiedAuthor = user;
    return this;
  }

  public IMarkupBuilder AddDueDate(DateTime date) {
    _markup.Topic.DueDate = date;
    return this;
  }

  public IMarkupBuilder AddAssignedTo(string user) {
    _markup.Topic.AssignedTo = user;
    return this;
  }

  public IMarkupBuilder AddDescription(string description) {
    _markup.Topic.Description = description;
    return this;
  }

  public IMarkupBuilder AddStage(string stage) {
    _markup.Topic.Stage = stage;
    return this;
  }

  public IMarkupBuilder AddBimSnippet(Action<IBimSnippetBuilder> builder) {
    var bimSnippet =
      (BimSnippet)BuilderUtils.BuildItem<BimSnippetBuilder, IBimSnippet>(
        builder);
    _markup.Topic.BimSnippet = bimSnippet;
    return this;
  }

  public IMarkupBuilder AddDocumentReference(
    Action<IDocumentReferenceBuilder> builder) {
    var documentReference =
      (TopicDocumentReference)BuilderUtils
        .BuildItem<DocumentReferenceBuilder, IDocReference>(builder);
    _markup.Topic.DocumentReference.Add(documentReference);
    return this;
  }

  public IMarkupBuilder AddComment(Action<ICommentBuilder> builder) {
    var comment =
      (Comment)BuilderUtils.BuildItem<CommentBuilder, IComment>(builder);
    _markup.Comment.Add(comment);
    throw new NotImplementedException();
  }

  public IMarkupBuilder AddViewPoint(Action<IViewPointBuilder> builder) {
    var visInfo =
      (VisualizationInfo)BuilderUtils
        .BuildItem<ViewPointBuilder, IVisualizationInfo>(builder);
    var viewPoint = new ViewPoint {
      VisualizationInfo = visInfo
    };
    _markup.Viewpoints.Add(viewPoint);
    return this;
  }

  public IMarkup Build() {
    return _markup;
  }
}