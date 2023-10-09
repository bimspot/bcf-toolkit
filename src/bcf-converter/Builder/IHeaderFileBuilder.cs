using System;
using BcfConverter.Model;

namespace BcfConverter.Builder;

public interface IHeaderFileBuilder<out TBuilder> : IBuilder<IHeaderFile> {
  /// <summary>
  ///   Returns the builder object extended with `IfcProject`.
  /// </summary>
  /// <param name="id">
  ///   `IfcGuid` Reference to the project to which this topic is related in
  ///   the IFC file.
  /// </param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddIfcProject(string id);
  /// <summary>
  ///   Returns the builder object extended with `IfcSpatialStructureElement`.
  /// </summary>
  /// <param name="id">
  ///   `IfcGuid` IfcGuid Reference to the spatial structure element, e.g.
  ///   IfcBuildingStorey, to which this topic is related.
  /// </param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddIfcSpatialStructureElement(string id);
  /// <summary>
  ///   Returns the builder object extended with `IsExternal`.
  /// </summary>
  /// <param name="isExternal">
  ///   Is the IFC file external or within the bcfzip. (Default = true).
  /// </param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddIsExternal(bool isExternal);
  /// <summary>
  ///   Returns the builder object extended with `FileName`.
  /// </summary>
  /// <param name="fileName">The BIM file related to this topic.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddFileName(string fileName);
  /// <summary>
  ///   Returns the builder object extended with `Date`.
  /// </summary>
  /// <param name="date">Date of the BIM file.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddDate(DateTime date);
  /// <summary>
  ///   Returns the builder object extended with `Reference`.
  /// </summary>
  /// <param name="reference">URI to the Ifc file.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder AddReference(string reference);
}