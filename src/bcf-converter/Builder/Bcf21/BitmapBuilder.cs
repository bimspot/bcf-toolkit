using System;
using bcf.bcf21;

namespace bcf.Builder.Bcf21;

public class BitmapBuilder : IBitmapBuilder<BitmapBuilder> {
  private readonly VisualizationInfoBitmap _bitmap = new();

  public BitmapBuilder AddFormat(string format) {
    _bitmap.Bitmap = (BitmapFormat)Enum.Parse(typeof(BitmapFormat), format);
    return this;
  }

  public BitmapBuilder AddReference(string reference) {
    _bitmap.Reference = reference;
    return this;
  }

  public BitmapBuilder AddLocation(double x, double y, double z) {
    _bitmap.Location.X = x;
    _bitmap.Location.Y = y;
    _bitmap.Location.Z = z;
    return this;
  }

  public BitmapBuilder AddNormal(double x, double y, double z) {
    _bitmap.Normal.X = x;
    _bitmap.Normal.Y = y;
    _bitmap.Normal.Z = z;
    return this;
  }

  public BitmapBuilder AddUp(double x, double y, double z) {
    _bitmap.Up.X = x;
    _bitmap.Up.Y = y;
    _bitmap.Up.Z = z;
    return this;
  }

  public BitmapBuilder AddHeight(double height) {
    _bitmap.Height = height;
    return this;
  }

  public IBitmap Build() {
    return _bitmap;
  }
}