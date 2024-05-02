using System;
using BcfToolkit.Builder.Bcf30.Interfaces;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

public class FileBuilder : IFileBuilder<FileBuilder> {
  private readonly File _file = new();

  public FileBuilder SetIfcProject(string id) {
    _file.IfcProject = id;
    return this;
  }

  public FileBuilder SetIfcSpatialStructureElement(string id) {
    _file.IfcSpatialStructureElement = id;
    return this;
  }

  public FileBuilder SetIsExternal(bool isExternal) {
    _file.IsExternal = isExternal;
    return this;
  }

  public FileBuilder SetFileName(string fileName) {
    _file.Filename = fileName;
    return this;
  }

  public FileBuilder SetDate(DateTime date) {
    _file.Date = date;
    return this;
  }

  public FileBuilder SetReference(string reference) {
    _file.Reference = reference;
    return this;
  }

  public File Build() {
    return BuilderUtils.ValidateItem(_file);
  }
}