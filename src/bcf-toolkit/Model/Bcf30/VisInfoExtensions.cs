namespace BcfToolkit.Model.Bcf30;

public partial class VisualizationInfo : IVisualizationInfo {
  // This method that controls the access to the `Components` instance.
  // On the first run, it creates an instance of the object. On subsequent runs,
  // it returns the existing object.
  public Components GetComponentsInstance() {
    return Components ??= new Components();
  }
}
public partial class ViewSetupHints : IViewSetupHints { }
public partial class Component : IComponent { }
public partial class ComponentVisibility : IVisibility { }
public partial class ComponentColoringColor : IColor { }
public partial class OrthogonalCamera : IOrthogonalCamera { }
public partial class PerspectiveCamera : IPerspectiveCamera { }
public partial class Line : ILine { }
public partial class ClippingPlane : IClippingPlane { }
public partial class Bitmap : IBitmap { }