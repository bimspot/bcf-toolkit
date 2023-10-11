using BcfToolkit.Model;

namespace BcfToolkit.Builder;

public interface IVersionBuilder<out TBuilder> : IBuilder<IVersion> {
  /// <summary>
  ///   Returns the builder object set with the `VersionId`.
  /// </summary>
  /// <param name="id">The version id of the BCF.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetVersionId(string id);
}