using BcfToolkit.Model;

namespace BcfToolkit.Builder;

public interface IProjectBuilder<out TBuilder> : IBuilder<IProject> {
  /// <summary>
  ///   Returns the builder object extended with `ProjectName`.
  /// </summary>
  /// <param name="name">Name of the project.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddProjectName(string name);
  /// <summary>
  ///   Returns the builder object extended with `ProjectId`.
  /// </summary>
  /// <param name="name">Id of the project.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddProjectId(string id);
}