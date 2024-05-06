using System;
using BcfToolkit.Builder.Bcf30.Interfaces;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

public class ProjectBuilder :
  IProjectBuilder<ProjectBuilder>,
  IDefaultBuilder<ProjectBuilder> {
  private readonly ProjectInfo _project = new();

  public ProjectBuilder() {
    _project.Project = new Project();
  }

  public ProjectBuilder SetProjectName(string name) {
    _project.Project.Name = name;
    return this;
  }

  public ProjectBuilder SetProjectId(string id) {
    _project.Project.ProjectId = id;
    return this;
  }

  public ProjectBuilder WithDefaults() {
    this.SetProjectId(Guid.NewGuid().ToString());
    return this;
  }

  public ProjectInfo Build() {
    return BuilderUtils.ValidateItem(_project);
  }
}