using BcfToolkit.Model;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

public class LineBuilder : 
  ILineBuilder<LineBuilder>, 
  IDefaultBuilder<LineBuilder> {
  private readonly Line _line = new();

  public LineBuilder SetStartPoint(double x, double y, double z) {
    _line.StartPoint.X = x;
    _line.StartPoint.Y = y;
    _line.StartPoint.Z = z;
    return this;
  }

  public LineBuilder SetEndPoint(double x, double y, double z) {
    _line.EndPoint.X = x;
    _line.EndPoint.Y = y;
    _line.EndPoint.Z = z;
    return this;
  }
  
  public LineBuilder WithDefaults() {
    this
      .SetStartPoint(0.0, 0.0, 0.0)
      .SetEndPoint(1.0, 0.0, 0.0);
    return this;
  }

  public ILine Build() {
    return BuilderUtils.ValidateItem(_line);
  }
}