using System;
using bcf.bcf21;

namespace bcf.Builder.Bcf21;

public class MarkupBuilder :
  IMarkupBuilder<
    MarkupBuilder,
    HeaderFileBuilder,
    BimSnippetBuilder,
    DocumentReferenceBuilder,
    CommentBuilder,
    ViewPointBuilder> {
  private readonly Markup _markup = new();

  public MarkupBuilder() {
    _markup.Topic = new Topic();
  }

  public MarkupBuilder AddGuid(string guid) {
    _markup.Topic.Guid = guid;
    return this;
  }

  public MarkupBuilder AddTopicType(string type) {
    _markup.Topic.TopicType = type;
    return this;
  }

  public MarkupBuilder AddTopicStatus(string status) {
    _markup.Topic.TopicStatus = status;
    return this;
  }

  public MarkupBuilder AddHeaderFile(Action<HeaderFileBuilder> builder) {
    var file =
      (HeaderFile)BuilderUtils.BuildItem<HeaderFileBuilder, IHeaderFile>(
        builder);
    _markup.Header.Add(file);
    return this;
  }

  public MarkupBuilder AddReferenceLink(string link) {
    _markup.Topic.ReferenceLink.Add(link);
    return this;
  }

  public MarkupBuilder AddTitle(string title) {
    _markup.Topic.Title = title;
    return this;
  }

  public MarkupBuilder AddPriority(string priority) {
    _markup.Topic.Priority = priority;
    return this;
  }

  public MarkupBuilder AddIndex(int inx) {
    _markup.Topic.Index = inx;
    return this;
  }

  public MarkupBuilder AddLabel(string label) {
    _markup.Topic.Labels.Add(label);
    return this;
  }

  public MarkupBuilder AddCreationDate(DateTime date) {
    _markup.Topic.CreationDate = date;
    return this;
  }

  public MarkupBuilder AddCreationAuthor(string user) {
    _markup.Topic.CreationAuthor = user;
    return this;
  }

  public MarkupBuilder AddModifiedDate(DateTime date) {
    _markup.Topic.ModifiedDate = date;
    return this;
  }

  public MarkupBuilder AddModifiedAuthor(string user) {
    _markup.Topic.ModifiedAuthor = user;
    return this;
  }

  public MarkupBuilder AddDueDate(DateTime date) {
    _markup.Topic.DueDate = date;
    return this;
  }

  public MarkupBuilder AddAssignedTo(string user) {
    _markup.Topic.AssignedTo = user;
    return this;
  }

  public MarkupBuilder AddDescription(string description) {
    _markup.Topic.Description = description;
    return this;
  }

  public MarkupBuilder AddStage(string stage) {
    _markup.Topic.Stage = stage;
    return this;
  }

  public MarkupBuilder AddBimSnippet(Action<BimSnippetBuilder> builder) {
    var bimSnippet =
      (BimSnippet)BuilderUtils.BuildItem<BimSnippetBuilder, IBimSnippet>(
        builder);
    _markup.Topic.BimSnippet = bimSnippet;
    return this;
  }

  public MarkupBuilder AddDocumentReference(
    Action<DocumentReferenceBuilder> builder) {
    var documentReference =
      (TopicDocumentReference)BuilderUtils
        .BuildItem<DocumentReferenceBuilder, IDocReference>(builder);
    _markup.Topic.DocumentReference.Add(documentReference);
    return this;
  }

  public MarkupBuilder AddComment(Action<CommentBuilder> builder) {
    var comment =
      (Comment)BuilderUtils.BuildItem<CommentBuilder, IComment>(builder);
    _markup.Comment.Add(comment);
    throw new NotImplementedException();
  }

  public MarkupBuilder AddViewPoint(Action<ViewPointBuilder> builder, string snapshotData) {
    var visInfo =
      (VisualizationInfo)BuilderUtils
        .BuildItem<ViewPointBuilder, IVisualizationInfo>(builder);
    var viewPoint = new ViewPoint {
      VisualizationInfo = visInfo,
      SnapshotData = snapshotData
    };
    _markup.Viewpoints.Add(viewPoint);
    return this;
  }

  public MarkupBuilder AddRelatedTopic(string relatedTopicGuid) {
    var relatedTopic = new TopicRelatedTopic {
      Guid = relatedTopicGuid
    };
    _markup.Topic.RelatedTopic.Add(relatedTopic);
    return this;
  }

  public IMarkup Build() {
    return _markup;
  }
}