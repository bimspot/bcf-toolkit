using BcfToolkit.Model;
using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Bcf21;

public class ComponentBuilder : IComponentBuilder<ComponentBuilder> {
  private readonly Component _component = new();

  public ComponentBuilder AddIfcGuid(string guid) {
    _component.IfcGuid = guid;
    return this;
  }

  public ComponentBuilder AddOriginatingSystem(string system) {
    _component.OriginatingSystem = system;
    return this;
  }

  public ComponentBuilder AddAuthoringToolId(string id) {
    _component.AuthoringToolId = id;
    return this;
  }

  public IComponent Build() {
    return BuilderUtils.ValidateItem(_component);
  }
}