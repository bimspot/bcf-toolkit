namespace bcf.Builder;

public interface IBitmapBuilder<out TBuilder> : IBuilder<IBitmap> {
  TBuilder AddFormat(string format);
  TBuilder AddReference(string reference);
  TBuilder AddLocation(double x, double y, double z);
  TBuilder AddNormal(double x, double y, double z);
  TBuilder AddUp(double x, double y, double z);
  TBuilder AddHeight(double height);
}