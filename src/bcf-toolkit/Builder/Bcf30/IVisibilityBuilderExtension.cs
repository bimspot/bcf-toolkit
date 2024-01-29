using System;

namespace BcfToolkit.Builder.Bcf30;

public interface IVisibilityBuilderExtension<
  out TBuilder,
  out TViewSetupHintsBuilder> {
  /// <summary>
  ///   Returns the builder object extended with a new `Exception`.
  /// </summary>
  /// <param name="builder">The builder for the `Exception` object.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetViewSetupHints(Action<TViewSetupHintsBuilder> builder);
}