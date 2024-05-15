using System;
using System.Linq;
using BcfToolkit.Builder.Bcf21;
using BcfToolkit.Builder.Bcf30;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf21;
using BcfBuilder = BcfToolkit.Builder.Bcf21.BcfBuilder;
using BimSnippetBuilder = BcfToolkit.Builder.Bcf21.BimSnippetBuilder;
using CommentBuilder = BcfToolkit.Builder.Bcf21.CommentBuilder;
using DocumentReferenceBuilder = BcfToolkit.Builder.Bcf21.DocumentReferenceBuilder;
using MarkupBuilder = BcfToolkit.Builder.Bcf21.MarkupBuilder;
using VisualizationInfoBuilder = BcfToolkit.Builder.Bcf21.VisualizationInfoBuilder;

namespace BcfToolkit.Converter.Bcf30;

public static class SchemaConverterToBcf21 {

  public static Model.Bcf21.Bcf Convert(Model.Bcf30.Bcf from) {
    var builder = new BcfBuilder();
    builder
      .AddMarkups(from.Markups.Select(ConvertMarkup).ToList());
    
    
    return builder.Build();
  }
  
  private static Model.Bcf21.Markup ConvertMarkup(Model.Bcf30.Markup from) {
    var builder = new MarkupBuilder();
    builder
      .SetGuid(from.Topic.Guid)
      .SetTopicType(from.Topic.TopicType)
      .SetTopicStatus(from.Topic.TopicStatus)
      .AddHeaderFiles(from.Header.Files.Select(ConvertHeaderFile).ToList())
      .AddReferenceLinks(from.Topic.ReferenceLinks.ToList())
      .SetTitle(from.Topic.Title)
      .SetPriority(from.Topic.Priority)
      .SetIndex(from.Topic.Index)
      .AddLabels(from.Topic.Labels.ToList())
      .SetCreationDate(from.Topic.CreationDate)
      .SetCreationAuthor(from.Topic.CreationAuthor)
      .SetModifiedDate(from.Topic.ModifiedDate)
      .SetModifiedAuthor(from.Topic.ModifiedAuthor)
      .SetDueDate(from.Topic.DueDate)
      .SetAssignedTo(from.Topic.AssignedTo)
      .SetDescription(from.Topic.Description)
      .SetStage(from.Topic.Stage)
      .SetBimSnippet(ConvertBimSnippet(from.Topic.BimSnippet))
      .AddDocumentReferences(from.Topic.DocumentReferences
        .Select(ConvertDocumentReference).ToList())
      .AddComments(from.Topic.Comments.Select(ConvertComment).ToList())
      .AddViewPoints(from.Topic.Viewpoints.Select(ConvertViewPoint).ToList())
      .AddRelatedTopics(from.Topic.RelatedTopics.Select(ConvertRelatedTopic).ToList());
      
    
    return builder.Build();
  }
  
  private static Model.Bcf21.HeaderFile ConvertHeaderFile(Model.Bcf30.File from) {
    var builder = new HeaderFileBuilder();
    builder
      .SetIfcProject(from.IfcProject)
      .SetIfcSpatialStructureElement(from.IfcSpatialStructureElement)
      .SetIsExternal(from.IsExternal)
      .SetFileName(from.Filename)
      .SetDate(from.Date)
      .SetReference(from.Reference);
    return builder.Build();
  }
  
  private static Model.Bcf21.BimSnippet ConvertBimSnippet(Model.Bcf30.BimSnippet from) {
    var builder = new BimSnippetBuilder();
    builder
      .SetSnippetType(from.SnippetType)
      .SetIsExternal(from.IsExternal)
      .SetReference(from.Reference)
      .SetReferenceSchema(from.ReferenceSchema);
    
    return builder.Build();
  }
  
  private static Model.Bcf21.TopicDocumentReference ConvertDocumentReference(
    Model.Bcf30.DocumentReference from) {
    var builder = new DocumentReferenceBuilder();
    var isExternal = string.IsNullOrEmpty(from.Url);
    builder
      .SetGuid(from.Guid)
      .SetDescription(from.Description)
      .SetIsExternal(isExternal)
      .SetReferencedDocument(isExternal ? from.Url : from.DocumentGuid);
      
    return builder.Build();
  }
  
  private static Model.Bcf21.Comment ConvertComment(Model.Bcf30.Comment from) {
    var builder = new CommentBuilder();
    
    builder
      .SetGuid(from.Guid)
      .SetDate(from.Date)
      .SetAuthor(from.Author)
      .SetCommentProperty(from.CommentProperty)
      .SetViewPointGuid(from.Viewpoint.Guid)
      .SetModifiedDate(from.ModifiedDate)
      .SetModifiedAuthor(from.ModifiedAuthor);
    
    return builder.Build();
  }
  
  private static Model.Bcf21.ViewPoint ConvertViewPoint(
    Model.Bcf30.ViewPoint from) {
    var builder = new VisualizationInfoBuilder();

    
    builder.Build();
    
    var viewPoint = new ViewPoint();
    
    return viewPoint;
  }
  
  private static Model.Bcf21.TopicRelatedTopic ConvertRelatedTopic(
    Model.Bcf30.TopicRelatedTopicsRelatedTopic from) {
    var relatedTopic = new TopicRelatedTopic {
      Guid = from.Guid
    };
    return relatedTopic;
  }
  
  
  
}