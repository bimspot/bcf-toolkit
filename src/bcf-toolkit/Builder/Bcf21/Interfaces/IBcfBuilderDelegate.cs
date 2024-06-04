using BcfToolkit.Model;
using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Bcf21.Interfaces;

public interface IBcfBuilderDelegate {
  public delegate void OnMarkupCreated<in TMarkup>(TMarkup markup)
    where TMarkup : IMarkup;

  public delegate void
    OnProjectCreated<in TProjectExtension>(ProjectExtension projectInfo)
    where TProjectExtension : ProjectExtension;

  public OnMarkupCreated<Markup> MarkupCreated { get; }
  
  public OnProjectCreated<ProjectExtension> ProjectCreated { get; }
}