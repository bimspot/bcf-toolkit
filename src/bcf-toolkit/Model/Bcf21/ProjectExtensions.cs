namespace BcfToolkit.Model.Bcf21;

public partial class ProjectExtension : IProject {
  // This method that controls the access to the `Project` instance.
  // On the first run, it creates an instance of the object. On subsequent runs,
  // it returns the existing object.
  public Project GetProjectInstance() {
    return Project ??= new Project();
  }
}