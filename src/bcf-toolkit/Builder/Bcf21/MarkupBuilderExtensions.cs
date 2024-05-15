using System.Collections.Generic;
using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Bcf21;


public partial class MarkupBuilder {
  public MarkupBuilder AddHeaderFiles(List<HeaderFile> files) {
    files.ForEach(_markup.Header.Add);
    return this;
  }
  
  public MarkupBuilder AddReferenceLinks(List<string> links) {
    links.ForEach(_markup.Topic.ReferenceLink.Add);
    return this;
  }
  
  public MarkupBuilder AddLabels(List<string> labels) {
    labels.ForEach(_markup.Topic.Labels.Add);
    return this;
  }
  
  public MarkupBuilder SetBimSnippet(BimSnippet bimSnippet) {
    _markup.Topic.BimSnippet = bimSnippet;
    return this;
  }
  
  public MarkupBuilder AddDocumentReferences(List<TopicDocumentReference> documentReferences) {
    documentReferences.ForEach(_markup.Topic.DocumentReference.Add);
    return this;
  }
  
  public MarkupBuilder AddComments(List<Comment> comments) {
    comments.ForEach(_markup.Comment.Add);
    return this;
  }
  
  public MarkupBuilder AddViewPoints(List<ViewPoint> viewPoints) {
    viewPoints.ForEach(_markup.Viewpoints.Add);
    return this;
  }
  
  public MarkupBuilder AddRelatedTopics(List<TopicRelatedTopic>  relatedTopics) {
    relatedTopics.ForEach(_markup.Topic.RelatedTopic.Add);
    return this;
  }

  
}