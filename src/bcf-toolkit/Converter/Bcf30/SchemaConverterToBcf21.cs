using System.Linq;
using BcfToolkit.Builder.Bcf21;
using BcfToolkit.Model.Bcf21;
using BcfBuilder = BcfToolkit.Builder.Bcf21.BcfBuilder;
using BimSnippetBuilder = BcfToolkit.Builder.Bcf21.BimSnippetBuilder;
using BitmapBuilder = BcfToolkit.Builder.Bcf21.BitmapBuilder;
using CommentBuilder = BcfToolkit.Builder.Bcf21.CommentBuilder;
using ComponentBuilder = BcfToolkit.Builder.Bcf21.ComponentBuilder;
using DocumentReferenceBuilder = BcfToolkit.Builder.Bcf21.DocumentReferenceBuilder;
using LineBuilder = BcfToolkit.Builder.Bcf21.LineBuilder;
using ClippingPlaneBuilder = BcfToolkit.Builder.Bcf21.ClippingPlaneBuilder;
using MarkupBuilder = BcfToolkit.Builder.Bcf21.MarkupBuilder;
using VisibilityBuilder = BcfToolkit.Builder.Bcf21.VisibilityBuilder;
using VisualizationInfoBuilder = BcfToolkit.Builder.Bcf21.VisualizationInfoBuilder;

namespace BcfToolkit.Converter.Bcf30;

public static class SchemaConverterToBcf21 {

  /// <summary>
  ///   This method translates the specified object with a BCF version to the
  ///   desired one.
  ///   From:   [BCF 3.0]
  ///   To:     [BCF 2.1]
  /// </summary>
  /// <param name="from">The object which must be converted.</param>
  /// <returns>Returns the converted object.</returns>
  public static Model.Bcf21.Bcf Convert(Model.Bcf30.Bcf from) {
    var builder = new BcfBuilder();

    return
    builder
      .AddMarkups(from.Markups.Select(ConvertMarkup).ToList())
      .SetProject(ConvertProject(from.Project?.Project))
      .Build();
  }

  private static Model.Bcf21.ProjectExtension?
    ConvertProject(Model.Bcf30.Project? from) {
    var builder = new ProjectExtensionBuilder();

    if (from is null) {
      return null;
    }

    return
      builder
      .SetProjectId(from.ProjectId)
      .SetProjectName(from.Name)
      .SetExtensionSchema("extensions.xsd")
      //TODO: ExtensionSchema is missing from 3.0
      .Build();
  }

  private static Model.Bcf21.Markup ConvertMarkup(Model.Bcf30.Markup from) {
    var builder = new MarkupBuilder();

    return
    builder
      .SetGuid(from.Topic.Guid)
      .SetTopicType(from.Topic.TopicType)
      .SetTopicStatus(from.Topic.TopicStatus)
      .AddHeaderFiles(from.Header.Files.Select(ConvertHeaderFile).ToList())
      .AddReferenceLinks(from.Topic.ReferenceLinks.ToList())
      .SetTitle(from.Topic.Title)
      .SetPriority(from.Topic.Priority)
      .SetIndex(from.Topic.Index)
      .AddLabels(from.Topic.Labels.ToList())
      .SetCreationDate(from.Topic.CreationDate)
      .SetCreationAuthor(from.Topic.CreationAuthor)
      .SetModifiedDate(from.Topic.ModifiedDate)
      .SetModifiedAuthor(from.Topic.ModifiedAuthor)
      .SetDueDate(from.Topic.DueDate)
      .SetAssignedTo(from.Topic.AssignedTo)
      .SetDescription(from.Topic.Description)
      .SetStage(from.Topic.Stage)
      .SetBimSnippet(ConvertBimSnippet(from.Topic.BimSnippet))
      .AddDocumentReferences(from.Topic.DocumentReferences
        .Select(ConvertDocumentReference).ToList())
      .AddComments(from.Topic.Comments.Select(ConvertComment).ToList())
      .AddViewPoints(from.Topic.Viewpoints.Select(ConvertViewPoint).ToList())
      .AddRelatedTopics(from.Topic.RelatedTopics.Select(ConvertRelatedTopic).ToList())
      .Build();

    return builder.Build();
  }

