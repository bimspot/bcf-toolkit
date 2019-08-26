namespace bcf2json.Model {
  /// <summary>
  ///   This element describes a viewpoint using perspective camera.
  /// </summary>
  public struct PerspectiveCamera {
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
    ///   Camera’s field of view angle in degrees.
    /// </summary>
    public float fieldOfView;

    /// <summary>
    ///  Creates and returns an instance of the PerspectiveCamera.
    /// </summary>
    /// <param name="cameraViewPoint">Camera location</param>
    /// <param name="cameraDirection">Camera direction</param>
    /// <param name="cameraUpVector">Camera up vector</param>
    /// <param name="fieldOfView">
    ///   Camera’s field of view angle in degrees.
    /// </param>
    public PerspectiveCamera(Vector3 cameraViewPoint, Vector3 cameraDirection,
      Vector3 cameraUpVector, float fieldOfView) {
      this.cameraViewPoint = cameraViewPoint;
      this.cameraDirection = cameraDirection;
      this.cameraUpVector = cameraUpVector;
      this.fieldOfView = fieldOfView;
    }
  }
}