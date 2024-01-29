namespace BcfToolkit.Builder.Bcf21;

public partial class VersionBuilder : IVersionBuilderExtension<VersionBuilder> {
  public VersionBuilder SetDetailedVersion(string version) {
    _version.DetailedVersion = version;
    return this;
  }
}