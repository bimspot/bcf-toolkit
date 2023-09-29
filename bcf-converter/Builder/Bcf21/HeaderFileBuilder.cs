using System;
using bcf.bcf21;

namespace bcf.Builder.Bcf21;

public class HeaderFileBuilder : IHeaderFileBuilder {
  private readonly HeaderFile _file = new();

  public IHeaderFileBuilder AddIfcProject(string id) {
    _file.IfcProject = id;
    return this;
  }

  public IHeaderFileBuilder AddIfcSpatialStructureElement(string id) {
    _file.IfcSpatialStructureElement = id;
    return this;
  }

  public IHeaderFileBuilder AddIsExternal(bool isExternal) {
    _file.IsExternal = isExternal;
    return this;
  }

  public IHeaderFileBuilder AddFileName(string fileName) {
    _file.Filename = fileName;
    return this;
  }

  public IHeaderFileBuilder AddDate(DateTime date) {
    _file.Date = date;
    return this;
  }

  public IHeaderFileBuilder AddReference(string reference) {
    _file.Reference = reference;
    return this;
  }

  public IHeaderFile Build() {
    return _file;
  }
}