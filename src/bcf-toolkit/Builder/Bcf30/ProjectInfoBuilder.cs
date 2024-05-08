using System;
using BcfToolkit.Builder.Bcf30.Interfaces;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

public class ProjectInfoBuilder :
  IProjectInfoBuilder<ProjectInfoBuilder>,
  IDefaultBuilder<ProjectInfoBuilder> {
  private readonly ProjectInfo _project = new();

  public ProjectInfoBuilder() {
    _project.Project = new Project();
  }

  public ProjectInfoBuilder SetProjectName(string name) {
    _project.Project.Name = name;
    return this;
  }

  public ProjectInfoBuilder SetProjectId(string id) {
    _project.Project.ProjectId = id;
    return this;
  }

  public ProjectInfoBuilder WithDefaults() {
    this.SetProjectId(Guid.NewGuid().ToString());
    return this;
  }

  public ProjectInfo Build() {
    return BuilderUtils.ValidateItem(_project);
  }
}