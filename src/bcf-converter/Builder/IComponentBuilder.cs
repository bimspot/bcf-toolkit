using BcfConverter.Model;

namespace BcfConverter.Builder;

public interface IComponentBuilder<out TBuilder> : IBuilder<IComponent> {
  /// <summary>
  ///   Returns the builder object extended with `IfcGuid`.
  /// </summary>
  /// <param name="guid">The `IfcGuid` of the component.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddIfcGuid(string guid);
  /// <summary>
  ///   Returns the builder object extended with `OriginatingSystem`.
  /// </summary>
  /// <param name="system">Name of the system in which the component is
  /// originated.
  /// </param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddOriginatingSystem(string system);
  /// <summary>
  ///   Returns the builder object extended with `AuthoringToolId`.
  /// </summary>
  /// <param name="id">System specific identifier of the component in the
  /// originating BIM tool.
  /// </param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddAuthoringToolId(string id);
}