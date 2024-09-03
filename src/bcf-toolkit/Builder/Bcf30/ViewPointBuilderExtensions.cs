using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

public partial class ViewPointBuilder {

  public ViewPointBuilder SetVisualizationInfo(VisualizationInfo visualizationInfo) {
    _viewPoint.VisualizationInfo = visualizationInfo;
    return this;
  }
}