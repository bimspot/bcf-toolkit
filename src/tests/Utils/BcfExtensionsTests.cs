using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using bcf21 = BcfToolkit.Model.Bcf21;
using bcf30 = BcfToolkit.Model.Bcf30;

namespace tests.Utils;

[TestFixture]
public class BcfExtensionsTests {
  /// <summary>
  ///   Topic with guid ee9a9498-698b-44ed-8ece-b3ae3b480a90 should have all
  ///   parts of decomposed wall (2_hQ1Rixj6lgHTra$L72O4) visible.
  /// </summary>
  [Test]
  [Category("BCF v2.1")]
  public async Task ParseBcfAllPartsVisibleTest() {
    await using var stream = new FileStream("Resources/Bcf/v2.1/AllPartsVisible.bcfzip", FileMode.Open, FileAccess.Read);

    var markups =
      await BcfToolkit.Utils.BcfExtensions.ParseMarkups<bcf21.Markup, bcf21.VisualizationInfo>(
        stream);
    var markup = markups.FirstOrDefault()!;
    Assert.That(1, Is.EqualTo(markups.Count));
    Assert.That("ee9a9498-698b-44ed-8ece-b3ae3b480a90", Is.EqualTo(markup.Topic.Guid));
    Assert.That("All components of curtain wall visible",
      Is.EqualTo(markup.Topic.Title));
    Assert.That("pasi.paasiala@solibri.com", Is.EqualTo(markup.Topic.ModifiedAuthor));
    var visInfo =
      (bcf21.VisualizationInfo)markup.Viewpoints.FirstOrDefault()
        ?.VisualizationInfo!;
    Assert.That("2_hQ1Rixj6lgHTra$L72O4",
      Is.EqualTo(visInfo.Components.Visibility.Exceptions.FirstOrDefault()?.IfcGuid));
  }

  /// <summary>
  ///   Exactly three components should be selected.
  /// </summary>
  [Test]
  [Category("BCF v2.1")]
  public async Task ParseBcfComponentsSelection21Test() {
    await using var stream = new FileStream("Resources/Bcf/v2.1/ComponentSelection.bcfzip", FileMode.Open, FileAccess.Read);

    var markups =
      await BcfToolkit.Utils.BcfExtensions.ParseMarkups<bcf21.Markup, bcf21.VisualizationInfo>(
        stream);
    var markup = markups.FirstOrDefault();
    Assert.That(1, Is.EqualTo(markups.Count));
    var visInfo =
      (bcf21.VisualizationInfo)markup?.Viewpoints.FirstOrDefault()
        ?.VisualizationInfo!;
    Assert.That(3, Is.EqualTo(visInfo.Components.Selection.Count));
    Assert.That("1GU8BMEqHBQxVAbwRD$4Jj",
      Is.EqualTo(visInfo.Components.Selection.FirstOrDefault()?.IfcGuid));
  }

  /// <summary>
  ///   Your application is aware of the relative reference to
  ///   http://bimfiles.example.com/JsonElement.json as given in the _Topic_s
  ///   BIMSnippet property in the markup.bcf file. The reference is marked as
  ///   external to indicate that the actual snippet data is not contained within
  ///   the BCFZip.
  /// </summary>
  [Test]
  [Category("BCF v2.1")]
  public async Task ParseBcfExternalBimSnippetTest() {
    await using var stream = new FileStream("Resources/Bcf/v2.1/ExternalBIMSnippet.bcfzip", FileMode.Open, FileAccess.Read);

    var markups =
      await BcfToolkit.Utils.BcfExtensions.ParseMarkups<bcf21.Markup, bcf21.VisualizationInfo>(
        stream);
    var markup = markups.FirstOrDefault()!;
    Assert.That(1, Is.EqualTo(markups.Count));
    Assert.That("http://bimfiles.example.com/JsonElement.json",
      Is.EqualTo(markup.Topic.BimSnippet.Reference));
  }