  private static Model.Bcf21.HeaderFile ConvertHeaderFile(Model.Bcf30.File from) {
    var builder = new HeaderFileBuilder();

    return
    builder
      .SetIfcProject(from.IfcProject)
      .SetIfcSpatialStructureElement(from.IfcSpatialStructureElement)
      .SetIsExternal(from.IsExternal)
      .SetFileName(from.Filename)
      .SetDate(from.Date)
      .SetReference(from.Reference)
      .Build();
  }

  private static Model.Bcf21.BimSnippet? ConvertBimSnippet(Model.Bcf30.BimSnippet? from) {
    if (from is null) {
      return null;
    }

    var builder = new BimSnippetBuilder();
    return
    builder
      .SetSnippetType(from.SnippetType)
      .SetIsExternal(from.IsExternal)
      .SetReference(from.Reference)
      .SetReferenceSchema(from.ReferenceSchema)
      .Build();
  }

  private static Model.Bcf21.TopicDocumentReference ConvertDocumentReference(
    Model.Bcf30.DocumentReference from) {
    var builder = new DocumentReferenceBuilder();
    var isExternal = string.IsNullOrEmpty(from.Url);

    return
    builder
      .SetGuid(from.Guid)
      .SetDescription(from.Description)
      .SetIsExternal(isExternal)
      .SetReferencedDocument(isExternal ? from.Url : from.DocumentGuid)
      .Build();
  }

  private static Model.Bcf21.Comment ConvertComment(Model.Bcf30.Comment from) {
    var builder = new CommentBuilder();

    builder
      .SetGuid(from.Guid)
      .SetDate(from.Date)
      .SetAuthor(from.Author)
      // CommentProperty is required in 2.1, however not in 3.0
      .SetCommentProperty(from.CommentProperty ?? "Not defined")
      .SetModifiedDate(from.ModifiedDate)
      .SetModifiedAuthor(from.ModifiedAuthor);


    if (from.Viewpoint is not null) {
      builder.SetViewPointGuid(from.Viewpoint.Guid);
    }

    return builder.Build();
  }

  private static Model.Bcf21.ViewPoint ConvertViewPoint(
    Model.Bcf30.ViewPoint from) {

    var builder = new ViewPointBuilder();

    return
      builder
        .SetVisualizationInfo(ConvertVisualizationInfo(from.VisualizationInfo))
        .SetSnapshot(from.Snapshot)
        .SetIndex(from.Index)
        .SetGuid(from.Guid)
        .SetSnapshotData(from.SnapshotData)
        .Build();
  }

  private static Model.Bcf21.VisualizationInfo? ConvertVisualizationInfo(
    Model.Bcf30.VisualizationInfo? from) {
    if (from is null)
      return null;

    var builder = new VisualizationInfoBuilder();

    return
    builder
      .SetGuid(from.Guid)
      .AddSelections(
        from.Components.Selection.Select(ConvertComponent).ToList())
      .SetVisibility(ConvertVisibility(from.Components.Visibility))
      .AddBitmaps(from.Bitmaps.Select(ConvertBitmap).ToList())
      .AddColorings(from.Components.Coloring.Select(ConvertColoring).ToList())
      .AddLines(from.Lines.Select(ConvertLine).ToList())
      .AddClippingPlanes(from.ClippingPlanes.Select(ConvertClippingPlane)
        .ToList())
      .SetOrthogonalCamera(ConvertOrthogonalCamera(from.OrthogonalCamera))
      .SetPerspectiveCamera(ConvertPerspectiveCamera(from.PerspectiveCamera))
      .Build();
    //TODO: SetViewSetupHints data loss
  }

