using System.Collections.Generic;

namespace bcf2json.Model {
  public struct Viewpoint {
    /// <summary>
    ///   Describes a viewpoint using perspective camera.
    /// </summary>
    public PerspectiveCamera perspectiveCamera;
    
    /// <summary>
    ///   Describes a viewpoint using orthogonal camera.
    /// </summary>
    public OrthogonalCamera orthogonalCamera;
    
    /// <summary>
    ///   The components node contains a set of Component references.
    /// </summary>
    public Components components;

    /// <summary>
    ///   ClippingPlanes can be used to define a subsection of a building model
    ///   that is related to the topic. Each clipping plane is defined by
    ///   Location and Direction.
    /// </summary>
    public List<ClippingPlane> clippingPlanes;
  }
}