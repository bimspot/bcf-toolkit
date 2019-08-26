namespace bcf2json.Model {
  
  /// <summary>
  ///   ClippingPlanes can be used to define a subsection of a building model
  ///   that is related to the topic. Each clipping plane is defined by
  ///   Location and Direction.
  /// </summary>
  public struct ClippingPlane {
    
    /// <summary>
    ///   The origin of the clipping plane.
    /// </summary>
    public Vector3 location;
    
    /// <summary>
    ///   The direction of the clipping plane.
    /// </summary>
    public Vector3 direction;

    /// <summary>
    ///    Creates and returns an instance of the ClippingPlane;
    /// </summary>
    /// <param name="location">The origin of the clipping plane.</param>
    /// <param name="direction">The direction of the clipping plane.</param>
    public ClippingPlane(Vector3 location, Vector3 direction) {
      this.location = location;
      this.direction = direction;
    }
  }
}