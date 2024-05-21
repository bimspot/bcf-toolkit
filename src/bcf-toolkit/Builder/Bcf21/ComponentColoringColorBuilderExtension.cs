using System.Collections.Generic;
using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Bcf21;

public partial class ComponentColoringColorBuilder {
  
  public ComponentColoringColorBuilder AddComponents(List<Component> components) {
    components.ForEach(_color.Component.Add);
    return this;
  }
  
}