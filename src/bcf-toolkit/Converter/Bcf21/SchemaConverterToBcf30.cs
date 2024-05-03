using System.Linq;
using BcfToolkit.Builder.Bcf30;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Converter.Bcf21;

public static class SchemaConverterToBcf30 {
  
  public static Model.Bcf30.Bcf Convert(Model.Bcf21.Bcf from) {
    var builder = new BcfBuilder();
    return builder
      .AddMarkups(from.Markups.Select(ConvertMarkup).ToList(), true)
      .SetProject(p => p.WithDefaults()) // TODO
      .SetDocumentInfo(d => d.AddDocument(doc => doc.WithDefaults())) // TODO
      .Build();
  }

  private static Model.Bcf30.Markup ConvertMarkup(Model.Bcf21.Markup from) {
    var builder = new MarkupBuilder();
    var topic = from.Topic;
    return builder
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
      .SetBimSnippet(b => b
        .SetReference(topic.BimSnippet.Reference)
        .SetReferenceSchema(topic.BimSnippet.ReferenceSchema)
        .SetSnippetType(topic.BimSnippet.SnippetType)
        .SetIsExternal(topic.BimSnippet.IsExternal))
      //.AddDocumentReferences(from.Topic.DocumentReference.Select(ConvertDocumentReference).ToList()) TODO
      .AddRelatedTopics(topic.RelatedTopic.Select(t => t.Guid).ToList())
      .AddComments(from.Comment.Select(ConvertComment).ToList())
      .AddViewPoints(from.Viewpoints.Select(ConvertViewPoint).ToList())
      .Build();
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
    //TODO DocumentGuid, Url
    var builder = new DocumentReferenceBuilder();
    return builder
      .SetDescription(from.Description)
      .SetGuid(from.Guid)
      .SetUrl(from.IsExternal ? from.ReferencedDocument : null)
      .Build();
  }

  private static Model.Bcf30.Comment ConvertComment(Model.Bcf21.Comment from) {
    var builder = new CommentBuilder();
    return builder
      .SetDate(from.Date)
      .SetAuthor(from.Author)
      .SetCommentProperty(from.CommentProperty)
      .SetViewPoint(from.Viewpoint.Guid)
      .SetModifiedDate(from.ModifiedDate)
      .SetModifiedAuthor(from.ModifiedAuthor)
      .Build();
  }

  private static Model.Bcf30.ViewPoint ConvertViewPoint(
    Model.Bcf21.ViewPoint from) {
    var builder = new VisualizationInfoBuilder();
    var visInfoSource = from.VisualizationInfo!;
    var viewSetupHints = visInfoSource.Components.ViewSetupHints;
    var visibility = visInfoSource.Components.Visibility;
    var coloring = visInfoSource.Components.Coloring;
    var orthoCamera = visInfoSource.OrthogonalCamera;
    var perspCamera = visInfoSource.PerspectiveCamera;
    var visInfo = builder
      .AddSelections(visInfoSource.Components.Selection.Select(ConvertComponent).ToList())
      .SetVisibility(vis => vis
        .SetViewSetupHints(hints => hints
          .SetSpaceVisible(viewSetupHints.SpacesVisible)
          .SetSpaceBoundariesVisible(viewSetupHints.SpaceBoundariesVisible)
          .SetOpeningVisible(viewSetupHints.OpeningsVisible))
        .AddExceptions(visibility.Exceptions.Select(ConvertComponent).ToList())
        .SetDefaultVisibility(visibility.DefaultVisibility))
      .AddColorings(coloring.Select(ConvertColor).ToList())
      .SetOrthogonalCamera(oC => oC
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
        .SetAspectRatio(1.0)) //by default
      .SetPerspectiveCamera(pC => pC
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
        .SetAspectRatio(1.0)) //by default
      .AddLines(visInfoSource.Lines.Select(ConvertLine).ToList())
      .AddClippingPlanes(visInfoSource.ClippingPlanes.Select(ConvertClippingPlane).ToList())
      .AddBitmaps(visInfoSource.Bitmap.Select(ConvertBitmap).ToList())
      .SetGuid(visInfoSource.Guid)
      .Build();
    return new ViewPoint {
      Viewpoint = from.Viewpoint,
      Snapshot = from.Snapshot,
      SnapshotData = from.SnapshotData,
      Index = from.Index,
      Guid = from.Guid,
      VisualizationInfo = visInfo
    };
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
}