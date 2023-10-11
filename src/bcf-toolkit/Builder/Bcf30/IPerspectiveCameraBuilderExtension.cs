namespace BcfToolkit.Builder.Bcf30;

public interface IPerspectiveCameraBuilderExtension<out TBuilder> {
  /// <summary>
  ///   Returns the builder object set with the `AspectRatio`.
  /// </summary>
  /// <param name="ratio">
  ///   Proportional relationship between the width and the height of the
  ///   view (w/h).
  /// </param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddAspectRatio(double ratio);
}