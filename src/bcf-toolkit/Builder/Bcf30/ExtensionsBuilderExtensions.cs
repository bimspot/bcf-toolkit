using System.Collections.Generic;

namespace BcfToolkit.Builder.Bcf30;

public partial class ExtensionsBuilder {
  public ExtensionsBuilder AddTopicTypes(List<string> types) {
    types.ForEach(t => _extensions.TopicTypes.Add(t));
    return this;
  }

  public ExtensionsBuilder AddTopicStatuses(List<string> statuses) {
    statuses.ForEach(s => _extensions.TopicStatuses.Add(s));
    return this;
  }

  public ExtensionsBuilder AddPriorities(List<string> priorities) {
    priorities.ForEach(p => _extensions.Priorities.Add(p));
    return this;
  }

  public ExtensionsBuilder AddTopicLabels(List<string> labels) {
    labels.ForEach(l => _extensions.TopicLabels.Add(l));
    return this;
  }

  public ExtensionsBuilder AddUsers(List<string> users) {
    users.ForEach(u => _extensions.Users.Add(u));
    return this;
  }

  public ExtensionsBuilder AddSnippetTypes(List<string> types) {
    types.ForEach(t => _extensions.SnippetTypes.Add(t));
    return this;
  }

  public ExtensionsBuilder AddStages(List<string> stages) {
    stages.ForEach(s => _extensions.Stages.Add(s));
    return this;
  }
}