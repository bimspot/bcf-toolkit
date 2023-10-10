using BcfToolkit.Model;

namespace BcfToolkit.Builder;

public interface IVersionBuilder<out TBuilder> : IBuilder<IVersion> {
  /// <summary>
  ///   Returns the builder object extended with `VersionId`.
  /// </summary>
  /// <param name="id">The version id.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddVersionId(string id);
}