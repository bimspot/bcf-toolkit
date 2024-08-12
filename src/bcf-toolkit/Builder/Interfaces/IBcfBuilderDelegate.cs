using BcfToolkit.Model.Interfaces;

namespace BcfToolkit.Builder.Interfaces;

public interface IBcfBuilderDelegate {
  public delegate void OnMarkupCreated<in TMarkup>(TMarkup markup)
    where TMarkup : IMarkup;
  
  public delegate void OnProjectCreated<in TProjectInfo>(
    TProjectInfo projectInfo)
    where TProjectInfo : IProject;
  
  public delegate void OnExtensionsCreated<in TExtensions>(
    TExtensions extensions)
    where TExtensions : IExtensions;
  
  public delegate void OnDocumentCreated<in TDocumentInfo>(
    TDocumentInfo documentInfo)
    where TDocumentInfo : IDocumentInfo;
  
  public OnMarkupCreated<IMarkup> MarkupCreated { get; }
  public OnExtensionsCreated<IExtensions> ExtensionsCreated { get; }
  public OnProjectCreated<IProject> ProjectCreated { get; }
  public OnDocumentCreated<IDocumentInfo> DocumentCreatedCreated { get; }
}