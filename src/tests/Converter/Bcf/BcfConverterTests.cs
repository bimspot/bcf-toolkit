using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using bcf21 = BcfToolkit.Model.Bcf21;
using bcf30 = BcfToolkit.Model.Bcf30;
using BcfToolkit;
using BcfToolkit.Model;
using NUnit.Framework;

namespace Tests.Converter.Bcf;

[TestFixture]
public class BcfConverterTests {
  /// <summary>
  ///   Topic with guid ee9a9498-698b-44ed-8ece-b3ae3b480a90 should have all
  ///   parts of decomposed wall (2_hQ1Rixj6lgHTra$L72O4) visible.
  /// </summary>
  [Test]
  [Category("BCF v2.1")]
  public async Task ParseBcfAllPartsVisibleTest() {
    await using FileStream stream = new FileStream("Resources/Bcf/v2.1/AllPartsVisible.bcfzip", FileMode.Open, FileAccess.Read);

    var markups =
      await BcfToolkit.Converter.BcfConverter.ParseMarkups<bcf21.Markup, bcf21.VisualizationInfo>(
        stream);
    var markup = markups.FirstOrDefault()!;
    Assert.AreEqual(1, markups.Count);
    Assert.AreEqual("ee9a9498-698b-44ed-8ece-b3ae3b480a90", markup.Topic.Guid);
    Assert.AreEqual("All components of curtain wall visible",
      markup.Topic.Title);
    Assert.AreEqual("pasi.paasiala@solibri.com", markup.Topic.ModifiedAuthor);
    var visInfo =
      (bcf21.VisualizationInfo)markup.Viewpoints.FirstOrDefault()
        ?.VisualizationInfo!;
    Assert.AreEqual("2_hQ1Rixj6lgHTra$L72O4",
      visInfo.Components.Visibility.Exceptions.FirstOrDefault()?.IfcGuid);
  }

  /// <summary>
  ///   Exactly three components should be selected.
  /// </summary>
  [Test]
  [Category("BCF v2.1")]
  public async Task ParseBcfComponentsSelection21Test() {
    await using FileStream stream = new FileStream("Resources/Bcf/v2.1/ComponentSelection.bcfzip", FileMode.Open, FileAccess.Read);

    var markups =
      await BcfToolkit.Converter.BcfConverter.ParseMarkups<bcf21.Markup, bcf21.VisualizationInfo>(
        stream);
    var markup = markups.FirstOrDefault();
    Assert.AreEqual(1, markups.Count);
    var visInfo =
      (bcf21.VisualizationInfo)markup?.Viewpoints.FirstOrDefault()
        ?.VisualizationInfo!;
    Assert.AreEqual(3, visInfo.Components.Selection.Count);
    Assert.AreEqual("1GU8BMEqHBQxVAbwRD$4Jj",
      visInfo.Components.Selection.FirstOrDefault()?.IfcGuid);
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
    await using FileStream stream = new FileStream("Resources/Bcf/v2.1/ExternalBIMSnippet.bcfzip", FileMode.Open, FileAccess.Read);

    var markups =
      await BcfToolkit.Converter.BcfConverter.ParseMarkups<bcf21.Markup, bcf21.VisualizationInfo>(
        stream);
    var markup = markups.FirstOrDefault()!;
    Assert.AreEqual(1, markups.Count);
    Assert.AreEqual("http://bimfiles.example.com/JsonElement.json",
      markup.Topic.BimSnippet.Reference);
  }

  /// <summary>
  ///   There should be two markups within the BCFZip.
  /// </summary>
  [Test]
  [Category("BCF v2.1")]
  public async Task ParseBcfMultipleMarkupsTest() {
    await using FileStream stream = new FileStream("Resources/Bcf/v2.1/MaximumInformation.bcfzip", FileMode.Open, FileAccess.Read);

    var markups =
      await BcfToolkit.Converter.BcfConverter.ParseMarkups<bcf21.Markup, bcf21.VisualizationInfo>(
        stream);
    Assert.AreEqual(2, markups.Count);
  }

  /// <summary>
  ///   No markups exception should be thrown.
  /// </summary>
  [Test]
  [Category("BCF v2.1")]
  public void ParseBcfNoMarkupsTest() {
    string filePath = "Resources/Bcf/v2.1/NoMakrups.bcfzip";

    Assert.That(async () => {
      await using FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
      await BcfToolkit.Converter.BcfConverter.ParseMarkups<bcf21.Markup, bcf21.VisualizationInfo>(fileStream);
    }, Throws.Exception);
  }

