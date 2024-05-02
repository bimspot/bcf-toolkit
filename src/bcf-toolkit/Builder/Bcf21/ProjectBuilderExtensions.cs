using BcfToolkit.Builder.Bcf21.Interfaces;

namespace BcfToolkit.Builder.Bcf21;

public partial class ProjectBuilder : IProjectBuilderExtension<ProjectBuilder> {
  public ProjectBuilder AddExtensionSchema(string schema) {
    _project.ExtensionSchema = schema;
    return this;
  }
}