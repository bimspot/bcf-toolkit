using System.Text.Json.Serialization;

namespace bcf_converter.Model {
  /// <summary>
  ///   This element describes a viewpoint using perspective camera.
  /// </summary>
  public struct PerspectiveCamera {
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
    ///   Camera’s field of view angle in degrees.
    /// </summary>
    [Newtonsoft.Json.JsonProperty("field_of_view")]
    public float fieldOfView;

    /// <summary>
    ///   Creates and returns an instance of the PerspectiveCamera.
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