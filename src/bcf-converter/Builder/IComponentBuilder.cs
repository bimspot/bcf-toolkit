using BcfConverter.Model;

namespace BcfConverter.Builder;

public interface IComponentBuilder<out TBuilder> : IBuilder<IComponent> {
  TBuilder AddIfcGuid(string guid);
  TBuilder AddOriginatingSystem(string system);
  TBuilder AddAuthoringToolId(string id);
}