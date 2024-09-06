using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BcfToolkit;
using BcfToolkit.Builder.Bcf21;
using BcfToolkit.Converter;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf21;
using NUnit.Framework;

namespace tests.Builder.Bcf21;


public class SuperBcfFile {
  private BcfBuilder _builder = null!;
  private IConverter _converter = null!;
  private Worker _worker;

  [SetUp]
  public void Setup() {
    _builder = new BcfBuilder();
    _converter = new BcfToolkit.Converter.Bcf21.Converter();
    _worker = new Worker();
  }

  [Test]
  public async Task CreateSuperBcf21File() {

    var labels = new List<string> { "label1", "label2", };
    var referenceLinks = new List<string> { "http://www.buildingsmart-tech.org", "www.google.com" };


    // Header
    var headerBuilder = new HeaderFileBuilder();
    var header =
      headerBuilder
        .SetDate(DateTime.Now)
        .SetReference("reference")
        .SetFileName("Bauprojekt1.ifc")
        .SetIfcProject("4g8GxLEzP459ZWW6_RGsez")
        .SetIfcSpatialStructureElement("128GxLEzP459ZWW6_RGsez")
        .SetIsExternal(true)
        .Build();
    var headers = new List<HeaderFile> { header };

    // DocumentReference
    var topicDocumentReferenceBuilder = new DocumentReferenceBuilder();
    var topicDocumentReference =
      topicDocumentReferenceBuilder
        .SetDescription("This is a document ref")
        .SetIsExternal(false)
        .SetGuid("000b4df2-0187-49a9-8a4a-23992696bafd")
        .SetReferencedDocument("ref_document.png")
        .Build();

    var topicDocumentReferenceExternal =
      topicDocumentReferenceBuilder
        .SetDescription("BCFv1 Markup Schema")
        .SetIsExternal(true)
        .SetReferencedDocument("http://www.buildingsmart-tech.org/specifications/bcf-releases/bcfxml-v1/markup.xsd/at_download/file<")
        .Build();

    topicDocumentReference.DocumentData = new FileData {
      Mime = "image/png",
      Data = "iVBORw0KGgoAAAANSUhEUgAAAAgAAAAIAQMAAAD+wSzIAAAABlBMVEX///+/v7+jQ3Y5AAAADklEQVQI12P4AIX8EAgALgAD/aNpbtEAAAAASUVORK5CYII="
    };
    var topicDocumentReferences = new List<TopicDocumentReference>
      {topicDocumentReference, topicDocumentReferenceExternal};

    // Comments
    var commentBuilder1 = new CommentBuilder();
    var commentBuilder2 = new CommentBuilder();
    var comment1 =
      commentBuilder1
        .SetGuid("998b4df2-0187-49a9-8a4a-23992696bafd")
        .SetModifiedAuthor("john.wick@johnwick.com")
        .SetDate(DateTime.Today)
        .SetModifiedDate(DateTime.Today)
        .SetAuthor("john.wick@johnwick.com")
        .SetCommentProperty("This wall should be moved 1cm left.")
        .Build();
    var comment2 =
      commentBuilder2
        .SetGuid("997b4df2-0187-49a9-8a4a-23992696bafd")
        .SetModifiedAuthor("jim.carry@jim.com")
        .SetDate(DateTime.Today)
        .SetModifiedDate(DateTime.Today)
        .SetAuthor("jim.carry@jim.com")
        .SetViewPointGuid("444b4df2-0187-49a9-8a4a-23992696bafd")
        .SetCommentProperty("This wall should be moved 2cm left.")
        .Build();
    var comments = new List<Comment>
      {comment1, comment2};

    // Viewpoint
    var visualizationInfoBuilder = new VisualizationInfoBuilder();

    var componentBuilder = new ComponentBuilder();
    var component =
      componentBuilder
        .SetIfcGuid("0g8GxLEzP459ZWW6_RGsez")
        .SetOriginatingSystem("originatingSystem")
        .SetAuthoringToolId("authoringToolId")
        .Build();
    var components = new List<Component> { component };

    var visibilityBuilder = new VisibilityBuilder();
    var compVis =
      visibilityBuilder
        .SetDefaultVisibility(true)
        .AddExceptions(components)
        .Build();

    var orthoBuilder = new OrthogonalCameraBuilder();
    var ortho =
      orthoBuilder
        .SetCameraDirection(1.2, 2.4, 3.6)
        .SetCameraUpVector(11.2, 21.4, 31.6)
        .SetCameraViewPoint(11.2, 12.4, 13.6)
        .SetViewToWorldScale(1.0)
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
        .SetUp(41.2, 24.4, 43.6)
        .SetFormat("Png")
        .SetHeight(3.2)
        .SetLocation(13.2, 23.4, 33.6)
        .SetNormal(31.2, 32.4, 33.6)
        .Build();
    var visBitmaps = new List<VisualizationInfoBitmap> { bitmap, bitmap };

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
        .SetLocation(15.2, 2.4, 3.6)
        .SetDirection(1.2, 52.4, 3.6)
        .Build();
    var clippingPlanes = new List<ClippingPlane> { clippingPlane, clippingPlane };

    var visualizationInfo =
      visualizationInfoBuilder
        .SetGuid("333b4df2-0187-49a9-8a4a-23992696bafd")
        .SetOrthogonalCamera(ortho)
        .SetViewSetupHints(vsHint)
        .AddBitmaps(visBitmaps)
        .AddColorings(colorings)
        .AddSelections(components)
        .AddClippingPlanes(clippingPlanes)
        .SetVisibility(compVis)
        .AddSelections(components)
        .Build();

    var viewPointBuilder = new ViewPointBuilder();
    var viewPoint =
      viewPointBuilder
        .SetGuid("444b4df2-0187-49a9-8a4a-23992696bafd")
        .SetIndex(0)
        .SetSnapshot("snapshot.png")
        .SetSnapshotData(new FileData {
          Mime = "data:image/png;base64",
          Data = "iVBORw0KGgoAAAANSUhEUgAAAAgAAAAIAQMAAAD+wSzIAAAABlBMVEX///+/v7+jQ3Y5AAAADklEQVQI12P4AIX8EAgALgAD/aNpbtEAAAAASUVORK5CYII="
        })
        .SetViewPoint("viewpoint.bcfv")
        .SetVisualizationInfo(visualizationInfo)
        .Build();

    var viewPoints = new List<ViewPoint> { viewPoint };

    var bimSnippetBuilder = new BimSnippetBuilder();
    var bimSnippet =
      bimSnippetBuilder
        .SetReference("https://.../snippetExample.ifc")
        .SetIsExternal(true)
        .SetSnippetType("snippetType")
        .SetReferenceSchema("refSchema")
        .Build();

    var topicRelatedTopic1 = new TopicRelatedTopic();
    topicRelatedTopic1.Guid = "4ffb4df2-0187-49a9-8a4a-23992696bafd";

    var topicRelatedTopic2 = new TopicRelatedTopic();
    topicRelatedTopic2.Guid = "5ffb4df2-0187-49a9-8a4a-23992696bafd";

    var relatedTopics = new List<TopicRelatedTopic>
      {topicRelatedTopic1, topicRelatedTopic2};

    var bcf = _builder
      .AddMarkup(m => m
        .AddHeaderFiles(headers)
        .SetTitle("Wall reposition issue")
        .SetPriority("Low")
        .SetGuid("3ffb4df2-0187-49a9-8a4a-23992696bafd")
        .SetCreationAuthor("john.wick@johnwick.com")
        .SetModifiedAuthor("john.wick@johnwick.com")
        .SetAssignedTo("jim.carry@jim.com")
        .SetStage("PreDesign")
        .SetTopicType("Warning")
        .SetTopicStatus("Closed")
        .SetDescription("Give me more details")
        .AddLabels(labels)
        .AddReferenceLinks(referenceLinks)
        .AddDocumentReferences(topicDocumentReferences)
        .AddComments(comments)
        .AddViewPoints(viewPoints)
        .SetIndex(0)
        .SetCreationDate(DateTime.Today)
        .SetDueDate(DateTime.Today)
        .SetModifiedDate(DateTime.Today)
        .SetBimSnippet(bimSnippet)
        .AddRelatedTopics(relatedTopics))
      .SetProject(p => p
        .SetProjectId("3ZSh2muKX7S8MCESk95seC")
        .SetProjectName("projectName")
        .SetExtensionSchema("extensionSchema"))
      //   .SetDocumentData(docData)
      .Build();

    await _converter.ToBcf(bcf,
      "Resources/Bcf/v2.1/super21.bcfzip");
  }


  public async Task ReadBcf21FileTest() {
    var samples = new List<string> {
      "Resources/Bcf/v2.1/super21.bcfzip"
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