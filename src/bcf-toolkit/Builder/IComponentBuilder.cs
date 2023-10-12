using BcfToolkit.Model;

namespace BcfToolkit.Builder;

public interface IComponentBuilder<out TBuilder> : IBuilder<IComponent> {
  /// <summary>
  ///   Returns the builder object set with the `IfcGuid`.
  /// </summary>
  /// <param name="guid">The `IfcGuid` of the component.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetIfcGuid(string guid);
  /// <summary>
  ///   Returns the builder object set with the `OriginatingSystem`.
  /// </summary>
  /// <param name="system">
  ///   Name of the system in which the component is originated.
  /// </param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetOriginatingSystem(string system);
  /// <summary>
  ///   Returns the builder object set with the `AuthoringToolId`.
  /// </summary>
  /// <param name="id">
  ///   System specific identifier of the component in the originating BIM tool.
  /// </param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetAuthoringToolId(string id);
}