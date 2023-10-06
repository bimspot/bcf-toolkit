using BcfConverter.Model;

namespace BcfConverter.Builder;

public interface IVersionBuilder<out TBuilder> : IBuilder<IVersion> {
  TBuilder AddVersionId(string id);
}