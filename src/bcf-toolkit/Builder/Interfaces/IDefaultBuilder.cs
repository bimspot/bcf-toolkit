namespace BcfToolkit.Builder.Interfaces;

public interface IDefaultBuilder<out TBuilder> {
  /// <summary>
  ///   It builds and returns the specified item filled with default values
  ///   for the required fields.
  /// </summary>
  /// <returns>Returns the built item.</returns>
  TBuilder WithDefaults();
}