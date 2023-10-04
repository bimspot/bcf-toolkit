using System;

namespace bcf.Builder.Bcf21; 

public interface IViewPointBuilderExtension<out TBuilder, out TViewSetupHintsBuilder>  {
  TBuilder AddViewSetupHints(Action<TViewSetupHintsBuilder> builder);
}