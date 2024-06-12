using BcfToolkit.Model;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30.Interfaces;

public interface IBcfBuilderDelegate {
  public delegate void OnMarkupCreated<in TMarkup>(TMarkup markup)
    where TMarkup : IMarkup;
  
  public delegate void
    OnProjectCreated<in TProjectInfo>(ProjectInfo projectInfo)
    where TProjectInfo : ProjectInfo;
  
  public delegate void OnExtensionsCreated<in TExtensions>(
    Extensions extensions);
  
  public delegate void OnDocumentCreated<in TExtensions>(
    Extensions documentInfo);
  
  public OnMarkupCreated<Markup> MarkupCreated { get; }
  
  public OnExtensionsCreated<Extensions> ExtensionsCreated { get; }
  
  public OnProjectCreated<ProjectInfo> ProjectCreated { get; }
  public OnDocumentCreated<DocumentInfo> DocumentCreatedCreated { get; }
}