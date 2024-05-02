namespace BcfToolkit.Builder.Bcf21.Interfaces;

public interface IVersionBuilderExtension<out TBuilder> {
  /// <summary>
  ///   Returns the builder object set with the`DetailedVersion`.
  /// </summary>
  /// <param name="version">The detailed version of the BCF.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetDetailedVersion(string version);
}