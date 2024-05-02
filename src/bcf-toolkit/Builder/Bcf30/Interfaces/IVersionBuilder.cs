using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30.Interfaces;

public interface IVersionBuilder<out TBuilder> : IBuilder<Version> {
  /// <summary>
  ///   Returns the builder object set with the `VersionId`.
  /// </summary>
  /// <param name="id">The version id of the BCF.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetVersionId(string id);
}