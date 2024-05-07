using System.Collections.Generic;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

public partial class ComponentColoringComponentColoringColorBuilder {
  public ComponentColoringComponentColoringColorBuilder AddComponents(List<Component> components) {
    components.ForEach(_color.Components.Add);
    return this;
  }
}