using System;

namespace bcf.Builder.Bcf30;

public interface IVisibilityBuilderExtension<
  out TBuilder,
  out TViewSetupHintsBuilder> {
  TBuilder AddViewSetupHints(Action<TViewSetupHintsBuilder> builder);
}