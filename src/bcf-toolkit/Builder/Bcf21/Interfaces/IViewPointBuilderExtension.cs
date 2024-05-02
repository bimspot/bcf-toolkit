using System;

namespace BcfToolkit.Builder.Bcf21.Interfaces;

public interface IViewPointBuilderExtension<
  out TBuilder,
  out TViewSetupHintsBuilder> {
  /// <summary>
  ///   Returns the builder object set with the `ViewSetupHints`.
  /// </summary>
  /// <param name="builder">The builder for the `ViewSetupHints` object.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetViewSetupHints(Action<TViewSetupHintsBuilder> builder);
}