  /// <summary>
  ///   There should be two markups within the BCFZip.
  /// </summary>
  [Test]
  [Category("BCF v2.1")]
  public async Task ParseBcfMultipleMarkupsAndViewpointsTest() {
    await using var stream = new FileStream("Resources/Bcf/v2.1/MaximumInformation.bcfzip", FileMode.Open, FileAccess.Read);

    var markups =
      await BcfToolkit.Utils.BcfExtensions.ParseMarkups<bcf21.Markup, bcf21.VisualizationInfo>(
        stream);
    Assert.That(2, Is.EqualTo(markups.Count));
    var markup = markups
      .FirstOrDefault(m =>
        m.Topic.Guid == "7ddc3ef0-0ab7-43f1-918a-45e38b42369c");
    Assert.That(3, Is.EqualTo(markup?.Viewpoints.Count));
  }

  /// <summary>
  ///   No markups exception should be thrown.
  /// </summary>
  [Test]
  [Category("BCF v2.1")]
  public void ParseBcfNoMarkupsTest() {
    const string filePath = "Resources/Bcf/v2.1/NoMakrups.bcfzip";

    Assert.That(async () => {
      await using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
      await BcfToolkit.Utils.BcfExtensions.ParseMarkups<bcf21.Markup, bcf21.VisualizationInfo>(fileStream);
    }, Throws.Exception);
  }

  /// <summary>
  ///   The topic should have a related topic, and that is available.
  /// </summary>
  [Test]
  [Category("BCF v2.1")]
  public async Task ParseBcfRelatedTopics21Test() {
    const string filePath = "Resources/Bcf/v2.1/RelatedTopics.bcfzip";
    await using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
    var markups = await BcfToolkit.Utils.BcfExtensions.ParseMarkups<bcf21.Markup, bcf21.VisualizationInfo>(fileStream);
    var relatedTopicId = markups.FirstOrDefault()?.Topic.RelatedTopic
      .FirstOrDefault()?.Guid;
    var secondTopicId = markups.ElementAt(1).Topic.Guid;
    Assert.That(relatedTopicId, Is.EqualTo(secondTopicId));
  }

  /// <summary>
  ///   Nothing should be selected and only a wall is visible.
  /// </summary>
  [Test]
  [Category("BCF v2.1")]
  public async Task ParseBcfSingleVisibleWallTest() {
    const string filePath = "Resources/Bcf/v2.1/SingleVisibleWall.bcfzip";
    await using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
    var markups =
      await BcfToolkit.Utils.BcfExtensions
        .ParseMarkups<bcf21.Markup, bcf21.VisualizationInfo>(fileStream);
    var visibility =
      markups.FirstOrDefault()?
      .Viewpoints.FirstOrDefault()?
      .VisualizationInfo?
      .Components
      .Visibility;

    Assert.That(visibility?.DefaultVisibility, Is.EqualTo(false));
    Assert.That(visibility?.Exceptions.FirstOrDefault()?.IfcGuid,
      Is.EqualTo("1E8YkwPMfB$h99jtn_uAjI"));
  }


  /// <summary>
  ///   The topic should have an assigned user.
  /// </summary>
  [Test]
  [Category("BCF v2.1")]
  public async Task ParseBcfUserAssignment21Test() {
    await using var stream = new FileStream("Resources/Bcf/v2.1/UserAssignment.bcfzip", FileMode.Open, FileAccess.Read);

    var markups =
      await BcfToolkit.Utils.BcfExtensions.ParseMarkups<bcf21.Markup, bcf21.VisualizationInfo>(
        stream);
    var markup = markups.FirstOrDefault()!;
    Assert.That("jon.anders.sollien@catenda.no", Is.EqualTo(markup.Topic.AssignedTo));
  }

