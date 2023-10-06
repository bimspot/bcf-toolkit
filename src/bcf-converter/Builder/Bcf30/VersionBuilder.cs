using bcf.bcf30;

namespace bcf.Builder.Bcf30;

public class VersionBuilder : IVersionBuilder<VersionBuilder> {
  private readonly Version _version = new();

  public VersionBuilder AddVersionId(string id) {
    _version.VersionId = id;
    return this;
  }

  public IVersion Build() {
    return _version;
  }
}