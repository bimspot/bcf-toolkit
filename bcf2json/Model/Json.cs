using System.Collections.Generic;

namespace bcf2json.Model {
  public struct Json {
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
    ///   A snapshot related to the topic as base64 string.
    /// </summary>
    public Snapshot snapshot;

    /// <summary>
    ///   ClippingPlanes can be used to define a subsection of a building model
    ///   that is related to the topic. Each clipping plane is defined by
    ///   Location and Direction.
    /// </summary>
    public List<ClippingPlane> clippingPlanes;

    /// <summary>
    ///   Creates and returns an instance of the Json representation of the
    ///   BCF data.
    /// </summary>
    /// <param name="perspectiveCamera">
    ///   Describes a viewpoint using perspective camera.
    /// </param>
    /// <param name="orthogonalCamera">
    ///   Describes a viewpoint using orthogonal camera.
    /// </param>
    /// <param name="components">
    ///   The components node contains a set of Component references.
    /// </param>
    /// <param name="snapshot">
    ///   A snapshot related to the topic as base64 string.
    /// </param>
    /// <param name="clippingPlanes">
    ///   Clipping planes for the topic.
    /// </param>
    public Json(PerspectiveCamera perspectiveCamera,
      OrthogonalCamera orthogonalCamera, Components components,
      Snapshot snapshot, List<ClippingPlane> clippingPlanes) {
      this.perspectiveCamera = perspectiveCamera;
      this.orthogonalCamera = orthogonalCamera;
      this.components = components;
      this.snapshot = snapshot;
      this.clippingPlanes = clippingPlanes;
    }
  }
}