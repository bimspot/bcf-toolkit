namespace bcf.Builder;

public interface IVersionBuilder<out TBuilder> : IBuilder<IVersion> {
  TBuilder AddVersionId(string id);
}