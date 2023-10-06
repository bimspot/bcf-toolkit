using System;

namespace BcfConverter.Builder.Bcf30;

public interface IVisibilityBuilderExtension<
  out TBuilder,
  out TViewSetupHintsBuilder> {
  TBuilder AddViewSetupHints(Action<TViewSetupHintsBuilder> builder);
}