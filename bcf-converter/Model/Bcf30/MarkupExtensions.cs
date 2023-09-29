using System.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;

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
  [XmlIgnore] public VisualizationInfo? VisualizationInfo { get; set; }

  public void SetVisualizationInfo(IVisualizationInfo? visInfo) {
    VisualizationInfo = (VisualizationInfo?)visInfo;
  }

  public IVisualizationInfo? GetVisualizationInfo() {
    return VisualizationInfo;
  }

  /// <summary>
  ///   The snapshot image data as base64 encoded string.
  /// </summary>
  [XmlIgnore]
  [JsonProperty("snapshot_data")]
  public string? SnapshotData { get; set; }
}

public partial class VisualizationInfo : IVisualizationInfo { }

public partial class File : IHeaderFile { }

public partial class BimSnippet : IBimSnippet { }

public partial class DocumentReference : IDocReference { }

public partial class Comment : IComment { }