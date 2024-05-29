using BcfToolkit.Model;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30.Interfaces;

public interface IBcfBuilderDelegate {
  public delegate void OnMarkupCreated<in TMarkup>(TMarkup markup)
    where TMarkup : IMarkup;
  
  public delegate void OnProjectCreated<in TProjectInfo>(
    TProjectInfo projectInfo)
    where TProjectInfo : ProjectInfo;
  
  public delegate void OnExtensionsCreated<in TExtensions>(
    TExtensions extensions)
    where TExtensions : Extensions;
  
  public delegate void OnDocumentCreated<in TDocumentInfo>(
    TDocumentInfo documentInfo)
    where TDocumentInfo : DocumentInfo;
  
  public OnMarkupCreated<Markup> MarkupCreated { get; }
  public OnExtensionsCreated<Extensions> ExtensionsCreated { get; }
  public OnProjectCreated<ProjectInfo> ProjectCreated { get; }
  public OnDocumentCreated<DocumentInfo> DocumentCreatedCreated { get; }
}