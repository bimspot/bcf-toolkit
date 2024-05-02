using System;
using BcfToolkit.Builder.Bcf21.Interfaces;
using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Bcf21;

public class HeaderFileBuilder : IHeaderFileBuilder<HeaderFileBuilder> {
  private readonly HeaderFile _file = new();

  public HeaderFileBuilder SetIfcProject(string id) {
    _file.IfcProject = id;
    return this;
  }

  public HeaderFileBuilder SetIfcSpatialStructureElement(string id) {
    _file.IfcSpatialStructureElement = id;
    return this;
  }

  public HeaderFileBuilder SetIsExternal(bool isExternal) {
    _file.IsExternal = isExternal;
    return this;
  }

  public HeaderFileBuilder SetFileName(string fileName) {
    _file.Filename = fileName;
    return this;
  }

  public HeaderFileBuilder SetDate(DateTime date) {
    _file.Date = date;
    return this;
  }

  public HeaderFileBuilder SetReference(string reference) {
    _file.Reference = reference;
    return this;
  }

  public HeaderFile Build() {
    return BuilderUtils.ValidateItem(_file);
  }
}