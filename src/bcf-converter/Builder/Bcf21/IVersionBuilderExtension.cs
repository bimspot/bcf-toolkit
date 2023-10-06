namespace BcfConverter.Builder.Bcf21;

public interface IVersionBuilderExtension<out TBuilder> {
  TBuilder AddDetailedVersion(string version);
}