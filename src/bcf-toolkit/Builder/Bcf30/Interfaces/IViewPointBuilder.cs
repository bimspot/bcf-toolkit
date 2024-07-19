using System;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30.Interfaces;

public interface IViewPointBuilder<out TBuilder, out TVisualizationInfoBuilder>
  : IBuilder<ViewPoint> {
  /// <summary>
  ///   Returns the builder object set with the `ViewPoint`, which is the
  ///   file name of the viewpoint (.bcfv).
  /// </summary>
  /// <param name="viewpoint">Viewpoint file name.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetViewPoint(string viewpoint);

  /// <summary>
  ///   Returns the builder object set with the `Snapshot`, which is the
  ///   file name of the snapshot (png or jpeg).
  /// </summary>
  /// <param name="snapshot">Snapshot file name.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetSnapshot(string snapshot);

  /// <summary>
  ///   Returns the builder object set with the `SnapshotData`, which is the
  ///   Base64 string of snapshot data.
  /// </summary>
  /// <param name="snapshotData">Base64 string of snapshot data.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetSnapshotData(FileData snapshotData);

  /// <summary>
  ///   Returns the builder object set with the `Index`, which is the
  ///   Guid of the viewpoint.
  /// </summary>
  /// <param name="index">Guid of the viewpoint.</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetIndex(int index);

  /// <summary>
  ///   Returns the builder object set with the `VisualizationInfo`, which
  ///   contains information of components related to the topic,
  ///   camera settings, and possible markup and clipping information.
  /// </summary>
  /// <param name="builder">The builder for `VisualizationInfo`</param>
  /// <returns>Returns the builder object.</returns>
  TBuilder SetVisualizationInfo(Action<TVisualizationInfoBuilder> builder);
}