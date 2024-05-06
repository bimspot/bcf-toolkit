using BcfToolkit.Builder.Bcf30.Interfaces;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

public class VersionBuilder :
  IVersionBuilder<VersionBuilder>,
  IDefaultBuilder<VersionBuilder> {
  private readonly Version _version = new();
  
  public VersionBuilder SetVersionId(string id) {
    _version.VersionId = id;
    return this;
  }
  
  public VersionBuilder WithDefaults() {
    this.SetVersionId(BcfVersion.ToVersion(BcfVersionEnum.Bcf30));
    return this;
  }

  public Version Build() {
    return BuilderUtils.ValidateItem(_version);
  }
}