using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BcfToolkit.Model.Bcf30;
using BcfToolkit.Utils;

namespace BcfToolkit.Builder.Bcf30;

public partial class BcfBuilder {
  /// <summary>
  ///   The method builds and returns an instance of BCF 3.0 object from the
  ///   specified file stream.
  /// </summary>
  /// <param name="source">The file stream.</param>
  /// <returns>Returns the built object.</returns>
  public async Task<Bcf> BuildFromStream(Stream source) {
    _bcf.Markups =
      await BcfExtensions.ParseMarkups<Markup, VisualizationInfo>(source);
    _bcf.Extensions = await BcfExtensions.ParseExtensions<Extensions>(source);
    _bcf.Project = await BcfExtensions.ParseProject<ProjectInfo>(source);
    _bcf.Document = await BcfExtensions.ParseDocuments<DocumentInfo>(source);
    return BuilderUtils.ValidateItem(_bcf);
  }

  /// <summary>
  ///   Returns the builder object extended with a set of `Markup` items.
  ///   Optionally the `Extensions` could be updated.
  /// </summary>
  /// <param name="markups">The list of `Markup` items.</param>
  /// <param name="update">Is the `Extensions` updated.</param>
  /// <returns>Returns the builder object.</returns>
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
  ///   Updates the `Extensions` with users, priorities, labels, stages, types
  ///   and statuses of the topics, Bim snippet types, etc.
  /// </summary>
  /// <param name="topic">
  ///   The topic which contains the values which must be included.
  /// </param>
  private void UpdateExtensions(Topic topic) {
    // collecting authors and assigned users
    var users = new HashSet<string> {
      topic.AssignedTo,
      topic.CreationAuthor,
      topic.ModifiedAuthor
    };

    // collecting commenters
    var commenters = topic.Comments
      .SelectMany(c => new HashSet<string> {
        c.Author,
        c.ModifiedAuthor
      });

    var extensions = new {
      topic.TopicType,
      topic.TopicStatus,
      topic.Priority,
      Labels = new HashSet<string>(topic.Labels).ToList(),
      Users = users.Concat(commenters).ToList(),
      topic.BimSnippet?.SnippetType,
      topic.Stage
    };

    _bcf.Extensions = new Extensions();
    _bcf.Extensions.TopicTypes.AddIfNotExists(extensions.TopicType);
    _bcf.Extensions.TopicStatuses.AddIfNotExists(extensions.TopicStatus);
    _bcf.Extensions.Priorities.AddIfNotExists(extensions.Priority);
    _bcf.Extensions.SnippetTypes.AddIfNotExists(extensions.SnippetType);
    _bcf.Extensions.Stages.AddIfNotExists(extensions.Stage);

    extensions.Labels.ForEach(l =>
      _bcf.Extensions.TopicLabels.AddIfNotExists(l));
    extensions.Users.ForEach(u =>
      _bcf.Extensions.Users.AddIfNotExists(u));
  }

  public BcfBuilder SetExtensions(Extensions extensions) {
    _bcf.Extensions = extensions;
    return this;
  }

  public BcfBuilder SetProject(ProjectInfo? project) {
    _bcf.Project = project;
    return this;
  }

  public BcfBuilder SetDocument(DocumentInfo? document) {
    _bcf.Document = document;
    return this;
  }
}