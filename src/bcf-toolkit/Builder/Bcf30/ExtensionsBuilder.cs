using System.Collections.Generic;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

public class ExtensionsBuilder :
  IExtensionsBuilder<ExtensionsBuilder>,
  IDefaultBuilder<ExtensionsBuilder> {
  private readonly Model.Bcf30.Extensions _extensions = new();

  public ExtensionsBuilder AddTopicType(string type) {
    _extensions.TopicTypes.Add(type);
    return this;
  }

  public ExtensionsBuilder AddTopicTypes(List<string> types) {
    types.ForEach(t => _extensions.TopicTypes.Add(t));
    return this;
  }

  public ExtensionsBuilder AddTopicStatus(string status) {
    _extensions.TopicStatuses.Add(status);
    return this;
  }

  public ExtensionsBuilder AddTopicStatuses(List<string> statuses) {
    statuses.ForEach(s => _extensions.TopicStatuses.Add(s));
    return this;
  }

  public ExtensionsBuilder AddPriority(string priority) {
    _extensions.Priorities.Add(priority);
    return this;
  }

  public ExtensionsBuilder AddPriorities(List<string> priorities) {
    priorities.ForEach(p => _extensions.Priorities.Add(p));
    return this;
  }

  public ExtensionsBuilder AddTopicLabel(string label) {
    _extensions.TopicLabels.Add(label);
    return this;
  }

  public ExtensionsBuilder AddTopicLabels(List<string> labels) {
    labels.ForEach(l => _extensions.TopicLabels.Add(l));
    return this;
  }

  public ExtensionsBuilder AddUser(string user) {
    _extensions.Users.Add(user);
    return this;
  }

  public ExtensionsBuilder AddUsers(List<string> users) {
    users.ForEach(u => _extensions.Users.Add(u));
    return this;
  }

  public ExtensionsBuilder AddSnippetType(string type) {
    _extensions.SnippetTypes.Add(type);
    return this;
  }

  public ExtensionsBuilder AddSnippetTypes(List<string> types) {
    types.ForEach(t => _extensions.SnippetTypes.Add(t));
    return this;
  }

  public ExtensionsBuilder AddStage(string stage) {
    _extensions.Stages.Add(stage);
    return this;
  }

  public ExtensionsBuilder AddStages(List<string> stages) {
    stages.ForEach(s => _extensions.Stages.Add(s));
    return this;
  }

  public ExtensionsBuilder WithDefaults() {
    var types = new List<string> {
      "ERROR",
      "WARNING",
      "INFORMATION",
      "CLASH",
      "OTHER"
    };
    var statuses = new List<string> {
      "OPEN",
      "IN_PROGRESS",
      "SOLVED",
      "CLOSED"
    };
    var priorities = new List<string> {
      "LOW",
      "MEDIUM",
      "HIGH",
      "CRITICAL"
    };
    this
      .AddTopicTypes(types)
      .AddTopicStatuses(statuses)
      .AddPriorities(priorities);
    return this;
  }

  public IExtensions Build() {
    return BuilderUtils.ValidateItem(_extensions);
  }
}