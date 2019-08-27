using System.Collections.Generic;
using Newtonsoft.Json;

namespace bcf2json.Model {
  /// <summary>
  ///   The components node contains a set of Component references.
  /// </summary>
  public struct Components {
    /// <summary>
    ///   The Selection element lists all components that should be either
    ///   highlighted or selected when displaying a viewpoint.
    /// </summary>
    public List<Component> selection;

    /// <summary>
    ///   Coloring describes a list of components that are colored in the
    ///   specific color.
    /// </summary>
    [JsonIgnore] // TODO: move this to a custom serializer for xeokit
    public List<Coloring> coloring;

    /// <summary>
    ///   The Visibility element states the components DefaultVisibility and
    ///   lists all Exceptions that apply to specific components.
    /// </summary>
    public Visibility visibility;

    /// <summary>
    ///   Creates and returns an instance of the Components.
    /// </summary>
    /// <param name="selection">
    ///   The Selection element lists all components that should be either
    ///   highlighted or selected when displaying a viewpoint.
    /// </param>
    /// <param name="coloring">
    ///   Coloring describes a list of components that are colored in the
    ///   specific color.
    /// </param>
    /// <param name="visibility">
    ///   The Visibility element states the components DefaultVisibility and
    ///   lists all Exceptions that apply to specific components.
    /// </param>
    public Components(List<Component> selection, List<Coloring> coloring,
      Visibility visibility) {
      this.selection = selection;
      this.coloring = coloring;
      this.visibility = visibility;
    }
  }
}