using System;
using BcfToolkit.Builder.Bcf21.Interfaces;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Bcf21;

public class ProjectExtensionBuilder :
  IProjectExtensionBuilder<ProjectExtensionBuilder>,
  IDefaultBuilder<ProjectExtensionBuilder> {
  private readonly ProjectExtension _project = new();

  public ProjectExtensionBuilder SetProjectName(string name) {
    _project.GetProjectInstance().Name = name;
    return this;
  }

  public ProjectExtensionBuilder SetProjectId(string id) {
    _project.GetProjectInstance().ProjectId = id;
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