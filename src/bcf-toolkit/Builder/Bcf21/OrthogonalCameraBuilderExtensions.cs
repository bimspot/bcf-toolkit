using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Bcf21;

public partial class OrthogonalCameraBuilder {

  public OrthogonalCameraBuilder SetCamera(OrthogonalCamera camera) {

    _camera.CameraViewPoint = camera.CameraViewPoint;
    _camera.CameraDirection = camera.CameraDirection;
    _camera.CameraUpVector = camera.CameraUpVector;
    return this;
  }

}