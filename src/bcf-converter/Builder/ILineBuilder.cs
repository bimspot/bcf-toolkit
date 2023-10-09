using BcfConverter.Model;

namespace BcfConverter.Builder;

public interface ILineBuilder<out TBuilder> : IBuilder<ILine> {
  /// <summary>
  ///   Returns the builder object extended with `StartPoint`.
  /// </summary>
  /// <param name="x"></param>
  /// <param name="y"></param>
  /// <param name="z"></param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddStartPoint(double x, double y, double z);
  /// <summary>
  ///   Returns the builder object extended with `EndPoint`.
  /// </summary>
  /// <param name="x"></param>
  /// <param name="y"></param>
  /// <param name="z"></param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddEndPoint(double x, double y, double z);
}