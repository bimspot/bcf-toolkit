using System;
using System.Collections.Generic;
using System.Linq;
using BcfToolkit.Builder.Bcf30.Interfaces;
using BcfToolkit.Model.Bcf30;
using BcfToolkit.Utils;

namespace BcfToolkit.Builder.Bcf30;

public partial class BcfBuilder :
  IBcfBuilderExtension<BcfBuilder, ExtensionsBuilder, DocumentInfoBuilder> {

  public BcfBuilder AddMarkups(List<Markup> markups, bool update = false) {
    markups.ForEach(m => {
      _bcf.Markups.Add(m);
      // updating Extensions
      if (update)
        UpdateExtensions(m.Topic);
    });
    return this;
  }

  /// <summary>
  /// TODO: add description
  /// </summary>
  /// <param name="topic"></param>
  private void UpdateExtensions(Topic topic) {
    var users = new HashSet<string> {
      topic.AssignedTo,
      topic.CreationAuthor,
      topic.ModifiedAuthor
    };

    var commenters = topic.Comments.SelectMany(c => new HashSet<string> { c.Author, c.ModifiedAuthor });

    var ext = new {
      topic.TopicType,
      topic.TopicStatus,
      topic.Priority,
      Labels = new HashSet<string>(topic.Labels).ToList(),
      Users = users.Concat(commenters).ToList(),
      topic.BimSnippet?.SnippetType,
      topic.Stage
    };

    _bcf.Extensions = new Extensions();
    _bcf.Extensions.TopicTypes.AddIfNotExists(ext.TopicType);
    _bcf.Extensions.TopicStatuses.AddIfNotExists(ext.TopicStatus);
    _bcf.Extensions.Priorities.AddIfNotExists(ext.Priority);
    _bcf.Extensions.SnippetTypes.AddIfNotExists(ext.SnippetType);
    _bcf.Extensions.Stages.AddIfNotExists(ext.Stage);

    ext.Labels.ForEach(l => _bcf.Extensions.TopicLabels.AddIfNotExists(l));
    ext.Users.ForEach(u => _bcf.Extensions.Users.AddIfNotExists(u));
  }

  public BcfBuilder SetExtensions(Action<ExtensionsBuilder> builder) {
    var extensions =
      BuilderUtils.BuildItem<ExtensionsBuilder, Extensions>(builder);
    _bcf.Extensions = extensions;
    return this;
  }

  public BcfBuilder SetDocumentInfo(Action<DocumentInfoBuilder> builder) {
    var documentInfo =
      BuilderUtils.BuildItem<DocumentInfoBuilder, DocumentInfo>(builder);
    _bcf.Document = documentInfo;
    return this;
  }
}