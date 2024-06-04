using BcfToolkit.Builder.Bcf21.Interfaces;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Bcf21;

public class LineBuilder :
  ILineBuilder<LineBuilder>,
  IDefaultBuilder<LineBuilder> {
  private readonly Line _line = new();
  
  public LineBuilder SetStartPoint(double x, double y, double z) {
    _line.StartPoint = new Point {
      X = x,
      Y = y,
      Z = z
    };
    return this;
  }
  
  public LineBuilder SetEndPoint(double x, double y, double z) {
    _line.EndPoint = new Point {
      X = x,
      Y = y,
      Z = z
    };
    return this;
  }

  public LineBuilder WithDefaults() {
    this
      .SetStartPoint(0.0, 0.0, 0.0)
      .SetEndPoint(1.0, 0.0, 0.0);
    return this;
  }

  public Line Build() {
    return BuilderUtils.ValidateItem(_line);
  }
}