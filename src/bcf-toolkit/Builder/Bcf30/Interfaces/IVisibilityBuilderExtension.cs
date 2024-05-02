using System;

namespace BcfToolkit.Builder.Bcf30.Interfaces;

public interface IVisibilityBuilderExtension<
  out TBuilder,
  out TViewSetupHintsBuilder> {
  /// <summary>
  ///   Returns the builder object set with the `ViewSetupHints`.
  /// </summary>
  /// <param name="builder">The builder for the `ViewSetupHints` object.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetViewSetupHints(Action<TViewSetupHintsBuilder> builder);
}