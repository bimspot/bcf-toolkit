using System;

namespace bcf.Builder;

public static class MarkupBuilderCreator {
  public static Bcf30.MarkupBuilder CreateBuilder() {
    return new Bcf30.MarkupBuilder();
  }
}