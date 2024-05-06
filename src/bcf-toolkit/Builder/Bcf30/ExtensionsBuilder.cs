using System.Collections.Generic;
using BcfToolkit.Builder.Bcf30.Interfaces;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

public partial class ExtensionsBuilder :
  IExtensionsBuilder<ExtensionsBuilder>,
  IDefaultBuilder<ExtensionsBuilder> {
  private readonly Extensions _extensions = new();
  
  public ExtensionsBuilder AddTopicType(string type) {
    _extensions.TopicTypes.Add(type);
    return this;
  }

  public ExtensionsBuilder AddTopicStatus(string status) {
    _extensions.TopicStatuses.Add(status);
    return this;
  }

  public ExtensionsBuilder AddPriority(string priority) {
    _extensions.Priorities.Add(priority);
    return this;
  }

  public ExtensionsBuilder AddTopicLabel(string label) {
    _extensions.TopicLabels.Add(label);
    return this;
  }

  public ExtensionsBuilder AddUser(string user) {
    _extensions.Users.Add(user);
    return this;
  }

  public ExtensionsBuilder AddSnippetType(string type) {
    _extensions.SnippetTypes.Add(type);
    return this;
  }

  public ExtensionsBuilder AddStage(string stage) {
    _extensions.Stages.Add(stage);
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

  public Extensions Build() {
    return BuilderUtils.ValidateItem(_extensions);
  }

  
}