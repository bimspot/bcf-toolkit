using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace BcfToolkit.Model.Bcf21;

public partial class Markup : IMarkup {
  public ITopic GetTopic() {
    return Topic;
  }

  public IViewPoint? GetFirstViewPoint() {
    return Viewpoints?.FirstOrDefault();
  }

  public void SetViewPoints<TVisualizationInfo>(
    Dictionary<string, TVisualizationInfo>? visInfos,
    Dictionary<string, string>? snapshots) where TVisualizationInfo : IVisualizationInfo {
    this.Viewpoints.ToList().ForEach(viewPoint => {
      if (visInfos is not null) {
        visInfos.TryGetValue(viewPoint.Viewpoint, out var visInfo);
        if (visInfo is not null && visInfo is VisualizationInfo visualizationInfo)
          viewPoint.VisualizationInfo = visualizationInfo;
      }
      if (snapshots is not null) {
        snapshots.TryGetValue(viewPoint.Snapshot, out var snapshot);
        if (snapshot is not null)
          viewPoint.SnapshotData = snapshot;
      }
    });
  }

  public void SetViewPoint(
    Dictionary<string, IVisualizationInfo> visInfos,
    Dictionary<string, string> snapshots) {
    this.Viewpoints.ToList().ForEach(viewPoint => {
      visInfos.TryGetValue(viewPoint.Viewpoint, out var visInfo);
      if (visInfo is not null)
        viewPoint.VisualizationInfo = (VisualizationInfo)visInfo;
      snapshots.TryGetValue(viewPoint.Snapshot, out var snapshot);
      if (snapshot is not null)
        viewPoint.SnapshotData = snapshot;
    });
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

public partial class HeaderFile : IHeaderFile { }
public partial class BimSnippet : IBimSnippet { }
public partial class TopicDocumentReference : IDocReference { }

public partial class Comment : IComment {
  // This method that controls the access to the `CommentViewpoint` instance.
  // On the first run, it creates an instance of the object. On subsequent runs,
  // it returns the existing object.
  public CommentViewpoint GetCommentViewPointInstance() {
    return Viewpoint ??= new CommentViewpoint();
  }
}