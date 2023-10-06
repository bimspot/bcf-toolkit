using System;
using BcfConverter.Model;
using BcfConverter.Model.Bcf21;

namespace BcfConverter.Builder.Bcf21;

public class HeaderFileBuilder : IHeaderFileBuilder<HeaderFileBuilder> {
  private readonly HeaderFile _file = new();

  public HeaderFileBuilder AddIfcProject(string id) {
    _file.IfcProject = id;
    return this;
  }

  public HeaderFileBuilder AddIfcSpatialStructureElement(string id) {
    _file.IfcSpatialStructureElement = id;
    return this;
  }

  public HeaderFileBuilder AddIsExternal(bool isExternal) {
    _file.IsExternal = isExternal;
    return this;
  }

  public HeaderFileBuilder AddFileName(string fileName) {
    _file.Filename = fileName;
    return this;
  }

  public HeaderFileBuilder AddDate(DateTime date) {
    _file.Date = date;
    return this;
  }

  public HeaderFileBuilder AddReference(string reference) {
    _file.Reference = reference;
    return this;
  }

  public IHeaderFile Build() {
    return BuilderUtils.ValidateItem(_file);
  }
}