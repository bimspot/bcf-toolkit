using System;
using BcfToolkit.Builder.Bcf21.Interfaces;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Bcf21;

public class BitmapBuilder :
  IBitmapBuilder<BitmapBuilder>,
  IDefaultBuilder<BitmapBuilder> {
  private readonly VisualizationInfoBitmap _bitmap = new();

  public BitmapBuilder SetFormat(string format) {
    _bitmap.Bitmap = (BitmapFormat)Enum.Parse(typeof(BitmapFormat), format);
    return this;
  }

  public BitmapBuilder SetReference(string reference) {
    _bitmap.Reference = reference;
    return this;
  }

  public BitmapBuilder SetLocation(double x, double y, double z) {
    _bitmap.Location.X = x;
    _bitmap.Location.Y = y;
    _bitmap.Location.Z = z;
    return this;
  }

  public BitmapBuilder SetNormal(double x, double y, double z) {
    _bitmap.Normal.X = x;
    _bitmap.Normal.Y = y;
    _bitmap.Normal.Z = z;
    return this;
  }

  public BitmapBuilder SetUp(double x, double y, double z) {
    _bitmap.Up.X = x;
    _bitmap.Up.Y = y;
    _bitmap.Up.Z = z;
    return this;
  }

  public BitmapBuilder SetHeight(double height) {
    _bitmap.Height = height;
    return this;
  }
  
  public BitmapBuilder WithDefaults() {
    this
      .SetFormat("PNG")
      .SetReference("Default bitmap file")
      .SetLocation(0.0, 0.0, 0.0)
      .SetNormal(1.0, 0.0, 0.0)
      .SetUp(0.0, 0.0, 1.0)
      .SetHeight(1.0);
    return this;
  }

  public VisualizationInfoBitmap Build() {
    return BuilderUtils.ValidateItem(_bitmap);
  }
}