using System;
using System.Collections.ObjectModel;

namespace bcf.Builder;

public interface IMarkupBuilder<out TBuilder, out THeaderFileBuilder,
  out TBimSnippetBuilder, out TDocumentReferenceBuilder, out TCommentBuilder,
  out TViewPointBuilder> : IBuilder<IMarkup> {
  TBuilder AddGuid(string guid);
  TBuilder AddTopicType(string type);
  TBuilder AddTopicStatus(string status);
  TBuilder AddHeaderFile(Action<THeaderFileBuilder> builder);
  TBuilder AddReferenceLink(string link);
  TBuilder AddTitle(string title);
  TBuilder AddPriority(string priority);
  TBuilder AddIndex(int inx);
  TBuilder AddLabel(string label);
  TBuilder AddCreationDate(DateTime date);
  TBuilder AddCreationAuthor(string user);
  TBuilder AddModifiedDate(DateTime date);
  TBuilder AddModifiedAuthor(string user);
  TBuilder AddDueDate(DateTime date);
  TBuilder AddAssignedTo(string user);
  TBuilder AddDescription(string description);
  TBuilder AddStage(string stage);
  TBuilder AddBimSnippet(Action<TBimSnippetBuilder> builder);

  TBuilder
    AddDocumentReference(Action<TDocumentReferenceBuilder> builder);

  TBuilder AddComment(Action<TCommentBuilder> builder);
  TBuilder AddViewPoint(Action<TViewPointBuilder> visInfoBuilder);
  TBuilder AddRelatedTopic(string relatedTopicGuid);
}