using bcf.bcf30;

namespace bcf.Builder.Bcf30;

public class LineBuilder : ILineBuilder<LineBuilder> {
  private readonly Line _line = new();

  public LineBuilder AddStartPoint(double x, double y, double z) {
    _line.StartPoint.X = x;
    _line.StartPoint.Y = y;
    _line.StartPoint.Z = z;
    return this;
  }

  public LineBuilder AddEndPoint(double x, double y, double z) {
    _line.EndPoint.X = x;
    _line.EndPoint.Y = y;
    _line.EndPoint.Z = z;
    return this;
  }

  public ILine Build() {
    return _line;
  }
}