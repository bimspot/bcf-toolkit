using BcfToolkit.Model;
using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Bcf21;

public partial class VersionBuilder :
  IVersionBuilder<VersionBuilder>,
  IDefaultBuilder<VersionBuilder> {
  private readonly Version _version = new();

  public VersionBuilder SetVersionId(string id) {
    _version.VersionId = id;
    return this;
  }

  public VersionBuilder WithDefaults() {
    this.SetVersionId(BcfVersion.ToVersion(BcfVersionEnum.Bcf21));
    return this;
  }

  public IVersion Build() {
    return BuilderUtils.ValidateItem(_version);
  }
}