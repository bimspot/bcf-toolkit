using System;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Bcf21.Interfaces;

public interface IComponentColoringColorBuilder<
  out TBuilder,
  out TComponentBuilder> :
  IBuilder<ComponentColoringColor> {
  /// <summary>
  ///   Returns the builder object extended with `Color`. For example, 40E0D0
  /// </summary>
  /// <param name="color">The color in ARGB format.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetColor(string color);

  /// <summary>
  ///   Returns the builder object extended with `Component` which is colored.
  /// </summary>
  /// <param name="builder">The builder of the `Component`.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddComponent(Action<TComponentBuilder> builder);
}