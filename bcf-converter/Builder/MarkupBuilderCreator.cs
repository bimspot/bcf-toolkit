using System;
using bcf.Builder.Bcf21;

namespace bcf.Builder;

public static class MarkupBuilderCreator {
  public static IMarkupBuilder CreateBuilder(BcfVersionEnum version) {
    return version switch {
      BcfVersionEnum.Bcf21 => new MarkupBuilder(),
      BcfVersionEnum.Bcf30 => new Bcf30.MarkupBuilder(),
      _ => throw new ArgumentException($"Unsupported BCF version: {version}")
    };
  }
}