using System;

namespace bcf.Builder;

public interface IHeaderFileBuilder : IBuilder<IHeaderFile> {
  IHeaderFileBuilder AddIfcProject(string id);
  IHeaderFileBuilder AddIfcSpatialStructureElement(string id);
  IHeaderFileBuilder AddIsExternal(bool isExternal);
  IHeaderFileBuilder AddFileName(string fileName);
  IHeaderFileBuilder AddDate(DateTime date);
  IHeaderFileBuilder AddReference(string reference);
}