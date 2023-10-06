using System;

namespace BcfConverter.Builder.Bcf21;

public interface IViewPointBuilderExtension<
  out TBuilder,
  out TViewSetupHintsBuilder> {
  TBuilder AddViewSetupHints(Action<TViewSetupHintsBuilder> builder);
}