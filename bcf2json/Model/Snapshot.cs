using System;
using System.Buffers.Text;

namespace bcf2json.Model {
  /// <summary>
  ///   A snapshot related to the topic as base64 string.
  /// </summary>
  public struct Snapshot {
    /// <summary>
    ///   The format of the snapshot. Predefined values png or jpg.
    /// </summary>
    public enum Type {
      png,
      jpg
    }

    /// <summary>
    ///   The format of the snapshot. Predefined values png or jpg.
    /// </summary>
    public Type snapshotType;

    /// <summary>
    ///   The snapshot image data as base64 encoded string.
    /// </summary>
    public string snapshotData;

    /// <summary>
    ///   Creates and returns an instance of the Snapshot.
    /// </summary>
    /// <param name="snapshotType">
    ///   The format of the snapshot. Predefined values png or jpg.
    /// </param>
    /// <param name="snapshotData">
    ///   The snapshot image data as base64 encoded string.
    /// </param>
    public Snapshot(Type snapshotType, string snapshotData) {
      this.snapshotType = snapshotType;
      this.snapshotData = snapshotData;
    }
  }
}