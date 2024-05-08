using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Bcf21.Interfaces;

public interface IProjectExtensionBuilder<out TBuilder> : IBuilder<ProjectExtension> {
  /// <summary>
  ///   Returns the builder object set with the `ProjectName`.
  /// </summary>
  /// <param name="name">Name of the project.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetProjectName(string name);

  /// <summary>
  ///   Returns the builder object set with the `ProjectId`.
  /// </summary>
  /// <param name="id">Id of the project.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetProjectId(string id);

  /// <summary>
  ///   Returns the builder object extended with a new `Schema`.
  /// </summary>
  /// <param name="schema">Extension schema of the project.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetExtensionSchema(string schema);
}