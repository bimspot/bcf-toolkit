using System;
using BcfToolkit.Builder.Bcf21.Interfaces;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Bcf21;

public class ProjectExtensionBuilder :
  IProjectExtensionBuilder<ProjectExtensionBuilder>,
  IDefaultBuilder<ProjectExtensionBuilder> {
  private readonly ProjectExtension _project = new();

  public ProjectExtensionBuilder() {
    _project.Project = new Project();
  }

  public ProjectExtensionBuilder SetProjectName(string name) {
    _project.Project.Name = name;
    return this;
  }

  public ProjectExtensionBuilder SetProjectId(string id) {
    _project.Project.ProjectId = id;
    return this;
  }

  public ProjectExtensionBuilder SetExtensionSchema(string schema) {
    _project.ExtensionSchema = schema;
    return this;
  }

  public ProjectExtensionBuilder WithDefaults() {
    this
      .SetProjectId(Guid.NewGuid().ToString())
      .SetExtensionSchema("extensions.xsd");
    return this;
  }

  public ProjectExtension Build() {
    return BuilderUtils.ValidateItem(_project);
  }
}