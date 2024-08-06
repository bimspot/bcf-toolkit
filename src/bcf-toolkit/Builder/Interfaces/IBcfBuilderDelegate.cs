using BcfToolkit.Model;
using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Interfaces;

public interface IBcfBuilderDelegate {
  public delegate void OnMarkupCreated<in TMarkup>(TMarkup markup)
    where TMarkup : IMarkup;

  public delegate void
    OnProjectCreated<in TProject>(ProjectExtension projectInfo)
    where TProject : IProject;

  public OnMarkupCreated<IMarkup> MarkupCreated { get; }

  public OnProjectCreated<IProject> ProjectCreated { get; }
}