  /// <summary>
  ///   Exactly three components should be selected.
  /// </summary>
  [Test]
  [Category("BCF v3.0")]
  public async Task ParseBcfComponentsSelection30Test() {
    await using var stream = new FileStream("Resources/Bcf/v3.0/ComponentSelection.bcfzip", FileMode.Open, FileAccess.Read);

    var markups =
      await BcfToolkit.Utils.BcfExtensions.ParseMarkups<bcf30.Markup, bcf30.VisualizationInfo>(
        stream);
    var markup = markups.FirstOrDefault();
    Assert.That(1, Is.EqualTo(markups.Count));
    var visInfo =
      (bcf30.VisualizationInfo)markup?.Topic.Viewpoints.FirstOrDefault()
        ?.VisualizationInfo!;
    var header = markup?.Header!;

    Assert.That(2, Is.EqualTo(header.Files.Count));
    Assert.That("Architectural.ifc",
      Is.EqualTo(header.Files.FirstOrDefault()?.Filename));
    Assert.That("0KkZ20so9BsO1d1hFcfLOl",
      Is.EqualTo(visInfo.Components.Selection.FirstOrDefault()?.IfcGuid));
  }

  /// <summary>
  ///   It should have the absolute reference to the document outside of the
  ///   BCFZip as given in the DocumentReferences property in the markup.bcf file.
  /// </summary>
  [Test]
  [Category("BCF v3.0")]
  public async Task ParseBcfDocumentRefExternalTest() {
    await using var stream = new FileStream("Resources/Bcf/v3.0/DocumentReferenceExternal.bcfzip", FileMode.Open, FileAccess.Read);

    var markups =
      await BcfToolkit.Utils.BcfExtensions.ParseMarkups<bcf30.Markup, bcf30.VisualizationInfo>(
          stream);
    var markup = markups.FirstOrDefault()!;
    Assert.That(1, Is.EqualTo(markups.Count));
    Assert.That(
      "http://www.buildingsmart-tech.org/specifications/bcf-releases/bcfxml-v1/markup.xsd/at_download/file",
      Is.EqualTo(markup.Topic.DocumentReferences.FirstOrDefault()?.Url));
  }

  /// <summary>
  ///   It should have the document to ThisIsADocument.txt as given in the
  ///   _Topic_s DocumentReferences property in the markup.bcf file.
  ///   The reference is marked as internal to indicate that the document is
  ///   contained within the BCFZip.
  /// </summary>
  [Test]
  [Category("BCF v3.0")]
  public async Task ParseBcfDocumentRefInternalTest() {
    await using var stream = new FileStream("Resources/Bcf/v3.0/DocumentReferenceInternal.bcfzip", FileMode.Open, FileAccess.Read);

    var markups =
      await BcfToolkit.Utils.BcfExtensions.ParseMarkups<bcf30.Markup, bcf30.VisualizationInfo>(
        stream);
    var documentInfo =
      await BcfToolkit.Utils.BcfExtensions.ParseDocuments<bcf30.DocumentInfo>(
        stream);
    var markup = markups.FirstOrDefault()!;
    Assert.That(1, Is.EqualTo(markups.Count));
    var documentGuid = markup.Topic.DocumentReferences.FirstOrDefault()?.DocumentGuid;
    var document = documentInfo?.Documents.FirstOrDefault()!;
    Assert.That("b1d1b7f0-60b9-457d-ad12-16e0fb997bc5", Is.EqualTo(documentGuid));
    Assert.That(documentGuid, Is.EqualTo(document.Guid));
    Assert.That("ThisIsADocument.txt", Is.EqualTo(document.Filename));
    Assert.That(documentInfo?.Documents.FirstOrDefault()?.DocumentData?.Mime, Is.EqualTo("data:text/plain;base64"));
  }

