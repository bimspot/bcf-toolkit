namespace bcf.Builder;

public interface IBuilder<out TItem> {
  TItem Build();
}