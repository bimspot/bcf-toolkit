using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using bcf21 = BcfToolkit.Model.Bcf21;
using bcf30 = BcfToolkit.Model.Bcf30;
using BcfToolkit.Utils;

namespace Tests.Utils.Json;

public class JsonExtensionsTests {
  /// <summary>
  ///   Topic with guid ee9a9498-698b-44ed-8ece-b3ae3b480a90 should have all
  ///   parts of decomposed wall (2_hQ1Rixj6lgHTra$L72O4) visible.
  /// </summary>
  [Test]
  [Category("BCF v2.1")]
  public async Task ParseJsonAllPartsVisibleTest() {
    var markups =
      await JsonExtensions.ParseMarkups<bcf21.Markup>(
        "Resources/Json/v2.1/AllPartsVisible");
    var markup = (bcf21.Markup)markups.FirstOrDefault()!;
    Assert.AreEqual(1, markups.Count);
    Assert.AreEqual("ee9a9498-698b-44ed-8ece-b3ae3b480a90",
      markup.Topic.Guid);
    Assert.AreEqual("All components of curtain wall visible",
      markup.Topic.Title);
    Assert.AreEqual("pasi.paasiala@solibri.com",
      markup.Topic.ModifiedAuthor);
    var visInfo =
      markup.Viewpoints.FirstOrDefault()
        ?.VisualizationInfo!;
    Assert.AreEqual("2_hQ1Rixj6lgHTra$L72O4",
      visInfo.Components.Visibility.Exceptions.FirstOrDefault()?.IfcGuid);
  }

  /// <summary>
  ///   It should skip the txt file, because it is not json.
  /// </summary>
  [Test]
  [Category("BCF v2.1")]
  public async Task ParseJsonSkippingFilesTest() {
    var markups =
      await JsonExtensions.ParseMarkups<bcf21.Markup>(
        "Resources/Json/v2.1/SkippingFiles");
    var markup = (bcf21.Markup)markups.FirstOrDefault()!;
    Assert.AreEqual(0, markups.Count);
  }

  /// <summary>
  ///   It should throw an exception, because of missing fields.
  /// </summary>
  [Test]
  [Category("BCF v2.1")]
  public void ParseJsonMissingRequiredFieldsTest() {
    Assert.That(async () => await JsonExtensions.ParseMarkups<bcf21.Markup>(
        "Resources/Json/v2.1/MissingRequiredFields"),
      Throws.Exception);
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
      await JsonExtensions
        .ParseMarkups<bcf30.Markup>(
          "Resources/Json/v3.0/DocumentReferenceInternal");
    var doc =
      await JsonExtensions.ParseObject<bcf30.DocumentInfo>(
        "Resources/Json/v3.0/DocumentReferenceInternal/documents.json");
    var markup = (bcf30.Markup)markups.FirstOrDefault()!;
    Assert.AreEqual(1, markups.Count);
    var documentGuid = markup.Topic.DocumentReferences.FirstOrDefault()?.DocumentGuid;
    var document = doc.Documents.FirstOrDefault()!;
    Assert.AreEqual("b1d1b7f0-60b9-457d-ad12-16e0fb997bc5", documentGuid);
    Assert.AreEqual(documentGuid, document.Guid);
    Assert.AreEqual("ThisIsADocument.txt", document.Filename);
  }

  [Test]
  [Category("BCF v3.0")]
  public void ParseEmptyBcfProjectTest() {
    Assert.That(async () => await JsonExtensions.ParseObject<bcf30.ProjectInfo>(
      "Resources/Json/v3.0/EmptyBcfRoot/project.json"), Throws.Exception);
  }

  // [Test]
  // [Category("BCF v3.0")]
  // public void ParseEmptyBcfDocumentTest() {
  //   Assert.That(async () => await JsonConverter.ParseObject<bcf30.DocumentInfo>(
  //     "Resources/Json/v3.0/EmptyBcfRoot/documents.json"), Throws.Exception);
  // }
  //
  // [Test]
  // [Category("BCF v3.0")]
  // public void ParseEmptyBcfExtensionsTest() {
  //   Assert.That(async () => await JsonConverter.ParseObject<bcf30.Extensions>(
  //     "Resources/Json/v3.0/EmptyBcfRoot/extensions.json"), Throws.Exception);
  // }
}