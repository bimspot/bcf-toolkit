
using BcfConverter.Model;
using BcfConverter.Model.Bcf30;

namespace BcfConverter.Builder.Bcf30;

public class ExtensionsBuilder : IExtensionsBuilder<ExtensionsBuilder> {
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

  public IExtensions Build() {
    return _extensions;
  }
}