using System;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30.Interfaces;

public interface IVisibilityBuilder<
  out TBuilder,
  out TComponentBuilder,
  out TViewSetupHintsBuilder> :
  IBuilder<ComponentVisibility> {
  /// <summary>
  ///   Returns the builder object set with the `DefaultVisibility`.
  /// </summary>
  /// <param name="visibility">The default visibility of the components.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetDefaultVisibility(bool? visibility);

  /// <summary>
  ///   Returns the builder object extended with a new `Exception`.
  /// </summary>
  /// <param name="builder">The builder for the `Exception` object.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddException(Action<TComponentBuilder> builder);

  /// <summary>
  ///   Returns the builder object set with the `ViewSetupHints`.
  /// </summary>
  /// <param name="builder">The builder for the `ViewSetupHints` object.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetViewSetupHints(Action<TViewSetupHintsBuilder> builder);
}