using System.Collections.Generic;
using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Bcf21;

public partial class VisibilityBuilder {

  public VisibilityBuilder AddExceptions(List<Component> components) {
    components.ForEach(_visibility.Exceptions.Add);
    return this;
  }

}