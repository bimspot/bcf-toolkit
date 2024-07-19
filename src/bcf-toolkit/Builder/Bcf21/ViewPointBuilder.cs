using System;
using BcfToolkit.Builder.Bcf21.Interfaces;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf21;
using BcfToolkit.Model.Interfaces;

namespace BcfToolkit.Builder.Bcf21;

public class ViewPointBuilder : IViewPointBuilder<
  ViewPointBuilder, VisualizationInfoBuilder> {
  private readonly ViewPoint _viewPoint = new();

  public ViewPointBuilder SetVisualizationInfo(VisualizationInfo? visualizationInfo) {
    _viewPoint.VisualizationInfo = visualizationInfo;
    return this;
  }

  public ViewPointBuilder SetViewPoint(string viewpoint) {
    _viewPoint.Viewpoint = viewpoint;
    return this;
  }

  public ViewPointBuilder SetSnapshot(string snapshot) {
    _viewPoint.Snapshot = snapshot;
    return this;
  }

  public ViewPointBuilder SetIndex(int? index) {
    _viewPoint.Index = index;
    return this;
  }

  public ViewPointBuilder SetVisualizationInfo(
    Action<VisualizationInfoBuilder> builder) {
    var visInfo =
      (VisualizationInfo)BuilderUtils
        .BuildItem<VisualizationInfoBuilder, IVisualizationInfo>(builder);
    _viewPoint.VisualizationInfo = visInfo;
    return this;
  }

  public ViewPointBuilder SetGuid(string guid) {
    _viewPoint.Guid = guid;
    return this;
  }

  public ViewPointBuilder SetSnapshotData(FileData? snapshotData) {
    _viewPoint.SnapshotData = snapshotData;
    return this;
  }

  public ViewPoint Build() {
    return BuilderUtils.ValidateItem(_viewPoint);
  }
}