  private static Model.Bcf21.OrthogonalCamera? ConvertOrthogonalCamera(
    Model.Bcf30.OrthogonalCamera? from) {
    if (from is null)
      return null;

    var builder = new OrthogonalCameraBuilder();

    return
      builder
        .SetCameraDirection(from.CameraDirection.X, from.CameraDirection.Y, from.CameraDirection.Z)
        .SetCameraViewPoint(from.CameraViewPoint.X, from.CameraViewPoint.Y, from.CameraViewPoint.Z)
        .SetCameraUpVector(from.CameraUpVector.X, from.CameraUpVector.Y, from.CameraUpVector.Z)
        .SetViewToWorldScale(from.ViewToWorldScale)
        .Build();
    //TODO: AspectRatio data loss

  }
  private static Model.Bcf21.PerspectiveCamera? ConvertPerspectiveCamera(
    Model.Bcf30.PerspectiveCamera? from) {
    if (from is null) {
      return null;
    }

    var builder = new PerspectiveCameraBuilder();

    return
      builder
        .SetCameraDirection(from.CameraDirection.X, from.CameraDirection.Y, from.CameraDirection.Z)
        .SetCameraViewPoint(from.CameraViewPoint.X, from.CameraViewPoint.Y, from.CameraViewPoint.Z)
        .SetCameraUpVector(from.CameraUpVector.X, from.CameraUpVector.Y, from.CameraUpVector.Z)
        .SetFieldOfView(from.FieldOfView)
        .Build();
    //TODO: AspectRatio data loss
  }

  private static Model.Bcf21.ClippingPlane ConvertClippingPlane(
    Model.Bcf30.ClippingPlane from) {
    var builder = new ClippingPlaneBuilder();

    return
    builder
      .SetLocation(from.Location.X, from.Location.Y, from.Location.Z)
      .SetDirection(from.Direction.X, from.Direction.Y, from.Direction.Z)
      .Build();
  }

  private static Model.Bcf21.Line ConvertLine(Model.Bcf30.Line from) {

    var builder = new LineBuilder();

    return
    builder
      .SetStartPoint(from.StartPoint.X, from.StartPoint.Y, from.StartPoint.Z)
      .SetEndPoint(from.EndPoint.X, from.EndPoint.Y, from.EndPoint.Z)
      .Build();
  }

  private static Model.Bcf21.ComponentColoringColor ConvertColoring(
    Model.Bcf30.ComponentColoringColor from) {
    var builder = new ComponentColoringColorBuilder();

    return
    builder
      .SetColor(from.Color)
      .AddComponents(from.Components.Select(ConvertComponent).ToList())
      .Build();
  }

  private static Model.Bcf21.VisualizationInfoBitmap ConvertBitmap(
    Model.Bcf30.Bitmap from) {
    var builder = new BitmapBuilder();

    return
    builder
      .SetFormat(from.Format.ToString())
      .SetReference(from.Reference)
      .SetLocation(from.Location.X, from.Location.Y, from.Location.Z)
      .SetHeight(from.Height)
      .SetNormal(from.Normal.X, from.Normal.Y, from.Normal.Z)
      .SetUp(from.Up.X, from.Up.Y, from.Up.Z)
      .Build();
  }

  private static Model.Bcf21.Component ConvertComponent(
    Model.Bcf30.Component from) {
    var builder = new ComponentBuilder();

    return
    builder
      .SetIfcGuid(from.IfcGuid)
      .SetOriginatingSystem(from.OriginatingSystem)
      .SetAuthoringToolId(from.AuthoringToolId)
      .Build();
  }

  private static Model.Bcf21.ComponentVisibility ConvertVisibility(
    Model.Bcf30.ComponentVisibility from) {
    var builder = new VisibilityBuilder();

    return
    builder
      .SetDefaultVisibility(from.DefaultVisibility)
      .AddExceptions(from.Exceptions.Select(ConvertComponent).ToList())
      .Build();
  }


  private static Model.Bcf21.TopicRelatedTopic ConvertRelatedTopic(
    Model.Bcf30.TopicRelatedTopicsRelatedTopic from) {
    var relatedTopic = new TopicRelatedTopic {
      Guid = from.Guid
    };
    return relatedTopic;
  }
}