using System;
using System.Collections.Generic;
using System.Linq;
using BcfToolkit.Builder.Bcf30;

namespace BcfToolkit.Converter.Bcf21;

public static class SchemaConverterToBcf30 {

  /// <summary>
  ///   This method translates the specified object with a BCF version to the
  ///   desired one.
  ///   From:   [BCF 2.1]
  ///   To:     [BCF 3.0]
  /// </summary>
  /// <param name="from">The object which must be converted.</param>
  /// <returns>Returns the converted object.</returns>
  public static Model.Bcf30.Bcf Convert(Model.Bcf21.Bcf from) {
    var builder = new BcfBuilder();
    return builder
      .AddMarkups(from.Markups.Select(ConvertMarkup).ToList(), true)
      .SetDocumentInfo(UpdateDocumentInfo(from.Markups
        .SelectMany(m => m.Topic.DocumentReference)
        .Where(r => !r.IsExternal)
        .ToList()))
      .SetProject(p => p.WithDefaults()) // TODO: convert project
      .Build();
  }

  private static Model.Bcf30.Markup ConvertMarkup(Model.Bcf21.Markup from) {
    var builder = new MarkupBuilder();
    var topic = from.Topic;
    builder
      .AddHeaderFiles(from.Header.Select(ConvertHeaderFile).ToList())
      .AddReferenceLinks(topic.ReferenceLink.ToList())
      .SetTitle(topic.Title)
      .SetPriority(topic.Priority)
      .SetIndex(topic.Index)
      .AddLabels(topic.Labels.ToList())
      .SetCreationDate(topic.CreationDate)
      .SetCreationAuthor(topic.CreationAuthor)
      .SetModifiedDate(topic.ModifiedDate)
      .SetModifiedAuthor(topic.ModifiedAuthor)
      .SetDueDate(topic.DueDate)
      .SetAssignedTo(topic.AssignedTo)
      .SetStage(topic.Stage)
      .SetDescription(topic.Description)
      .AddRelatedTopics(topic.RelatedTopic.Select(t => t.Guid).ToList())
      .AddComments(from.Comment.Select(ConvertComment).ToList())
      .AddViewPoints(from.Viewpoints.Select(ConvertViewPoint).ToList())
      .SetGuid(from.Topic.Guid)
      .SetTopicType(from.Topic.TopicType ??= "ERROR")
      .SetTopicStatus(from.Topic.TopicStatus ??= "OPEN")
      .AddDocumentReferences(from.Topic.DocumentReference
        .Select(ConvertDocumentReference).ToList());

    var bimSnippet = topic.BimSnippet;
    if (bimSnippet != null)
      builder
        .SetBimSnippet(b => b
          .SetReference(bimSnippet.Reference)
          .SetReferenceSchema(bimSnippet.ReferenceSchema)
          .SetSnippetType(bimSnippet.SnippetType)
          .SetIsExternal(bimSnippet.IsExternal));

    return builder.Build();
  }

  private static Model.Bcf30.File ConvertHeaderFile(Model.Bcf21.HeaderFile from) {
    var builder = new FileBuilder();
    return builder
      .SetFileName(from.Filename)
      .SetDate(from.Date)
      .SetReference(from.Reference)
      .SetIfcProject(from.IfcProject)
      .SetIfcSpatialStructureElement(from.IfcSpatialStructureElement)
      .SetIsExternal(from.IsExternal)
      .Build();
  }

  private static Model.Bcf30.DocumentReference ConvertDocumentReference(
    Model.Bcf21.TopicDocumentReference from) {
    var builder = new DocumentReferenceBuilder();
    return builder
      .SetDescription(from.Description)
      .SetGuid(from.Guid ??= Guid.NewGuid().ToString()) //TODO: generate guid based on description
      .SetUrl(from.IsExternal ? from.ReferencedDocument : null)
      .SetDocumentGuid(from.IsExternal ? null : Guid.NewGuid().ToString()) //TODO: generate guid based on guid and description
      .Build();
  }

  private static Model.Bcf30.Comment ConvertComment(Model.Bcf21.Comment from) {
    var builder = new CommentBuilder();
    builder
      .SetDate(from.Date)
      .SetAuthor(from.Author)
      .SetCommentProperty(from.CommentProperty)
      .SetModifiedDate(from.ModifiedDate)
      .SetModifiedAuthor(from.ModifiedAuthor)
      .SetGuid(from.Guid);

    if (from.Viewpoint != null)
      builder.SetViewPoint(from.Viewpoint?.Guid);

    return builder.Build();
  }

  private static Model.Bcf30.ViewPoint ConvertViewPoint(
    Model.Bcf21.ViewPoint from) {
    return new Model.Bcf30.ViewPoint {
      Viewpoint = from.Viewpoint,
      Snapshot = from.Snapshot,
      SnapshotData = from.SnapshotData,
      Index = from.Index,
      Guid = from.Guid,
      VisualizationInfo = ConvertVisualizationInfo(from.VisualizationInfo)
    };
  }

