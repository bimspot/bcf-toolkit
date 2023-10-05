using bcf.bcf21;

namespace bcf.Builder.Bcf21;

public partial class ProjectBuilder : IProjectBuilder<ProjectBuilder> {
  private readonly ProjectExtension _project = new();

  public ProjectBuilder AddProjectName(string name) {
    _project.Project.Name = name;
    return this;
  }

  public ProjectBuilder AddProjectId(string id) {
    _project.Project.ProjectId = id;
    return this;
  }

  public IProject Build() {
    return _project;
  }
}