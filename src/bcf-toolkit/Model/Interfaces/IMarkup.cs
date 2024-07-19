using System.Collections.Generic;

namespace BcfToolkit.Model.Interfaces;

public interface IMarkup {
  public ITopic? GetTopic();
  public IViewPoint? GetFirstViewPoint();

  public void SetViewPoints<TVisualizationInfo>(
    Dictionary<string, TVisualizationInfo>? visInfos,
    Dictionary<string, FileData>? snapshots) where TVisualizationInfo : IVisualizationInfo;
}