  /// <summary>
  ///   The topic should have a related topic, and that is available.
  ///   UPDATE: required property is missing Comment
  /// </summary>
  [Test]
  [Category("BCF v2.1")]
  public void ParseBcfRelatedTopics21Test() {
    string filePath = "Resources/Bcf/v2.1/RelatedTopics.bcfzip";

    Assert.That(async () => {
      await using FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
      await BcfToolkit.Converter.BcfConverter.ParseMarkups<bcf21.Markup, bcf21.VisualizationInfo>(fileStream);
    }, Throws.Exception);
  }

  /// <summary>
  ///   Nothing should be selected and only a wall is visible.
  ///   UPDATE: required property is missing Comment
  /// </summary>
  [Test]
  [Category("BCF v2.1")]
  public void ParseBcfSingleVisibleWallTest() {
    string filePath = "Resources/Bcf/v2.1/SingleVisibleWall.bcfzip";

    Assert.That(async () => {
      await using FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
      await BcfToolkit.Converter.BcfConverter.ParseMarkups<bcf21.Markup, bcf21.VisualizationInfo>(fileStream);
    }, Throws.Exception);
  }


  /// <summary>
  ///   The topic should have an assigned user.
  /// </summary>
  [Test]
  [Category("BCF v2.1")]
  public async Task ParseBcfUserAssignment21Test() {
    await using FileStream stream = new FileStream("Resources/Bcf/v2.1/UserAssignment.bcfzip", FileMode.Open, FileAccess.Read);

    var markups =
      await BcfToolkit.Converter.BcfConverter.ParseMarkups<bcf21.Markup, bcf21.VisualizationInfo>(
        stream);
    var markup = markups.FirstOrDefault()!;
    Assert.AreEqual("jon.anders.sollien@catenda.no", markup.Topic.AssignedTo);
  }

  /// <summary>
  ///   Exactly three components should be selected.
  /// </summary>
  [Test]
  [Category("BCF v3.0")]
  public async Task ParseBcfComponentsSelection30Test() {
    await using FileStream stream = new FileStream("Resources/Bcf/v3.0/ComponentSelection.bcfzip", FileMode.Open, FileAccess.Read);

    var markups =
      await BcfToolkit.Converter.BcfConverter.ParseMarkups<bcf30.Markup, bcf30.VisualizationInfo>(
        stream);
    var markup = markups.FirstOrDefault();
    Assert.AreEqual(1, markups.Count);
    var visInfo =
      (bcf30.VisualizationInfo)markup?.Topic.Viewpoints.FirstOrDefault()
        ?.VisualizationInfo!;
    var header = markup?.Header!;

    Assert.AreEqual(2, header.Files.Count);
    Assert.AreEqual("Architectural.ifc",
      header.Files.FirstOrDefault()?.Filename);
    Assert.AreEqual("0KkZ20so9BsO1d1hFcfLOl",
      visInfo.Components.Selection.FirstOrDefault()?.IfcGuid);
  }

  /// <summary>
  ///   It should have the absolute reference to the document outside of the
  ///   BCFZip as given in the DocumentReferences property in the markup.bcf file.
  /// </summary>
  [Test]
  [Category("BCF v3.0")]
  public async Task ParseBcfDocumentRefExternalTest() {
    await using FileStream stream = new FileStream("Resources/Bcf/v3.0/DocumentReferenceExternal.bcfzip", FileMode.Open, FileAccess.Read);

    var markups =
      await BcfToolkit.Converter.BcfConverter.ParseMarkups<bcf30.Markup, bcf30.VisualizationInfo>(
          stream);
    var markup = markups.FirstOrDefault()!;
    Assert.AreEqual(1, markups.Count);
    Assert.AreEqual(
      "http://www.buildingsmart-tech.org/specifications/bcf-releases/bcfxml-v1/markup.xsd/at_download/file",
      markup.Topic.DocumentReferences.FirstOrDefault()?.Url);
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
    await using FileStream markupStream = new FileStream("Resources/Bcf/v3.0/DocumentReferenceInternal.bcfzip", FileMode.Open, FileAccess.Read);
    await using FileStream documentInfoStream = new FileStream("Resources/Bcf/v3.0/DocumentReferenceInternal.bcfzip", FileMode.Open, FileAccess.Read);

    var markups =
      await BcfToolkit.Converter.BcfConverter.ParseMarkups<bcf30.Markup, bcf30.VisualizationInfo>(
        markupStream);
    var documentInfo =
      await BcfToolkit.Converter.BcfConverter.ParseDocuments<bcf30.DocumentInfo>(
        documentInfoStream);
    var markup = markups.FirstOrDefault()!;
    Assert.AreEqual(1, markups.Count);
    var documentGuid = markup.Topic.DocumentReferences.FirstOrDefault()?.DocumentGuid;
    var document = documentInfo?.Documents.FirstOrDefault()!;
    Assert.AreEqual("b1d1b7f0-60b9-457d-ad12-16e0fb997bc5", documentGuid);
    Assert.AreEqual(documentGuid, document.Guid);
    Assert.AreEqual("ThisIsADocument.txt", document.Filename);
  }

