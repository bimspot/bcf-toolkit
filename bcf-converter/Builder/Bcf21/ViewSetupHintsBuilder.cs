using bcf.bcf21;

namespace bcf.Builder.Bcf21; 

public class ViewSetupHintsBuilder : 
  IViewSetupHintsBuilder<ViewSetupHintsBuilder> {
  private readonly ViewSetupHints _viewSetupHints = new();
  
  public ViewSetupHintsBuilder AddSpaceVisible(bool spaceVisible) {
    _viewSetupHints.SpacesVisible = spaceVisible;
    return this;
  }

  public ViewSetupHintsBuilder AddSpaceBoundariesVisible(bool spaceBoundariesVisible) {
    _viewSetupHints.SpaceBoundariesVisible = spaceBoundariesVisible;
    return this;
  }

  public ViewSetupHintsBuilder AddOpeningVisible(bool openingVisible) {
    _viewSetupHints.OpeningsVisible = openingVisible;
    return this;
  }
  
  public IViewSetupHints Build() {
    return _viewSetupHints;
  }
}