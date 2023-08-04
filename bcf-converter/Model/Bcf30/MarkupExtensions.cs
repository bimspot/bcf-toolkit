using System.Linq;
using System.Xml.Serialization;

namespace bcf.bcf30;

public partial class Markup : IMarkup {
  public ITopic GetTopic() {
    return Topic;
  }

  public IViewPoint? GetFirstViewPoint() {
    return Topic.Viewpoints?.FirstOrDefault();
  }
}

public partial class Topic : ITopic { }

public partial class ViewPoint : IViewPoint {
  [XmlIgnore] 
  public IVisualizationInfo? VisualizationInfo { get; set; }

  /// <summary>
  ///   The snapshot image data as base64 encoded string.
  /// </summary>
  [XmlIgnore]
  [Newtonsoft.Json.JsonProperty("snapshot_data")]
  public string? SnapshotData { get; set; }
}

public partial class VisualizationInfo : IVisualizationInfo {}