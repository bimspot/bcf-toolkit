using System;
using BcfToolkit.Model.Interfaces;

namespace BcfToolkit.Model;

public enum BcfVersionEnum {
  Bcf21,
  Bcf30
}

public static class BcfVersion {
  public static BcfVersionEnum TryParse(string? version) {
    return version switch {
      "2.1" => BcfVersionEnum.Bcf21,
      "3.0" => BcfVersionEnum.Bcf30,
      _ => throw new ArgumentException($"Unsupported BCF version: {version}")
    };
  }

  public static BcfVersionEnum TryParse(IBcf bcf) {
    return bcf switch {
      Bcf21.Bcf => BcfVersionEnum.Bcf21,
      Bcf30.Bcf => BcfVersionEnum.Bcf30,
      _ => throw new ArgumentException($"Unsupported BCF version: {bcf.GetType()}")
    };
  }

  public static BcfVersionEnum TryParse(Type type) {
    return type switch {
      not null when type == typeof(Bcf21.Bcf) => BcfVersionEnum.Bcf21,
      not null when type == typeof(Bcf30.Bcf) => BcfVersionEnum.Bcf30,
      _ => throw new ArgumentException($"Unsupported BCF version: {type}")
    };
  }

  public static string ToString(BcfVersionEnum version) {
    return version switch {
      BcfVersionEnum.Bcf21 => "2.1",
      BcfVersionEnum.Bcf30 => "3.0"
    };
  }
}