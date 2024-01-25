using System;
using BcfToolkit.Model;

namespace BcfToolkit.Builder;

public interface IBcfBuilder<
  out TBuilder,
  out TMarkupBuilder,
  out TProjectBuilder> :
  IBuilder<IBcf>,
  IFromStreamBuilder<IBcf> {
  /// <summary>
  ///   Returns the builder object extended with a new `Markup`.
  /// </summary>
  /// <param name="builder">Builder of the markup.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddMarkup(Action<TMarkupBuilder> builder);
  /// <summary>
  ///   Returns the builder object set with the `Project`.
  /// </summary>
  /// <param name="builder">Builder of the project.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetProject(Action<TProjectBuilder> builder);
}