using bcf.bcf21;

namespace bcf.Builder.Bcf21; 

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
    return _component;
  }
}