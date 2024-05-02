using System;

namespace BcfToolkit.Model;

public enum BcfVersionEnum {
  Bcf21,
  Bcf30
}

public static class BcfVersion {
  public static BcfVersionEnum Parse(string? version) {
    return version switch {
      "2.1" => BcfVersionEnum.Bcf21,
      "3.0" => BcfVersionEnum.Bcf30,
      _ => throw new ArgumentException($"Unsupported BCF version: {version}")
    };
  }
  public static string ToVersion(BcfVersionEnum version) {
    return version switch {
      BcfVersionEnum.Bcf21 => "2.1",
      BcfVersionEnum.Bcf30 => "3.0"
    };
  }
}