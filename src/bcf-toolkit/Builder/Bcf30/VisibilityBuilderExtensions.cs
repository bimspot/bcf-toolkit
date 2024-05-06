using System.Collections.Generic;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

public partial class VisibilityBuilder {
  public VisibilityBuilder AddExceptions(List<Component> exceptions) {
    exceptions.ForEach(_visibility.Exceptions.Add);
    return this;
  }
}