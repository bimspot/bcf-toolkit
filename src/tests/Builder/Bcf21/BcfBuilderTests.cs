using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BcfToolkit.Builder.Bcf21;
using BcfToolkit.Converter;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf21;
using NUnit.Framework;

namespace tests.Builder.Bcf21;

public class BcfBuilderTests {
  private BcfBuilder _builder = null!;


  private IConverter _converter = null!;

  [SetUp]
  public void Setup() {
    _builder = new BcfBuilder();
    _converter = new BcfToolkit.Converter.Bcf21.Converter();
  }

  [Test]
  public void EmptyFieldsConversationTest() {

    var labels = new List<string> { "label1", string.Empty, "label2", };
    var referenceLinks = new List<string> { "link1", string.Empty, string.Empty };

    // Header
    var headerBuilder = new HeaderFileBuilder();
    var header =
      headerBuilder
      .SetDate(DateTime.Now)
      .SetReference(string.Empty)
      .SetFileName("Filename")
      .SetIfcProject("1234567890123456789012")
      .SetIfcSpatialStructureElement("1234567890123456789012")
      .SetIsExternal(true)
      .Build();
    var headers = new List<HeaderFile> { header };

    // DocumentReference
    var topicDocumentReferenceBuilder = new DocumentReferenceBuilder();
    var topicDocumentReference =
      topicDocumentReferenceBuilder
        .SetDescription(string.Empty)
        .SetIsExternal(false)
        .SetGuid("000b4df2-0187-49a9-8a4a-23992696bafd")
        .SetReferencedDocument("ref_document")
        .Build();
    var topicDocumentReferences = new List<TopicDocumentReference> { topicDocumentReference };

    // Comments
    var commentBuilder1 = new CommentBuilder();
    var commentBuilder2 = new CommentBuilder();
    var commentPropMustHaveValue =
      commentBuilder1
        .SetGuid("999b4df2-0187-49a9-8a4a-23992696bafd")
        .SetModifiedAuthor(string.Empty)
        .SetDate(DateTime.Today)
        .SetModifiedDate(DateTime.Today)
        .SetAuthor("author1")
        .SetCommentProperty("commProp1")
        .Build();
    var commentPropCanBeEmpty =
      commentBuilder2
        .SetGuid("999b4df2-0187-49a9-8a4a-23992696bafd")
        .SetModifiedAuthor(string.Empty)
        .SetDate(DateTime.Today)
        .SetModifiedDate(DateTime.Today)
        .SetAuthor("author2")
        .SetViewPointGuid("111b4df2-0187-49a9-8a4a-23992696bafd")
        .SetCommentProperty(string.Empty)
        .Build();
    var comments = new List<Comment> { commentPropMustHaveValue, commentPropCanBeEmpty };

    // Viewpoint
    var visualizationInfoBuilder = new VisualizationInfoBuilder();

    var componentBuilder = new ComponentBuilder();
    var component =
      componentBuilder
        .SetIfcGuid("1234567890123456789012")
        .SetOriginatingSystem(string.Empty)
        .SetAuthoringToolId(string.Empty)
        .Build();
    var components = new List<Component> { component };

    var componentVisibility = new ComponentVisibility();
    componentVisibility.DefaultVisibility = false;
    componentVisibility.DefaultVisibilityValueSpecified = false;

    var visualizationInfo =
      visualizationInfoBuilder
        .SetGuid("333b4df2-0187-49a9-8a4a-23992696bafd")
        .SetVisibility(componentVisibility)
        .AddSelections(components)
        .Build();

    var viewPointBuilder = new ViewPointBuilder();
    var viewPoint =
      viewPointBuilder
        .SetGuid("444b4df2-0187-49a9-8a4a-23992696bafd")
        .SetIndex(5)
        .SetSnapshot(string.Empty)
        .SetSnapshotData(new FileData {
          Data = "snapshotdata1"
        })
        .SetViewPoint(string.Empty)
        .SetVisualizationInfo(visualizationInfo)
        .Build();

    var viewPoints = new List<ViewPoint> { viewPoint };

    var bcf = _builder
      .AddMarkup(m => m
        .AddHeaderFiles(headers)
        .SetTitle("required field")
        .SetPriority(string.Empty)
        .SetGuid("3ffb4df2-0187-49a9-8a4a-23992696bafd")
        .SetCreationAuthor("required field")
        .SetModifiedAuthor(string.Empty)
        .SetAssignedTo(string.Empty)
        .SetStage(string.Empty)
        .SetTopicType(string.Empty)
        .SetTopicStatus(string.Empty)
        .SetDescription(string.Empty)
        .AddLabels(labels)
        .AddReferenceLinks(referenceLinks)
        .AddDocumentReferences(topicDocumentReferences)
        .AddComments(comments)
        .AddViewPoints(viewPoints))
      .SetProject(p => p
        .SetProjectId("3ZSh2muKX7S8MCESk95seC")
        .SetProjectName(string.Empty)
        .SetExtensionSchema(string.Empty))
      .Build();

    var res = _converter.ToBcf(bcf,
      BcfVersionEnum.Bcf30);

    if (res.Exception != null && res.Exception.Message.Length > 0)
      Assert.Fail("Error message found: " + res.Exception.Message);

  }
  
  [Test]
  public async Task BuildMaximumInformationBcfFromStreamTest() {
    await using var stream = new FileStream(
      "Resources/Bcf/v2.1/MaximumInformation.bcfzip",
      FileMode.Open,
      FileAccess.Read);
    var bcf = await _builder.BuildFromStream(stream);
    Assert.That(bcf.Markups.Count, Is.EqualTo(2));
    Assert.That(bcf.DocumentInfo?.Documents.Count, Is.EqualTo(1));
  }
}