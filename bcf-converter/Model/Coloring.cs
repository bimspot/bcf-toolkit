using System.Collections.Generic;

namespace bcf_converter.Model {
  /// <summary>
  ///   Coloring describes a list of components that are colored in the specific
  ///   color.
  /// </summary>
  public struct Coloring {
    /// <summary>
    ///   Color of the components
    /// </summary>
    public string color;

    /// <summary>
    ///   A list of components using this color.
    /// </summary>
    public List<Component> components;

    /// <summary>
    ///   Creates and returns an instance of Coloring.
    /// </summary>
    /// <param name="color">Color of the components</param>
    /// <param name="components">A list of components using this color.</param>
    public Coloring(string color, List<Component> components) {
      this.color = color;
      this.components = components;
    }
  }
}