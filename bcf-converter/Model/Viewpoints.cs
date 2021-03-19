using System.Xml.Serialization;

namespace bcf_converter.Model {
  /// <summary>
  ///   The Viewpoints struct wraps the Viewpoint and the related Snapshot
  ///   data.
  /// </summary>
  public struct Viewpoints {
    public Viewpoint? viewpoint;
    [XmlIgnore] public Snapshot? snapshot;

    public Viewpoints(Viewpoint? viewpoint, Snapshot? snapshot) {
      this.viewpoint = viewpoint;
      this.snapshot = snapshot;
    }
  }
}