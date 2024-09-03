using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BcfToolkit;
using BcfToolkit.Builder.Bcf30;
using BcfToolkit.Converter;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf30;
using NUnit.Framework;
using BcfBuilder = BcfToolkit.Builder.Bcf30.BcfBuilder;
using File = BcfToolkit.Model.Bcf30.File;

namespace tests.Builder.Bcf30;


public class SuperBcfFile {
  private BcfBuilder _builder = null!;
  private IConverter _converter = null!;
  private Worker _worker;

  [SetUp]
  public void Setup() {
    _builder = new BcfBuilder();
    _converter = new BcfToolkit.Converter.Bcf30.Converter();
    _worker = new Worker();
  }

  [Test]
  public async Task CreateSuperBcf30File() {

    var labels = new List<string> { "label1", "label2", };
    var referenceLinks = new List<string> { "link1", "link2", "link3" };

    // Header
    var fileBuilder = new FileBuilder();
    var headerFile =
      fileBuilder
        .SetReference("reference")
        .SetIsExternal(true)
        .SetFileName("FileName")
        .SetDate(DateTime.Today)
        .SetIfcProject("1234567890123456789012")
        .SetIfcSpatialStructureElement("1234567890123456789013")
        .Build();
    var headers = new List<File> { headerFile };

    // DocumentReference
    var topicDocumentReferenceBuilder = new DocumentReferenceBuilder();
    var topicDocumentReference =
      topicDocumentReferenceBuilder
        .SetDescription("description")
        .SetGuid("000b4df2-0187-49a9-8a4a-23992696bafd")
        .SetUrl("documentUrl")
        .SetDocumentGuid("111b4df2-0187-49a9-8a4a-23992696bafd")
        .Build();
    var topicDocumentReferences = new List<DocumentReference> { topicDocumentReference };

    // Comments
    var commentBuilder1 = new CommentBuilder();
    var commentBuilder2 = new CommentBuilder();
    var comment1 =
      commentBuilder1
        .SetGuid("999b4df2-0187-49a9-8a4a-23992696bafd")
        .SetModifiedAuthor("modAuthor1")
        .SetDate(DateTime.Today)
        .SetModifiedDate(DateTime.Today)
        .SetAuthor("author1")
        .SetCommentProperty("commProp1")
        .Build();
    var comment2 =
      commentBuilder2
        .SetGuid("998b4df2-0187-49a9-8a4a-23992696bafd")
        .SetModifiedAuthor("modAuthor2")
        .SetDate(DateTime.Today)
        .SetModifiedDate(DateTime.Today)
        .SetAuthor("author2")
        .SetViewPointGuid("111b4df2-0187-49a9-8a4a-23992696bafd")
        .SetCommentProperty("commProp2")
        .Build();
    var comments = new List<Comment> { comment1, comment2 };

    // Viewpoint
    var visualizationInfoBuilder = new VisualizationInfoBuilder();
    var componentBuilder = new ComponentBuilder();
    var component =
      componentBuilder
        .SetIfcGuid("1234567890123456789015")
        .SetOriginatingSystem("originatingSystem")
        .SetAuthoringToolId("authoringToolId")
        .Build();
    var components = new List<Component> { component, component };

    var visibilityBuilder = new VisibilityBuilder();
    var compVis =
      visibilityBuilder
        .SetDefaultVisibility(true)
        .AddExceptions(components)
        .Build();

    var orthoBuilder = new OrthogonalCameraBuilder();
    var ortho =
      orthoBuilder
        .SetAspectRatio(1.2)
        .SetViewToWorldScale(1.2)
        .SetCameraViewPoint(21.2, 2.4, 3.6)
        .SetCameraDirection(31.2, 2.4, 3.6)
        .SetCameraUpVector(41.2, 2.4, 3.6)
        .Build();

    var persBuilder = new PerspectiveCameraBuilder();
    var pers =
      persBuilder
        .SetCameraDirection(51.2, 2.4, 3.6)
        .SetCameraUpVector(16.2, 2.4, 3.6)
        .SetCameraViewPoint(71.2, 2.4, 3.6)
        .SetFieldOfView(2.0)
        .Build();

    var vsHintBuilder = new ViewSetupHintsBuilder();
    var vsHint =
      vsHintBuilder
        .SetOpeningVisible(true)
        .SetSpaceVisible(true)
        .SetSpaceBoundariesVisible(true)
        .Build();

    var bitmapBuilder = new BitmapBuilder();
    var bitmap =
      bitmapBuilder
        .SetReference("reference")
        .SetUp(19.2, 2.4, 3.6)
        .SetFormat("Png")
        .SetHeight(3.2)
        .SetLocation(91.2, 2.4, 3.6)
        .SetNormal(17.2, 2.4, 3.6)
        .Build();
    var bitmaps = new List<Bitmap> { bitmap, bitmap };

    var coloringBuilder = new ComponentColoringColorBuilder();
    var coloring =
      coloringBuilder
        .SetColor("40E0D0")
        .AddComponents(components)
        .Build();
    var colorings = new List<ComponentColoringColor> { coloring, coloring };

    var clippingPlaneBuilder = new ClippingPlaneBuilder();
    var clippingPlane =
      clippingPlaneBuilder
        .SetLocation(1.22, 2.4, 3.6)
        .SetDirection(1.32, 2.4, 3.6)
        .Build();
    var clippingPlanes = new List<ClippingPlane> { clippingPlane, clippingPlane };

    var lineBuilder1 = new LineBuilder();
    var lineBuilder2 = new LineBuilder();

    var line1 =
      lineBuilder1
        .SetEndPoint(1.42, 2.4, 3.6)
        .SetStartPoint(1.42, 4.4, 3.6)
        .Build();

    var line2 =
      lineBuilder2
        .SetEndPoint(3.2, 2.4, 3.6)
        .SetStartPoint(3.2, 4.4, 3.6)
        .Build();

    var lines = new List<Line> { line1, line2 };

    var visualizationInfo =
      visualizationInfoBuilder
        .SetGuid("334b4df2-0187-49a9-8a4a-23992696bafd")
        .SetOrthogonalCamera(ortho)
        .SetPerspectiveCamera(pers)
        .AddBitmaps(bitmaps)
        .AddColorings(colorings)
        .AddSelections(components)
        .AddClippingPlanes(clippingPlanes)
        .SetVisibility(compVis)
        .AddSelections(components)
        .AddLines(lines)
        .Build();

    var viewPointBuilder = new ViewPointBuilder();
    var viewPoint =
      viewPointBuilder
        .SetGuid("445b4df2-0187-49a9-8a4a-23992696bafd")
        .SetIndex(5)
        .SetSnapshot("snapshot")
        .SetSnapshotData(new FileData {
          Data = "aGVsbG8="
        })
        .SetViewPoint("viewpoint.bcfv")
        .SetVisualizationInfo(visualizationInfo)
        .Build();

    var viewPoints = new List<ViewPoint> { viewPoint };

    var bimSnippetBuilder = new BimSnippetBuilder();
    var bimSnippet =
      bimSnippetBuilder
        .SetReference("reference")
        .SetIsExternal(true)
        .SetSnippetType("snippetType")
        .SetReferenceSchema("refSchema")
        .Build();

    var projectInfoBuilder = new ProjectInfoBuilder();
    var project =
      projectInfoBuilder
        .SetProjectId("446b4df2-0187-49a9-8a4a-23992696bafd")
        .SetProjectName("Test project")
        .Build();

    var documentBuilder = new DocumentBuilder();
    var documentData = new FileData {
      Data = "aGVsbG8="
    };
    var doc =
      documentBuilder
        .SetDescription("desc")
        .SetGuid("447b4df2-0187-49a9-8a4a-23992696bafd")
        .SetFileName("New document")
        .SetDocumentData(documentData)
        .Build();
    var docList = new List<Document>();
    var docBuilder = new DocumentInfoBuilder();

    var docInfo =
      docBuilder
        .AddDocuments(docList)
        .Build();

    var priorities = new List<string> { "priority1", "priority2" };
    var users = new List<string> { "user1", "user2" };
    var stages = new List<string> { "stage1", "stage2" };
    var snippetTypes = new List<string> { "snippetType1", "snippetType2" };
    var topicLabels = new List<string> { "label1", "label2" };
    var topicStatuses = new List<string> { "open", "closed" };
    var topicTypes = new List<string> { "type1", "type2" };

    var extensionsBuilder = new ExtensionsBuilder();
    var extensions =
      extensionsBuilder
        .AddPriorities(priorities)
        .AddUsers(users)
        .AddStages(stages)
        .AddSnippetTypes(snippetTypes)
        .AddTopicLabels(topicLabels)
        .AddTopicStatuses(topicStatuses)
        .AddTopicTypes(topicTypes)
        .Build();

    var bcf = _builder
      .SetProject(project)
      .SetDocument(docInfo)
      .SetExtensions(extensions)
      .AddMarkup(m => m
        .AddDocumentReferences(topicDocumentReferences)
        .AddHeaderFiles(headers)
        .AddReferenceLinks(referenceLinks)
        .AddLabels(labels)
        .AddComments(comments)
        .AddViewPoints(viewPoints)
        .SetTitle("title")
        .SetPriority("priority")
        .SetGuid("3ffb4df2-0187-49a9-8a4a-23992696bafd")
        .SetCreationAuthor("creationAuthor")
        .SetModifiedAuthor("modAuthor")
        .SetAssignedTo("assignedTo")
        .SetStage("stage")
        .SetTopicType("topicType")
        .SetTopicStatus("readyyy")
        .SetDescription("description")
        .SetIndex(5)
        .SetCreationDate(DateTime.Today)
        .SetDueDate(DateTime.Today)
        .SetModifiedDate(DateTime.Today)
        .SetBimSnippet(bimSnippet))
      .Build();

    await _converter.ToBcf(bcf,
      "Resources/Bcf/v3.0/super30.bcfzip");
  }

  public async Task ReadBcf30FileTest() {
    var samples = new List<string> {
      "Resources/Bcf/v3.0/super30.bcfzip"
    };

    var tasks = samples.Select(async path => {
      await using var stream =
        new FileStream(path, FileMode.Open, FileAccess.Read);
      var bcf = await _worker.BcfFromStream(stream);
      Assert.That(bcf.Version.VersionId, Is.EqualTo("3.0"));
    }).ToArray();

    await Task.WhenAll(tasks);
  }
}