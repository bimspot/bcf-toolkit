using BcfConverter.Model;

namespace BcfConverter.Builder;

public interface IExtensionsBuilder<out TBuilder> : IBuilder<IExtensions> {
  TBuilder AddTopicType(string type);
  TBuilder AddTopicStatus(string status);
  TBuilder AddPriority(string priority);
  TBuilder AddTopicLabel(string label);
  TBuilder AddUser(string user);
  TBuilder AddSnippetType(string type);
  TBuilder AddStage(string stage);
}