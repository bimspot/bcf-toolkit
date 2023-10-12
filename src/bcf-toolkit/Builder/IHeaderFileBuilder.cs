using System;
using BcfToolkit.Model;

namespace BcfToolkit.Builder;

public interface IHeaderFileBuilder<out TBuilder> : IBuilder<IHeaderFile> {
  /// <summary>
  ///   Returns the builder object set with the `IfcProject`.
  /// </summary>
  /// <param name="id">
  ///   `IfcGuid` Reference to the project to which this topic is related in
  ///   the IFC file.
  /// </param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetIfcProject(string id);
  /// <summary>
  ///   Returns the builder object set with the `IfcSpatialStructureElement`.
  /// </summary>
  /// <param name="id">
  ///   `IfcGuid` IfcGuid Reference to the spatial structure element, e.g.
  ///   IfcBuildingStorey, to which this topic is related.
  /// </param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetIfcSpatialStructureElement(string id);
  /// <summary>
  ///   Returns the builder object set with the `IsExternal`.
  /// </summary>
  /// <param name="isExternal">
  ///   Is the IFC file external or within the bcfzip. (Default = true).
  /// </param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetIsExternal(bool isExternal);
  /// <summary>
  ///   Returns the builder object set with the `FileName`.
  /// </summary>
  /// <param name="fileName">The BIM file related to this topic.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetFileName(string fileName);
  /// <summary>
  ///   Returns the builder object set with the `Date`.
  /// </summary>
  /// <param name="date">Date of the BIM file.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetDate(DateTime date);
  /// <summary>
  ///   Returns the builder object set with the `Reference`.
  /// </summary>
  /// <param name="reference">URI to the Ifc file.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetReference(string reference);
}