namespace BcfConverter.Builder;

public interface IBuilder<out TItem> {
  TItem Build();
}