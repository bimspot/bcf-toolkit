using System;

namespace bcf.Builder;

public static class BuilderUtils {
  public static TItem BuildItem<TBuilder, TItem>(Action<TBuilder> itemBuilder)
    where TBuilder : IBuilder<TItem>, new() {
    var builder = new TBuilder();
    itemBuilder(builder);
    return builder.Build();
  }
}