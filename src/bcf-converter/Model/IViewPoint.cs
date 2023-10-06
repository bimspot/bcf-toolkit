namespace BcfConverter.Model;

public interface IViewPoint {
  public string? SnapshotData { get; set; }
  public string Snapshot { get; set; }
  public void SetVisualizationInfo(IVisualizationInfo? visInfo);
  public IVisualizationInfo? GetVisualizationInfo();
}