  /// <summary>
  ///   Due date should be assigned to the topic.
  /// </summary>
  [Test]
  [Category("BCF v3.0")]
  public async Task ParseBcfDueDateTest() {
    await using var stream = new FileStream("Resources/Bcf/v3.0/DueDate.bcfzip", FileMode.Open, FileAccess.Read);

    var markups =
      await BcfToolkit.Utils.BcfExtensions
        .ParseMarkups<bcf30.Markup, bcf30.VisualizationInfo>(
          stream);
    var markup = markups.FirstOrDefault();
    Assert.That(1, Is.EqualTo(markups.Count));
    Assert.That(DateTime.Parse("2021-03-15T11:00:00.000Z").ToUniversalTime(),
      Is.EqualTo(markup?.Topic.DueDate));
  }

  /// <summary>
  ///   It should display the "Architects" label in the topic.
  ///   The labels should be defined in the extensions.
  /// </summary>
  [Test]
  [Category("BCF v3.0")]
  public async Task ParseBcfLabelsTest() {
    await using var markupsStream = new FileStream("Resources/Bcf/v3.0/Labels.bcfzip", FileMode.Open, FileAccess.Read);
    await using var extensionsStream = new FileStream("Resources/Bcf/v3.0/Labels.bcfzip", FileMode.Open, FileAccess.Read);


    var markups =
      await BcfToolkit.Utils.BcfExtensions
        .ParseMarkups<bcf30.Markup, bcf30.VisualizationInfo>(
          markupsStream);
    var markup = markups.FirstOrDefault()!;
    var extensions =
      await BcfToolkit.Utils.BcfExtensions.ParseExtensions<bcf30.Extensions>(
        extensionsStream);
    Assert.That(1, Is.EqualTo(markups.Count));
    var label = markup.Topic.Labels.FirstOrDefault();
    Assert.That("Architects", Is.EqualTo(label));
    var topicLabels = extensions.TopicLabels;
    ClassicAssert.Contains(label, topicLabels);
  }

  /// <summary>
  ///   The topic should have a milestone set.
  /// </summary>
  [Test]
  [Category("BCF v3.0")]
  public async Task ParseBcfStageTest() {
    await using var markupsStream = new FileStream("Resources/Bcf/v3.0/Milestone.bcfzip", FileMode.Open, FileAccess.Read);
    await using var extensionsStream = new FileStream("Resources/Bcf/v3.0/Milestone.bcfzip", FileMode.Open, FileAccess.Read);

    var markups =
      await BcfToolkit.Utils.BcfExtensions
        .ParseMarkups<bcf30.Markup, bcf30.VisualizationInfo>(
          markupsStream);
    var markup = markups.FirstOrDefault()!;
    var extensions =
      await BcfToolkit.Utils.BcfExtensions.ParseExtensions<bcf30.Extensions>(
        extensionsStream);
    Assert.That(1, Is.EqualTo(markups.Count));
    var stage = markup.Topic.Stage;
    Assert.That("February", Is.EqualTo(stage));
    var stages = extensions.Stages;
    ClassicAssert.Contains(stage, stages);
  }

  /// <summary>
  ///   The topic should have a related topic, and that is available.
  /// </summary>
  [Test]
  [Category("BCF v3.0")]
  public async Task ParseBcfRelatedTopics30Test() {
    await using var stream = new FileStream("Resources/Bcf/v3.0/RelatedTopics.bcfzip", FileMode.Open, FileAccess.Read);

    var markups =
      await BcfToolkit.Utils.BcfExtensions
        .ParseMarkups<bcf30.Markup, bcf30.VisualizationInfo>(
          stream);
    var markup1 = markups.FirstOrDefault()!;
    var markup2 = markups.ElementAt(1);
    Assert.That(2, Is.EqualTo(markups.Count));
    Assert.That(markup1.Topic.Guid,
      Is.EqualTo(markup2.Topic.RelatedTopics.FirstOrDefault()?.Guid));
  }