  /// <summary>
  ///   Due date should be assigned to the topic.
  /// </summary>
  [Test]
  [Category("BCF v3.0")]
  public async Task ParseBcfDueDateTest() {
    await using FileStream stream = new FileStream("Resources/Bcf/v3.0/DueDate.bcfzip", FileMode.Open, FileAccess.Read);

    var markups =
      await BcfToolkit.Converter.BcfConverter
        .ParseMarkups<bcf30.Markup, bcf30.VisualizationInfo>(
          stream);
    var markup = markups.FirstOrDefault()!;
    Assert.AreEqual(1, markups.Count);
    Assert.AreEqual("2021-03-15T11:00:00.000Z",
      markup.Topic.DueDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
  }

  /// <summary>
  ///   It should display the "Architects" label in the topic.
  ///   The labels should be defined in the extensions.
  /// </summary>
  [Test]
  [Category("BCF v3.0")]
  public async Task ParseBcfLabelsTest() {
    await using FileStream markupsStream = new FileStream("Resources/Bcf/v3.0/Labels.bcfzip", FileMode.Open, FileAccess.Read);
    await using FileStream extensionsStream = new FileStream("Resources/Bcf/v3.0/Labels.bcfzip", FileMode.Open, FileAccess.Read);


    var markups =
      await BcfToolkit.Converter.BcfConverter
        .ParseMarkups<bcf30.Markup, bcf30.VisualizationInfo>(
          markupsStream);
    var markup = markups.FirstOrDefault()!;
    var extensions =
      await BcfToolkit.Converter.BcfConverter.ParseExtensions<bcf30.Extensions>(
        extensionsStream);
    Assert.AreEqual(1, markups.Count);
    var label = markup.Topic.Labels.FirstOrDefault();
    Assert.AreEqual("Architects", label);
    var topicLabels = extensions.TopicLabels;
    Assert.Contains(label, topicLabels);
  }

  /// <summary>
  ///   The topic should have a milestone set.
  /// </summary>
  [Test]
  [Category("BCF v3.0")]
  public async Task ParseBcfStageTest() {
    await using FileStream markupsStream = new FileStream("Resources/Bcf/v3.0/Milestone.bcfzip", FileMode.Open, FileAccess.Read);
    await using FileStream extensionsStream = new FileStream("Resources/Bcf/v3.0/Milestone.bcfzip", FileMode.Open, FileAccess.Read);

    var markups =
      await BcfToolkit.Converter.BcfConverter
        .ParseMarkups<bcf30.Markup, bcf30.VisualizationInfo>(
          markupsStream);
    var markup = markups.FirstOrDefault()!;
    var extensions =
      await BcfToolkit.Converter.BcfConverter.ParseExtensions<bcf30.Extensions>(
        extensionsStream);
    Assert.AreEqual(1, markups.Count);
    var stage = markup.Topic.Stage;
    Assert.AreEqual("February", stage);
    var stages = extensions.Stages;
    Assert.Contains(stage, stages);
  }

  /// <summary>
  ///   The topic should have a related topic, and that is available.
  /// </summary>
  [Test]
  [Category("BCF v3.0")]
  public async Task ParseBcfRelatedTopics30Test() {
    await using FileStream stream = new FileStream("Resources/Bcf/v3.0/RelatedTopics.bcfzip", FileMode.Open, FileAccess.Read);

    var markups =
      await BcfToolkit.Converter.BcfConverter
        .ParseMarkups<bcf30.Markup, bcf30.VisualizationInfo>(
          stream);
    var markup1 = markups.FirstOrDefault()!;
    var markup2 = markups.ElementAt(1);
    Assert.AreEqual(2, markups.Count);
    Assert.AreEqual(markup1.Topic.Guid,
      markup2.Topic.RelatedTopics.FirstOrDefault()?.Guid);
  }

  /// <summary>
  ///   Nothing should be selected and the wall is hidden, but everything else
  ///   is visible.
  /// </summary>
  [Test]
  [Category("BCF v3.0")]
  public async Task ParseBcfSingleInvisibleWallTest() {
    await using FileStream stream = new FileStream("Resources/Bcf/v3.0/SingleInvisibleWall.bcfzip", FileMode.Open, FileAccess.Read);

    var markups =
      await BcfToolkit.Converter.BcfConverter
        .ParseMarkups<bcf30.Markup, bcf30.VisualizationInfo>(
          stream);
    var markup = markups.FirstOrDefault()!;
    var visInfo =
      (bcf30.VisualizationInfo)markup.Topic.Viewpoints.FirstOrDefault()
        ?.VisualizationInfo!;
    Assert.AreEqual(true, visInfo.Components.Visibility.DefaultVisibility);
    Assert.AreEqual("1E8YkwPMfB$h99jtn_uAjI",
      visInfo.Components.Visibility.Exceptions.FirstOrDefault()?.IfcGuid);
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
    await using FileStream stream = new FileStream("Resources/Bcf/v3.0/TopicsWithDifferentModelsVisible.bcfzip", FileMode.Open, FileAccess.Read);

    var markups =
      await BcfToolkit.Converter.BcfConverter
        .ParseMarkups<bcf30.Markup, bcf30.VisualizationInfo>(
          stream);
    var markupARC = markups.FirstOrDefault(m =>
      m.Topic.Title.Equals(
        "Topics with different model visible - Architectural"))!;
    var markupMEP = markups.FirstOrDefault(m =>
      m.Topic.Title.Equals("Topics with different model visible - MEP"))!;

    Assert.AreEqual(1, markupARC.Header.Files.Count);
    Assert.AreEqual("Architectural.ifc",
      markupARC.Header.Files.FirstOrDefault()?.Filename);
    Assert.AreEqual(1, markupMEP.Header.Files.Count);
    Assert.AreEqual("MEP.ifc",
      markupMEP.Header.Files.FirstOrDefault()?.Filename);
  }

  /// <summary>
  ///   The topic should have an assigned user.
  /// </summary>
  [Test]
  [Category("BCF v3.0")]
  public async Task ParseBcfUserAssignment30Test() {
    await using FileStream stream = new FileStream("Resources/Bcf/v3.0/UserAssignment.bcfzip", FileMode.Open, FileAccess.Read);

    var markups =
      await BcfToolkit.Converter.BcfConverter.ParseMarkups<bcf30.Markup, bcf30.VisualizationInfo>(
        stream);
    var markup = markups.FirstOrDefault()!;
    Assert.AreEqual("Architect@example.com", markup.Topic.AssignedTo);
  }

  /// <summary>
  ///   Testing the required parsing method.
  /// </summary>
  [Test]
  public async Task ParseRequiredObjectTest() {
    await using FileStream stream = new FileStream("Resources/Bcf/v3.0/Milestone.bcfzip", FileMode.Open, FileAccess.Read);

    var extensions =
      await BcfToolkit.Converter.BcfConverter.ParseExtensions<bcf30.Extensions>(
        stream);
    var type = extensions.TopicTypes.FirstOrDefault();
    Assert.AreEqual("Error", type);

    Assert.That(async () => await BcfToolkit.Converter.BcfConverter.ParseExtensions<bcf30.Extensions>(
      stream), Throws.Exception);
  }

  /// <summary>
  ///   The title is missing from the topic, it should throw an exception.
  /// </summary>
  [Test]
  [Category("BCF v2.1")]
  public void ParseBcfMissingTopicTitleTest() {
    string filePath = "Resources/Bcf/v2.1/MissingTitle.bcfzip";

    Assert.That(async () => {
      await using FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
      await BcfToolkit.Converter.BcfConverter.ParseMarkups<bcf21.Markup, bcf21.VisualizationInfo>(fileStream);
    }, Throws.Exception);
  }

}