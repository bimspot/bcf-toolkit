namespace BcfToolkit.Model.Interfaces;

public interface IViewPoint {
  public FileData? SnapshotData { get; set; }
  public string Snapshot { get; set; }
  public void SetVisualizationInfo(IVisualizationInfo? visInfo);
  public IVisualizationInfo? GetVisualizationInfo();
}