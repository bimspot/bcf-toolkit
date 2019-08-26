namespace bcf2json.Model {
  /// <summary>
  ///   A vector with coordinated in tree dimensions.
  /// </summary>
  public struct Vector3 {
    public float x;
    public float y;
    public float z;

    /// <summary>
    ///   Creates and returns an instance of the Vector.
    /// </summary>
    public Vector3(float x, float y, float z) {
      this.x = x;
      this.y = y;
      this.z = z;
    }
  }
}