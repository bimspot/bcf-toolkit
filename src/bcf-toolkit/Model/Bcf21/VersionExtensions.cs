using BcfToolkit.Model.Interfaces;

namespace BcfToolkit.Model.Bcf21;

public partial class Version : IVersion {
  public Version() {
    VersionId = "2.1";
    DetailedVersion = "2.1";
  }
}