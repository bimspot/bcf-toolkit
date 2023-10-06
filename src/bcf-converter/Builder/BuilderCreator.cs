using BcfConverter.Builder.Bcf30;

namespace BcfConverter.Builder;

public static class BuilderCreator {
  /// <summary>
  ///   Creates a new instance of `MarkupBuilder` object.
  /// </summary>
  /// <returns></returns>
  public static MarkupBuilder CreateMarkupBuilder() {
    return new MarkupBuilder();
  }
  
  /// <summary>
  ///   Creates a new instance of `ProjectBuilder` object.
  /// </summary>
  /// <returns></returns>
  public static ProjectBuilder CreateProjectBuilder() {
    return new ProjectBuilder();
  }
  
  /// <summary>
  ///   Creates a new instance of `ExtensionsBuilder` object.
  /// </summary>
  /// <returns></returns>
  public static ExtensionsBuilder CreateExtensionsBuilder() {
    return new ExtensionsBuilder();
  }
  
  /// <summary>
  ///   Creates a new instance of `DocumentInfoBuilder` object.
  /// </summary>
  /// <returns></returns>
  public static DocumentInfoBuilder CreateDocumentBuilder() {
    return new DocumentInfoBuilder();
  }
}