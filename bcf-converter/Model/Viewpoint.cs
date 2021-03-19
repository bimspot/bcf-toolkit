using System.Collections.Generic;
using System.Xml.Serialization;

namespace bcf_converter.Model {
  public struct Viewpoint {
    [XmlAttribute] public string guid;

    /// <summary>
    ///   Describes a viewpoint using perspective camera.
    /// </summary>
    [Newtonsoft.Json.JsonProperty("perspective_camera")]
    public PerspectiveCamera? perspectiveCamera;

    /// <summary>
    ///   Describes a viewpoint using orthogonal camera.
    /// </summary>
    /// s
    [Newtonsoft.Json.JsonIgnore] // TODO: Fix the issue witch default empty object.
    [Newtonsoft.Json.JsonProperty("orthogonal_camera")]
    public OrthogonalCamera? orthogonalCamera;

    /// <summary>
    ///   The components node contains a set of Component references.
    /// </summary>
    public Components components;

    /// <summary>
    ///   ClippingPlanes can be used to define a subsection of a building model
    ///   that is related to the topic. Each clipping plane is defined by
    ///   Location and Direction.
    /// </summary>
    [Newtonsoft.Json.JsonProperty("clipping_planes")]
    public List<ClippingPlane> clippingPlanes;
  }
}