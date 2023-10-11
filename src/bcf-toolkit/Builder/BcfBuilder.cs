using BcfToolkit.Builder.Bcf30;

namespace BcfToolkit.Builder;

public static class BcfBuilder {
  /// <summary>
  ///   Creates a new instance of `MarkupBuilder` object.
  /// </summary>
  /// <returns></returns>
  public static MarkupBuilder Markup() {
    return new MarkupBuilder();
  }

  /// <summary>
  ///   Creates a new instance of `ProjectBuilder` object.
  /// </summary>
  /// <returns></returns>
  public static ProjectBuilder Project() {
    return new ProjectBuilder();
  }

  /// <summary>
  ///   Creates a new instance of `ExtensionsBuilder` object.
  /// </summary>
  /// <returns></returns>
  public static ExtensionsBuilder Extensions() {
    return new ExtensionsBuilder();
  }

  /// <summary>
  ///   Creates a new instance of `DocumentInfoBuilder` object.
  /// </summary>
  /// <returns></returns>
  public static DocumentInfoBuilder Document() {
    return new DocumentInfoBuilder();
  }
}