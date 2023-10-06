namespace BcfConverter.Builder.Bcf21;

public partial class VersionBuilder : IVersionBuilderExtension<VersionBuilder> {
  public VersionBuilder AddDetailedVersion(string version) {
    _version.DetailedVersion = version;
    return this;
  }
}