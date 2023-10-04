namespace bcf.Builder;

public interface ILineBuilder<out TBuilder> : IBuilder<ILine> {
  TBuilder AddStartPoint(double x, double y, double z);
  TBuilder AddEndPoint(double x, double y, double z);
}