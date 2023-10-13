using System;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Bcf21;

public partial class ProjectBuilder :
  IProjectBuilder<ProjectBuilder>,
  IDefaultBuilder<ProjectBuilder> {
  private readonly ProjectExtension _project = new();

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
    this
      .SetProjectId(Guid.NewGuid().ToString())
      .AddExtensionSchema("extensions.xsd");
    return this;
  }

  public IProject Build() {
    return BuilderUtils.ValidateItem(_project);
  }
}