  private static Model.Bcf30.VisualizationInfo? ConvertVisualizationInfo(
    Model.Bcf21.VisualizationInfo? from) {
    if (from == null) return null;

    var builder = new VisualizationInfoBuilder();
    var components = from.Components;
    var selection = components?.Selection;
    var viewSetupHints = components?.ViewSetupHints;
    var visibility = components?.Visibility;
    var coloring = components?.Coloring;
    var lines = from.Lines;
    var clippingPlanes = from.ClippingPlanes;
    var bitmaps = from.Bitmap;
    var orthoCamera = from.OrthogonalCamera;
    var perspCamera = from.PerspectiveCamera;

    builder
      .AddSelections(selection?.Select(ConvertComponent).ToList())
      .AddColorings(coloring?.Select(ConvertColor).ToList())
      .AddLines(lines?.Select(ConvertLine).ToList())
      .AddClippingPlanes(clippingPlanes?.Select(ConvertClippingPlane).ToList())
      .AddBitmaps(bitmaps?.Select(ConvertBitmap).ToList())
      .SetGuid(from.Guid);

    if (visibility != null)
      builder
        .SetVisibility(vis => {
          vis
            .AddExceptions(visibility.Exceptions
              .Select(ConvertComponent)
              .ToList())
            .SetDefaultVisibility(visibility.DefaultVisibility);
          if (viewSetupHints != null)
            vis
              .SetViewSetupHints(hints => hints
                .SetSpaceVisible(viewSetupHints.SpacesVisible)
                .SetSpaceBoundariesVisible(
                  viewSetupHints.SpaceBoundariesVisible)
                .SetOpeningVisible(viewSetupHints.OpeningsVisible));
        });
    if (orthoCamera != null)
      builder.SetOrthogonalCamera(oC => oC
        .SetCamera(c => c
          .SetViewPoint(
            orthoCamera.CameraViewPoint.X,
            orthoCamera.CameraViewPoint.Y,
            orthoCamera.CameraViewPoint.Z)
          .SetDirection(
            orthoCamera.CameraDirection.X,
            orthoCamera.CameraDirection.Y,
            orthoCamera.CameraDirection.Z)
          .SetUpVector(
            orthoCamera.CameraUpVector.X,
            orthoCamera.CameraUpVector.Y,
            orthoCamera.CameraUpVector.Z))
        .SetViewToWorldScale(orthoCamera.ViewToWorldScale)
        .SetAspectRatio(1.0)); //by default

    if (perspCamera != null)
      builder.SetPerspectiveCamera(pC => pC
        .SetCamera(c => c
          .SetViewPoint(
            perspCamera.CameraViewPoint.X,
            perspCamera.CameraViewPoint.Y,
            perspCamera.CameraViewPoint.Z)
          .SetDirection(
            perspCamera.CameraDirection.X,
            perspCamera.CameraDirection.Y,
            perspCamera.CameraDirection.Z)
          .SetUpVector(
            perspCamera.CameraUpVector.X,
            perspCamera.CameraUpVector.Y,
            perspCamera.CameraUpVector.Z))
        .SetFieldOfView(perspCamera.FieldOfView)
        .SetAspectRatio(1.0)); //by default

    return builder.Build();
  }

  private static Model.Bcf30.Component ConvertComponent(
    Model.Bcf21.Component from) {
    var builder = new ComponentBuilder();
    return builder
      .SetOriginatingSystem(from.OriginatingSystem)
      .SetAuthoringToolId(from.AuthoringToolId)
      .SetIfcGuid(from.IfcGuid)
      .Build();
  }

  private static Model.Bcf30.ComponentColoringColor ConvertColor(
    Model.Bcf21.ComponentColoringColor from) {
    var builder = new ColorBuilder();
    return builder
      .AddComponents(from.Component.Select(ConvertComponent).ToList())
      .SetColor(from.Color)
      .Build();
  }

  private static Model.Bcf30.Line ConvertLine(Model.Bcf21.Line from) {
    var builder = new LineBuilder();
    return builder
      .SetStartPoint(
        from.StartPoint.X,
        from.StartPoint.Y,
        from.StartPoint.Z)
      .SetEndPoint(
        from.EndPoint.X,
        from.EndPoint.Y,
        from.EndPoint.Z)
      .Build();
  }

  private static Model.Bcf30.ClippingPlane ConvertClippingPlane(
    Model.Bcf21.ClippingPlane from) {
    var builder = new ClippingPlaneBuilder();
    return builder
      .SetLocation(
        from.Location.X,
        from.Location.Y,
        from.Location.Z)
      .SetDirection(
        from.Direction.X,
        from.Direction.Y,
        from.Direction.Z)
      .Build();
  }

  private static Model.Bcf30.Bitmap ConvertBitmap(
    Model.Bcf21.VisualizationInfoBitmap from) {
    var builder = new BitmapBuilder();
    return builder
      .SetFormat(from.Bitmap.ToString())
      .SetReference(from.Reference)
      .SetLocation(
        from.Location.X,
        from.Location.Y,
        from.Location.Z)
      .SetNormal(
        from.Normal.X,
        from.Normal.Y,
        from.Normal.Z)
      .SetUp(
        from.Up.X,
        from.Up.Y,
        from.Up.Z)
      .SetHeight(from.Height)
      .Build();
  }

  private static Action<DocumentInfoBuilder> UpdateDocumentInfo(
    List<Model.Bcf21.TopicDocumentReference> docReferences) {
    return dI => dI
      .AddDocuments(docReferences.Select(ConvertDocument).ToList());
  }

  private static Model.Bcf30.Document ConvertDocument(
    Model.Bcf21.TopicDocumentReference docReference) {
    var builder = new DocumentBuilder();
    return builder
      .SetFileName(docReference.ReferencedDocument)
      .SetDescription(docReference.Description)
      .SetGuid(docReference.Guid)
      .Build();
  }
}