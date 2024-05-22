using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Bcf21;

public class ViewPointBuilder {
  private readonly ViewPoint _viewPoint = new();

  public ViewPointBuilder SetVisualizationInfo(Model.Bcf21.VisualizationInfo? visualizationInfo) {
    _viewPoint.VisualizationInfo = visualizationInfo;
    return this;
  }

  public ViewPointBuilder SetSnapshot(string snapshot) {
    _viewPoint.Snapshot = snapshot;
    return this;
  }

  public ViewPointBuilder SetIndex(int index) {
    _viewPoint.Index = index;
    return this;
  }

  public ViewPointBuilder SetGuid(string guid) {
    _viewPoint.Guid = guid;
    return this;
  }

  public ViewPointBuilder SetSnapshotData(string? snapshotData) {
    _viewPoint.SnapshotData = snapshotData;
    return this;
  }

  public ViewPoint Build() {
    return BuilderUtils.ValidateItem(_viewPoint);
  }
}