using System;
using BcfToolkit.Builder.Bcf30.Interfaces;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf30;
using BcfToolkit.Model.Interfaces;

namespace BcfToolkit.Builder.Bcf30;

public partial class MarkupBuilder :
  IMarkupBuilder<
    MarkupBuilder,
    FileBuilder,
    BimSnippetBuilder,
    DocumentReferenceBuilder,
    CommentBuilder,
    VisualizationInfoBuilder,
    ViewPointBuilder>,
  IDefaultBuilder<MarkupBuilder> {
  private readonly Markup _markup = new();

  public MarkupBuilder() {
    // this is a required field so it must be initialized
    _markup.Topic = new Topic();
  }

  public MarkupBuilder SetGuid(string guid) {
    _markup.Topic.Guid = guid;
    return this;
  }

  public MarkupBuilder SetTopicType(string type) {
    _markup.Topic.TopicType = type;
    return this;
  }

  public MarkupBuilder SetTopicStatus(string status) {
    _markup.Topic.TopicStatus = status;
    return this;
  }

  public MarkupBuilder AddHeaderFile(
    Action<FileBuilder> builder) {
    var file =
      (File)BuilderUtils.BuildItem<FileBuilder, IHeaderFile>(builder);
    _markup.GetHeaderInstance().Files.Add(file);
    return this;
  }

  public MarkupBuilder AddReferenceLink(string link) {
    _markup.Topic.ReferenceLinks.Add(link);
    return this;
  }

  public MarkupBuilder SetTitle(string title) {
    _markup.Topic.Title = title;
    return this;
  }

  public MarkupBuilder SetPriority(string priority) {
    _markup.Topic.Priority = priority;
    return this;
  }

  public MarkupBuilder SetIndex(int? index) {
    _markup.Topic.Index = index;
    return this;
  }

  public MarkupBuilder AddLabel(string label) {
    _markup.Topic.Labels.Add(label);
    return this;
  }

  public MarkupBuilder SetCreationDate(DateTime date) {
    _markup.Topic.CreationDate = date;
    return this;
  }

  public MarkupBuilder SetCreationAuthor(string user) {
    _markup.Topic.CreationAuthor = user;
    return this;
  }

  public MarkupBuilder SetModifiedDate(DateTime? date) {
    _markup.Topic.ModifiedDate = date;
    return this;
  }

  public MarkupBuilder SetModifiedAuthor(string user) {
    _markup.Topic.ModifiedAuthor = user;
    return this;
  }

  public MarkupBuilder SetDueDate(DateTime? date) {
    _markup.Topic.DueDate = date;
    return this;
  }

  public MarkupBuilder SetAssignedTo(string user) {
    _markup.Topic.AssignedTo = user;
    return this;
  }

  public MarkupBuilder SetDescription(string description) {
    _markup.Topic.Description = description;
    return this;
  }

  public MarkupBuilder SetStage(string stage) {
    _markup.Topic.Stage = stage;
    return this;
  }

  public MarkupBuilder SetBimSnippet(Action<BimSnippetBuilder> builder) {
    var bimSnippet =
      (BimSnippet)BuilderUtils.BuildItem<BimSnippetBuilder, IBimSnippet>(
        builder);
    _markup.Topic.BimSnippet = bimSnippet;
    return this;
  }

  public MarkupBuilder AddDocumentReference(
    Action<DocumentReferenceBuilder> builder) {
    var documentReference =
      (DocumentReference)BuilderUtils
        .BuildItem<DocumentReferenceBuilder, IDocReference>(builder);
    _markup.Topic.DocumentReferences.Add(documentReference);
    return this;
  }

  public MarkupBuilder AddComment(Action<CommentBuilder> builder) {
    var comment =
      (Comment)BuilderUtils.BuildItem<CommentBuilder, IComment>(builder);
    _markup.Topic.Comments.Add(comment);
    return this;
  }

  public MarkupBuilder AddViewPoint(
    string viewpoint,
    string snapshot,
    FileData snapshotData,
    int index,
    string guid,
    Action<VisualizationInfoBuilder> builder) {
    var visInfo =
      (VisualizationInfo)BuilderUtils
        .BuildItem<VisualizationInfoBuilder, IVisualizationInfo>(builder);
    var viewPoint = new ViewPoint {
      Viewpoint = viewpoint,
      Snapshot = snapshot,
      SnapshotData = snapshotData,
      Index = index,
      Guid = guid,
      VisualizationInfo = visInfo
    };
    _markup.Topic.Viewpoints.Add(viewPoint);
    return this;
  }

  public MarkupBuilder AddViewPoint(Action<ViewPointBuilder> builder) {
    var viewPoint =
      (ViewPoint)BuilderUtils
        .BuildItem<ViewPointBuilder, IViewPoint>(builder);
    _markup.Topic.Viewpoints.Add(viewPoint);
    return this;
  }

  public MarkupBuilder AddRelatedTopic(string relatedTopicGuid) {
    var topic = new TopicRelatedTopicsRelatedTopic {
      Guid = relatedTopicGuid
    };
    _markup.Topic.RelatedTopics.Add(topic);
    return this;
  }

  public MarkupBuilder SetServerAssignedId(string id) {
    _markup.Topic.ServerAssignedId = id;
    return this;
  }

  public MarkupBuilder WithDefaults() {
    this
      .SetTitle("Default title")
      .SetCreationDate(DateTime.Now)
      .SetCreationAuthor("Default user")
      .SetGuid(Guid.NewGuid().ToString())
      .SetTopicType("ERROR")
      .SetTopicStatus("OPEN");
    return this;
  }

  public Markup Build() {
    return BuilderUtils.ValidateItem(_markup);
  }
}