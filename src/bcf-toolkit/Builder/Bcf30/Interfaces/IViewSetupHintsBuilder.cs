using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30.Interfaces;

public interface
  IViewSetupHintsBuilder<out TBuilder> : IBuilder<ViewSetupHints> {
  /// <summary>
  ///   Returns the builder object set with the `SpaceVisible`.
  /// </summary>
  /// <param name="spaceVisible">The visibility of spaces by default.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetSpaceVisible(bool? spaceVisible);

  /// <summary>
  ///   Returns the builder object set with the `SpaceBoundariesVisible`.
  /// </summary>
  /// <param name="spaceBoundariesVisible">
  ///   The visibility of space boundaries by default.
  /// </param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetSpaceBoundariesVisible(bool? spaceBoundariesVisible);

  /// <summary>
  ///   Returns the builder object set with the `OpeningVisible`.
  /// </summary>
  /// <param name="openingVisible">
  ///   The visibility of openings by default.
  /// </param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetOpeningVisible(bool? openingVisible);
}