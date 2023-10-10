using BcfToolkit.Model;
using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Bcf21;

public partial class VersionBuilder : IVersionBuilder<VersionBuilder> {
  private readonly Version _version = new();

  public VersionBuilder AddVersionId(string id) {
    _version.VersionId = id;
    return this;
  }

  public IVersion Build() {
    return BuilderUtils.ValidateItem(_version);
  }
}