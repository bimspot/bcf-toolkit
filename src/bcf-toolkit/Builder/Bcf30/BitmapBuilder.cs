using System;
using BcfToolkit.Builder.Bcf30.Interfaces;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

public class BitmapBuilder :
  IBitmapBuilder<BitmapBuilder>,
  IDefaultBuilder<BitmapBuilder> {
  private readonly Bitmap _bitmap = new();

  public BitmapBuilder SetFormat(string format) {
    _bitmap.Format = (BitmapFormat)Enum.Parse(typeof(BitmapFormat), format);
    return this;
  }

  public BitmapBuilder SetReference(string reference) {
    _bitmap.Reference = reference;
    return this;
  }

  public BitmapBuilder SetLocation(double x, double y, double z) {
    _bitmap.Location = new Point {
      X = x,
      Y = y,
      Z = z
    };
    return this;
  }

  public BitmapBuilder SetNormal(double x, double y, double z) {
    _bitmap.Normal = new Direction {
      X = x,
      Y = y,
      Z = z
    };
    return this;
  }

  public BitmapBuilder SetUp(double x, double y, double z) {
    _bitmap.Up = new Direction {
      X = x,
      Y = y,
      Z = z
    };
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

  public Bitmap Build() {
    return BuilderUtils.ValidateItem(_bitmap);
  }
}