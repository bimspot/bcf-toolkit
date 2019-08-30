namespace bcf_converter.Model {
  /// <summary>
  ///   This element describes a viewpoint using orthogonal camera.
  /// </summary>
  public struct OrthogonalCamera {
    /// <summary>
    ///   Camera location
    /// </summary>
    public Vector3 cameraViewPoint;

    /// <summary>
    ///   Camera direction
    /// </summary>
    public Vector3 cameraDirection;

    /// <summary>
    ///   Camera up vector
    /// </summary>
    public Vector3 cameraUpVector;

    /// <summary>
    ///   Scaling from view to world
    /// </summary>
    public float viewToWorldScale;

    /// <summary>
    ///   Creates and returns an instance of the PerspectiveCamera.
    /// </summary>
    /// <param name="cameraViewPoint">Camera location</param>
    /// <param name="cameraDirection">Camera direction</param>
    /// <param name="cameraUpVector">Camera up vector</param>
    /// <param name="viewToWorldScale">Scaling from view to world</param>
    public OrthogonalCamera(Vector3 cameraViewPoint, Vector3 cameraDirection,
      Vector3 cameraUpVector, float viewToWorldScale) {
      this.cameraViewPoint = cameraViewPoint;
      this.cameraDirection = cameraDirection;
      this.cameraUpVector = cameraUpVector;
      this.viewToWorldScale = viewToWorldScale;
    }
  }
}