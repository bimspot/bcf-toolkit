namespace bcf_converter.Model {
  public struct Header {
    
    /// <summary>
    /// IfcGuid Reference to the project to which this topic is related in
    /// the IFC file
    /// </summary>
    public string ifcProject;
    
    /// <summary>
    /// IfcGuid Reference to the spatial structure element, e.g.
    /// IfcBuildingStorey, to which this topic is related.
    /// </summary>
    public string ifcSpatialStructureElement;

    /// <summary>
    /// Is the IFC file external or within the bcfzip. (Default = true).
    /// </summary>
    public bool isExternal;

    public Header(
      string ifcProject = null, 
      string ifcSpatialStructureElement = null,
      bool isExternal = true) {
      this.ifcProject = ifcProject;
      this.ifcSpatialStructureElement = ifcSpatialStructureElement;
      this.isExternal = isExternal;
    }
  }
}