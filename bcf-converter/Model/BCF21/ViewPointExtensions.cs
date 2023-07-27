using System.Xml.Serialization;

namespace bcf.bcf21 {
  public partial class ViewPoint {
    [XmlIgnore] public VisualizationInfo? VisualizationInfo;

    /// <summary>
    ///   The snapshot image data as base64 encoded string.
    /// </summary>
    [XmlIgnore]
    [Newtonsoft.Json.JsonProperty("snapshot_data")]
    public string? SnapshotData { get; set; }
  }
}