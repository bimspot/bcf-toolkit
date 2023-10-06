namespace bcf.Builder;

public interface IProjectBuilder<out TBuilder> : IBuilder<IProject> {
  TBuilder AddProjectName(string name);
  TBuilder AddProjectId(string id);
}