  /// <summary>
  ///   Nothing should be selected and the wall is hidden, but everything else
  ///   is visible.
  /// </summary>
  [Test]
  [Category("BCF v3.0")]
  public async Task ParseBcfSingleInvisibleWallTest() {
    await using var stream = new FileStream("Resources/Bcf/v3.0/SingleInvisibleWall.bcfzip", FileMode.Open, FileAccess.Read);

    var markups =
      await BcfToolkit.Utils.BcfExtensions
        .ParseMarkups<bcf30.Markup, bcf30.VisualizationInfo>(
          stream);
    var markup = markups.FirstOrDefault()!;
    var visInfo =
      (bcf30.VisualizationInfo)markup.Topic.Viewpoints.FirstOrDefault()
        ?.VisualizationInfo!;
    Assert.That(true, Is.EqualTo(visInfo.Components.Visibility.DefaultVisibility));
    Assert.That("1E8YkwPMfB$h99jtn_uAjI",
      Is.EqualTo(visInfo.Components.Visibility.Exceptions.FirstOrDefault()?.IfcGuid));
  }

  /// <summary>
  ///   Topic named "Topics with different model visible - MEP" should only
  ///   display the MEP model when visualized.
  ///   Topic named "Topics with different model visible - Architectural"
  ///   should only display the Architectural model when visualized.
  /// </summary>
  [Test]
  [Category("BCF v3.0")]
  public async Task ParseBcfDifferentModelsVisibleTest() {
    await using var stream = new FileStream("Resources/Bcf/v3.0/TopicsWithDifferentModelsVisible.bcfzip", FileMode.Open, FileAccess.Read);

    var markups =
      await BcfToolkit.Utils.BcfExtensions
        .ParseMarkups<bcf30.Markup, bcf30.VisualizationInfo>(
          stream);
    var markupARC = markups.FirstOrDefault(m =>
      m.Topic.Title.Equals(
        "Topics with different model visible - Architectural"))!;
    var markupMEP = markups.FirstOrDefault(m =>
      m.Topic.Title.Equals("Topics with different model visible - MEP"))!;

    Assert.That(1, Is.EqualTo(markupARC.Header.Files.Count));
    Assert.That("Architectural.ifc",
      Is.EqualTo(markupARC.Header.Files.FirstOrDefault()?.Filename));
    Assert.That(1, Is.EqualTo(markupMEP.Header.Files.Count));
    Assert.That("MEP.ifc",
      Is.EqualTo(markupMEP.Header.Files.FirstOrDefault()?.Filename));
  }

  /// <summary>
  ///   The topic should have an assigned user.
  /// </summary>
  [Test]
  [Category("BCF v3.0")]
  public async Task ParseBcfUserAssignment30Test() {
    await using var stream = new FileStream("Resources/Bcf/v3.0/UserAssignment.bcfzip", FileMode.Open, FileAccess.Read);

    var markups =
      await BcfToolkit.Utils.BcfExtensions.ParseMarkups<bcf30.Markup, bcf30.VisualizationInfo>(
        stream);
    var markup = markups.FirstOrDefault()!;
    Assert.That("Architect@example.com", Is.EqualTo(markup.Topic.AssignedTo));
  }

  /// <summary>
  ///   Testing the required parsing method.
  /// </summary>
  [Test]
  public async Task ParseRequiredObjectTest() {
    await using var stream = new FileStream("Resources/Bcf/v3.0/Milestone.bcfzip", FileMode.Open, FileAccess.Read);

    var extensions =
      await BcfToolkit.Utils.BcfExtensions.ParseExtensions<bcf30.Extensions>(
        stream);
    var type = extensions.TopicTypes.FirstOrDefault();
    Assert.That("Error", Is.EqualTo(type));
  }

  /// <summary>
  ///   The title is missing from the topic, it should throw an exception.
  /// </summary>
  [Test]
  [Category("BCF v2.1")]
  public void ParseBcfMissingTopicTitleTest() {
    string filePath = "Resources/Bcf/v2.1/MissingTitle.bcfzip";

    Assert.That(async () => {
      await using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
      await BcfToolkit.Utils.BcfExtensions.ParseMarkups<bcf21.Markup, bcf21.VisualizationInfo>(fileStream);
    }, Throws.Exception);
  }

}