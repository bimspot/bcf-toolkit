using System.Text.Json.Serialization;

namespace bcf_converter.Model {
  /// <summary>
  ///   This element describes a viewpoint using orthogonal camera.
  /// </summary>
  public struct OrthogonalCamera {
    /// <summary>
    ///   Camera location
    /// </summary>
    [Newtonsoft.Json.JsonProperty("camera_view_point")]
    public Vector3 cameraViewPoint;

    /// <summary>
    ///   Camera direction
    /// </summary>
    [Newtonsoft.Json.JsonProperty("camera_direction")]
    public Vector3 cameraDirection;

    /// <summary>
    ///   Camera up vector
    /// </summary>
    [Newtonsoft.Json.JsonProperty("camera_up_vector")]
    public Vector3 cameraUpVector;

    /// <summary>
    ///   Scaling from view to world
    /// </summary>
    [Newtonsoft.Json.JsonProperty("view_to_world_scale")]
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