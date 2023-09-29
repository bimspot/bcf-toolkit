using System;

namespace bcf.Builder;

public interface IMarkupBuilder : IBuilder<IMarkup> {
  IMarkupBuilder AddHeaderFile(Action<IHeaderFileBuilder> builder);
  IMarkupBuilder AddReferenceLink(string link);
  IMarkupBuilder AddTitle(string title);
  IMarkupBuilder AddPriority(string priority);
  IMarkupBuilder AddIndex(int inx);
  IMarkupBuilder AddLabel(string label);
  IMarkupBuilder AddCreationDate(DateTime date);
  IMarkupBuilder AddCreationAuthor(string user);
  IMarkupBuilder AddModifiedDate(DateTime date);
  IMarkupBuilder AddModifiedAuthor(string user);
  IMarkupBuilder AddDueDate(DateTime date);
  IMarkupBuilder AddAssignedTo(string user);
  IMarkupBuilder AddDescription(string description);
  IMarkupBuilder AddStage(string stage);
  IMarkupBuilder AddBimSnippet(Action<IBimSnippetBuilder> builder);

  IMarkupBuilder
    AddDocumentReference(Action<IDocumentReferenceBuilder> builder);

  IMarkupBuilder AddComment(Action<ICommentBuilder> builder);
  IMarkupBuilder AddViewPoint(Action<IViewPointBuilder> visInfoBuilder);
}