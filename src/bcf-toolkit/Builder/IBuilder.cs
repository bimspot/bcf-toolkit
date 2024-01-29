namespace BcfToolkit.Builder;

public interface IBuilder<out TItem> {
  /// <summary>
  ///   It builds and returns the specified item from the builder.
  /// </summary>
  /// <returns>Returns the built item.</returns>
  TItem Build();
}