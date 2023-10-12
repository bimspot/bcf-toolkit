using BcfToolkit.Model;

namespace BcfToolkit.Builder;

public interface ILineBuilder<out TBuilder> : IBuilder<ILine> {
  /// <summary>
  ///   Returns the builder object set with the `StartPoint`.
  /// </summary>
  /// <param name="x">X coordinate of the point.</param>
  /// <param name="y">Y coordinate of the point.</param>
  /// <param name="z">Z coordinate of the point.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetStartPoint(double x, double y, double z);
  /// <summary>
  ///   Returns the builder object set with the `EndPoint`.
  /// </summary>
  /// <param name="x">X coordinate of the point.</param>
  /// <param name="y">Y coordinate of the point.</param>
  /// <param name="z">Z coordinate of the point.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetEndPoint(double x, double y, double z);
}