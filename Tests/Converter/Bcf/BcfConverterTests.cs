using System.Linq;
using System.Threading.Tasks;
using bcf.bcf21;
using bcf.Converter;
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
    var markups =
      await BcfConverter.ParseMarkups<Markup, VisualizationInfo>(
        "Resources/Bcf/v2.1/AllPartsVisible.bcfzip");
    var markup = markups.FirstOrDefault()!;
    Assert.AreEqual(1, markups.Count);
    Assert.AreEqual("ee9a9498-698b-44ed-8ece-b3ae3b480a90", markup.Topic.Guid);
    Assert.AreEqual("All components of curtain wall visible",
      markup.Topic.Title);
    Assert.AreEqual("pasi.paasiala@solibri.com", markup.Topic.ModifiedAuthor);
    var visInfo =
      (VisualizationInfo)markup.Viewpoints.FirstOrDefault()
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
    var markups =
      await BcfConverter.ParseMarkups<Markup, VisualizationInfo>(
        "Resources/Bcf/v2.1/ComponentSelection.bcfzip");
    var markup = markups.FirstOrDefault();
    Assert.AreEqual(1, markups.Count);
    var visInfo =
      (VisualizationInfo)markup?.Viewpoints.FirstOrDefault()
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
    var markups =
      await BcfConverter.ParseMarkups<Markup, VisualizationInfo>(
        "Resources/Bcf/v2.1/ExternalBIMSnippet.bcfzip");
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
    var markups =
      await BcfConverter.ParseMarkups<Markup, VisualizationInfo>(
        "Resources/Bcf/v2.1/MaximumInformation.bcfzip");
    Assert.AreEqual(2, markups.Count);
  }

  /// <summary>
  ///   No markups exception should be thrown.
  /// </summary>
  [Test]
  [Category("BCF v2.1")]
  public void ParseBcfNoMarkupsTest() {
    Assert.That(async () =>
      await BcfConverter.ParseMarkups<Markup, VisualizationInfo>(
        "Resources/Bcf/v2.1/NoMakrups.bcfzip"), Throws.Exception);
  }

  /// <summary>
  ///   The topic should have a related topic, and that is available.
  /// </summary>
  [Test]
  [Category("BCF v2.1")]
  public async Task ParseBcfRelatedTopicTest() {
    var markups =
      await BcfConverter.ParseMarkups<Markup, VisualizationInfo>(
        "Resources/Bcf/v2.1/RelatedTopics.bcfzip");
    var markup1 = markups.FirstOrDefault()!;
    var markup2 = markups.ElementAt(1);
    Assert.AreEqual(2, markups.Count);
    Assert.AreEqual(markup1.Topic.Guid,
      markup2.Topic.RelatedTopic.FirstOrDefault()?.Guid);
  }

  /// <summary>
  ///   Nothing should be selected and only a wall is visible.
  /// </summary>
  [Test]
  [Category("BCF v2.1")]
  public async Task ParseBcfSingleVisibleWallTest() {
    var markups =
      await BcfConverter.ParseMarkups<Markup, VisualizationInfo>(
        "Resources/Bcf/v2.1/SingleVisibleWall.bcfzip");
    var markup = markups.FirstOrDefault()!;
    var visInfo =
      (VisualizationInfo)markup.Viewpoints.FirstOrDefault()
        ?.VisualizationInfo!;
    Assert.AreEqual(false, visInfo.Components.Visibility.DefaultVisibility);
    Assert.AreEqual("1E8YkwPMfB$h99jtn_uAjI",
      visInfo.Components.Visibility.Exceptions.FirstOrDefault()?.IfcGuid);
  }

  /// <summary>
  ///   The topic should have an assigned user.
  /// </summary>
  [Test]
  [Category("BCF v2.1")]
  public async Task ParseBcfUserAssignmentTest() {
    var markups =
      await BcfConverter.ParseMarkups<Markup, VisualizationInfo>(
        "Resources/Bcf/v2.1/UserAssignment.bcfzip");
    var markup = markups.FirstOrDefault()!;
    Assert.AreEqual("jon.anders.sollien@catenda.no", markup.Topic.AssignedTo);
  }

  /// <summary>
  ///   Exactly three components should be selected.
  /// </summary>
  [Test]
  [Category("BCF v3.0")]
  public async Task ParseBcfComponentsSelection30Test() {
    var markups =
      await BcfConverter
        .ParseMarkups<bcf.bcf30.Markup, bcf.bcf30.VisualizationInfo>(
          "Resources/Bcf/v3.0/ComponentSelection.bcfzip");
    var markup = markups.FirstOrDefault();
    Assert.AreEqual(1, markups.Count);
    var visInfo =
      (bcf.bcf30.VisualizationInfo)markup?.Topic.Viewpoints.FirstOrDefault()
        ?.VisualizationInfo!;
    var header = markup?.Header!;

    Assert.AreEqual(2, header.Files.Length);
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
    var markups =
      await BcfConverter
        .ParseMarkups<bcf.bcf30.Markup, bcf.bcf30.VisualizationInfo>(
          "Resources/Bcf/v3.0/DocumentReferenceExternal.bcfzip");
    var markup = markups.FirstOrDefault()!;
    Assert.AreEqual(1, markups.Count);
    Assert.AreEqual("http://www.buildingsmart-tech.org/specifications/bcf-releases/bcfxml-v1/markup.xsd/at_download/file", markup.Topic.DocumentReferences.FirstOrDefault()?.Item);
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
    var markups =
      await BcfConverter
        .ParseMarkups<bcf.bcf30.Markup, bcf.bcf30.VisualizationInfo>(
          "Resources/Bcf/v3.0/DocumentReferenceInternal.bcfzip");
    var documentInfo = await BcfConverter.ParseDocuments<bcf.bcf30.DocumentInfo>("Resources/Bcf/v3.0/DocumentReferenceInternal.bcfzip");
    var markup = markups.FirstOrDefault()!;
    Assert.AreEqual(1, markups.Count);
    var documentGuid = markup.Topic.DocumentReferences.FirstOrDefault()?.Item;
    var document = documentInfo?.Documents.FirstOrDefault()!;
    Assert.AreEqual("b1d1b7f0-60b9-457d-ad12-16e0fb997bc5", documentGuid);
    Assert.AreEqual(documentGuid, document.Guid);
    Assert.AreEqual("ThisIsADocument.txt", document.Filename);
  }
}