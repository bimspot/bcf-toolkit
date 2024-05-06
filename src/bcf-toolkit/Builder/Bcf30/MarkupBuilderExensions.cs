using System.Collections.Generic;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

public partial class MarkupBuilder {
  public MarkupBuilder AddHeaderFiles(List<File> files) {
    files.ForEach(_markup.Header.Files.Add);
    return this;
  }

  public MarkupBuilder AddReferenceLinks(List<string> links) {
    links.ForEach(_markup.Topic.ReferenceLinks.Add);
    return this;
  }

  public MarkupBuilder AddLabels(List<string> labels) {
    labels.ForEach(_markup.Topic.Labels.Add);
    return this;
  }

  public MarkupBuilder AddComments(List<Comment> comments) {
    comments.ForEach(_markup.Topic.Comments.Add);
    return this;
  }

  public MarkupBuilder AddViewPoints(List<ViewPoint> viewpoints) {
    viewpoints.ForEach(_markup.Topic.Viewpoints.Add);
    return this;
  }

  public MarkupBuilder AddRelatedTopics(List<string> relatedTopicGuids) {
    relatedTopicGuids.ForEach(id => {
      var topic = new TopicRelatedTopicsRelatedTopic {
        Guid = id
      };
      _markup.Topic.RelatedTopics.Add(topic);
    });
    return this;
  }

  public MarkupBuilder AddDocumentReferences(
    List<DocumentReference> documentReferences) {
    documentReferences.ForEach(_markup.Topic.DocumentReferences.Add);
    return this;
  }
}