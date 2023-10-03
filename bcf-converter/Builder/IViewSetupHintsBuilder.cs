namespace bcf.Builder; 

public interface IViewSetupHintsBuilder<out TBuilder> : IBuilder<IViewSetupHints> {
  TBuilder AddSpaceVisible(bool spaceVisible);
  TBuilder AddSpaceBoundariesVisible(bool spaceBoundariesVisible);
  TBuilder AddOpeningVisible(bool openingVisible);
}