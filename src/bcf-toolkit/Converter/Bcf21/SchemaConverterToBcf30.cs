using System.Linq;
using BcfToolkit.Builder.Bcf30;

namespace BcfToolkit.Converter.Bcf21;

public static class SchemaConverterToBcf30 {
  
  public static Model.Bcf30.Bcf Convert(Model.Bcf21.Bcf from) {
    var builder = new BcfBuilder();
    return builder
      .AddMarkups(from.Markups.Select(ConvertMarkup).ToList(), true)
      .SetProject(p => p.WithDefaults()) // TODO
      .SetDocumentInfo(d => d.AddDocument(doc => doc.WithDefaults())) // TODO
      .Build();
  }

  private static Model.Bcf30.Markup ConvertMarkup(Model.Bcf21.Markup from) {
    var builder = new MarkupBuilder();
    var topic = from.Topic;
    return builder
      .AddHeaderFiles(from.Header.Select(ConvertHeaderFile).ToList())
      .AddReferenceLinks(topic.ReferenceLink.ToList())
      .SetTitle(topic.Title)
      .SetPriority(topic.Priority)
      .SetIndex(topic.Index)
      .AddLabels(topic.Labels.ToList())
      .SetCreationDate(topic.CreationDate)
      .SetCreationAuthor(topic.CreationAuthor)
      .SetModifiedDate(topic.ModifiedDate)
      .SetModifiedAuthor(topic.ModifiedAuthor)
      .SetDueDate(topic.DueDate)
      .SetAssignedTo(topic.AssignedTo)
      .SetStage(topic.Stage)
      .SetDescription(topic.Description)
      .SetBimSnippet(b => b
        .SetReference(topic.BimSnippet.Reference)
        .SetReferenceSchema(topic.BimSnippet.ReferenceSchema)
        .SetSnippetType(topic.BimSnippet.SnippetType)
        .SetIsExternal(topic.BimSnippet.IsExternal))
      //.AddDocumentReferences(from.Topic.DocumentReference.Select(ConvertDocumentReference).ToList()) TODO
      .AddRelatedTopics(topic.RelatedTopic.Select(t => t.Guid).ToList())
      .AddComments(from.Comment.Select(ConvertComment).ToList())
      .AddViewPoint(from.Viewpoints.S)
      .Build();
  }

  private static Model.Bcf30.File ConvertHeaderFile(Model.Bcf21.HeaderFile from) {
    var builder = new FileBuilder();
    return builder
      .SetFileName(from.Filename)
      .SetDate(from.Date)
      .SetReference(from.Reference)
      .SetIfcProject(from.IfcProject)
      .SetIfcSpatialStructureElement(from.IfcSpatialStructureElement)
      .SetIsExternal(from.IsExternal)
      .Build();
  }

  private static Model.Bcf30.DocumentReference ConvertDocumentReference(
    Model.Bcf21.TopicDocumentReference from) {
    //TODO DocumentGuid, Url
    var builder = new DocumentReferenceBuilder();
    return builder
      .SetDescription(from.Description)
      .SetGuid(from.Guid)
      .SetUrl(from.IsExternal ? from.ReferencedDocument : null)
      .Build();
  }

  private static Model.Bcf30.Comment ConvertComment(Model.Bcf21.Comment from) {
    var builder = new CommentBuilder();
    return builder
      .SetDate(from.Date)
      .SetAuthor(from.Author)
      .SetCommentProperty(from.CommentProperty)
      .SetViewPoint(from.Viewpoint.Guid)
      .SetModifiedDate(from.ModifiedDate)
      .SetModifiedAuthor(from.ModifiedAuthor)
      .Build();
  }

  private static Model.Bcf30.ViewPoint ConvertViewPoint(
    Model.Bcf21.ViewPoint from) {
    var builder = new VisualizationInfoBuilder();
    return builder
      .SetGuid(from.Guid)
      .SetSnapshot(from.Snapshot)
      .SetIndex(from.Index)
      .Build();
  }
  
}