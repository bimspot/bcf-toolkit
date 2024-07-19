using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using BcfToolkit.Model.Interfaces;
using Newtonsoft.Json;

namespace BcfToolkit.Model.Bcf30;

public partial class Markup : IMarkup {
  public ITopic GetTopic() {
    return Topic;
  }

  public IViewPoint? GetFirstViewPoint() {
    return Topic.Viewpoints?.FirstOrDefault();
  }

  public void SetViewPoints<TVisualizationInfo>(
    Dictionary<string, TVisualizationInfo>? visInfos,
    Dictionary<string, FileData>? snapshots) where TVisualizationInfo : IVisualizationInfo {
    this.Topic.Viewpoints.ToList().ForEach(viewPoint => {
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

  // This method that controls the access to the `Header` instance.
  // On the first run, it creates an instance of the object. On subsequent runs,
  // it returns the existing object.
  public Header GetHeaderInstance() {
    return Header ??= new Header();
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
  public FileData? SnapshotData { get; set; }
}
public partial class File : IHeaderFile { }
public partial class BimSnippet : IBimSnippet { }
public partial class DocumentReference : IDocReference { }

public partial class Comment : IComment {
  // This method that controls the access to the `CommentViewpoint` instance.
  // On the first run, it creates an instance of the object. On subsequent runs,
  // it returns the existing object.
  public CommentViewpoint GetCommentViewPointInstance() {
    return Viewpoint ??= new CommentViewpoint();
  }
}