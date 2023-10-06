using bcf.bcf30;

namespace bcf.Builder.Bcf30;

public class ProjectBuilder : IProjectBuilder<ProjectBuilder> {
  private readonly ProjectInfo _project = new();

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