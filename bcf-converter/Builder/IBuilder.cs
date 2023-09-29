namespace bcf.Builder;

public interface IBuilder<out T> {
  T Build();
}