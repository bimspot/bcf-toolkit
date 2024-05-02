using BcfToolkit.Builder.Bcf30.Interfaces;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

public class ViewSetupHintsBuilder :
  IViewSetupHintsBuilder<ViewSetupHintsBuilder> {
  private readonly ViewSetupHints _viewSetupHints = new();

  public ViewSetupHintsBuilder SetSpaceVisible(bool spaceVisible) {
    _viewSetupHints.SpacesVisible = spaceVisible;
    return this;
  }

  public ViewSetupHintsBuilder SetSpaceBoundariesVisible(bool spaceBoundariesVisible) {
    _viewSetupHints.SpaceBoundariesVisible = spaceBoundariesVisible;
    return this;
  }

  public ViewSetupHintsBuilder SetOpeningVisible(bool openingVisible) {
    _viewSetupHints.OpeningsVisible = openingVisible;
    return this;
  }

  public ViewSetupHints Build() {
    return BuilderUtils.ValidateItem(_viewSetupHints);
  }
}