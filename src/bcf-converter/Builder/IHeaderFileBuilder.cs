using System;
using BcfConverter.Model;

namespace BcfConverter.Builder;

public interface IHeaderFileBuilder<out TBuilder> : IBuilder<IHeaderFile> {
  TBuilder AddIfcProject(string id);
  TBuilder AddIfcSpatialStructureElement(string id);
  TBuilder AddIsExternal(bool isExternal);
  TBuilder AddFileName(string fileName);
  TBuilder AddDate(DateTime date);
  TBuilder AddReference(string reference);
}