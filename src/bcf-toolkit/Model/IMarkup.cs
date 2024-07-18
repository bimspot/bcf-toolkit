using System.Collections.Generic;

namespace BcfToolkit.Model;

public interface IMarkup {
  public ITopic? GetTopic();
  public IViewPoint? GetFirstViewPoint();

  public void SetViewPoints<TVisualizationInfo>(
    Dictionary<string, TVisualizationInfo>? visInfos,
    Dictionary<string, string>? snapshots) where TVisualizationInfo : IVisualizationInfo;
}