using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BcfToolkit.Converter;
using BcfToolkit.Model.Bcf30;
using NUnit.Framework;

namespace tests.Worker;

[TestFixture]
public class Converter30Tests {
  [SetUp]
  public void Setup() {
    _converter = new BcfToolkit.Worker.Bcf30.Converter();
  }

  private IConverter _converter = null!;

  [Test]
  public void BcfToJsonSampleFilesTest() {

    _converter.BcfToJson(
      "Resources/Bcf/v3.0/ComponentSelection.bcfzip",
      "Resources/output/json/v3.0/ComponentSelection");
    _converter.BcfToJson(
      "Resources/Bcf/v3.0/DocumentReferenceExternal.bcfzip",
      "Resources/output/json/v3.0/DocumentReferenceExternal");
    _converter.BcfToJson(
      "Resources/Bcf/v3.0/DocumentReferenceInternal.bcfzip",
      "Resources/output/json/v3.0/DocumentReferenceInternal");
    _converter.BcfToJson(
      "Resources/Bcf/v3.0/DueDate.bcfzip",
      "Resources/output/json/v3.0/DueDate");
    _converter.BcfToJson(
      "Resources/Bcf/v3.0/Labels.bcfzip",
      "Resources/output/json/v3.0/Labels");
    _converter.BcfToJson(
      "Resources/Bcf/v3.0/Milestone.bcfzip",
      "Resources/output/json/v3.0/Milestone");
    _converter.BcfToJson(
      "Resources/Bcf/v3.0/RelatedTopics.bcfzip",
      "Resources/output/json/v3.0/RelatedTopics");
    _converter.BcfToJson(
      "Resources/Bcf/v3.0/SingleInvisibleWall.bcfzip",
      "Resources/output/json/v3.0/SingleInvisibleWall");
    _converter.BcfToJson(
      "Resources/Bcf/v3.0/TestBcf30.bcfzip",
      "Resources/output/json/v3.0/TestBcf30");
    _converter.BcfToJson(
      "Resources/Bcf/v3.0/TopicsWithDifferentModelsVisible.bcfzip",
      "Resources/output/json/v3.0/TopicsWithDifferentModelsVisible");
    _converter.BcfToJson(
      "Resources/Bcf/v3.0/UserAssignment.bcfzip",
      "Resources/output/json/v3.0/UserAssignment");
  }

  [Test]
  public Task JsonToBcfSampleFilesTest() {
    var tasks = new List<Task> {
      _converter.JsonToBcf(
        "Resources/Json/v3.0/DocumentReferenceInternal",
        "Resources/output/Bcf/v3.0/DocumentReferenceInternal.bcfzip"),
    };

    return Task.WhenAll(tasks);
  }

  [Test]
  public async Task BcfStream_ShouldReturnFileStream() {
    await using var stream = new FileStream("Resources/Bcf/v3.0/DocumentReferenceExternal.bcfzip", FileMode.Open, FileAccess.Read);

    var extensions =
      await BcfConverter.ParseExtensions<Extensions>(stream);
    var projectInfo = await BcfConverter.ParseProject<ProjectInfo>(stream);
    var documentInfo = await BcfConverter.ParseDocuments<DocumentInfo>(stream);

    var markups =
      await BcfConverter.ParseMarkups<Markup, VisualizationInfo>(stream);

    var bcf = new Bcf {
      Markups = markups,
      Extensions = extensions,
      Project = projectInfo,
      Document = documentInfo
    };

    var streamResult = await _converter.BcfStream(bcf);

    Assert.IsNotNull(streamResult);
    Assert.IsTrue(streamResult.CanRead);

    await streamResult.DisposeAsync();
  }

  [Test]
  public void BcfToJsonBackwardCompatibilityTest() {
    // 2.1 -> 3.0 is not backward compatible
    Assert.That(async () => await _converter.BcfToJson(
      "Resources/Bcf/v2.1/AllPartsVisible.bcfzip",
      "Resources/output/Json/v3.0/AllPartsVisible"), Throws.Exception);
  }

  [Test]
  public void BcfToJsonWrongPathTest() {
    Assert.That(async () => await _converter.BcfToJson(
      "Resources/Bcf/v3.0/Meszaros.bcfzip",
      "Resources/output/json/v3.0/Meszaros"), Throws.ArgumentException);
  }

  [Test]
  public void JsonToBcfMissingBcfRootTest() {
    Assert.That(async () => await _converter.BcfToJson(
      "Resources/json/v3.0/MissingBcfRoot.bcfzip",
      "Resources/output/Bcf/v3.0/MissingBcfRoot"), Throws.Exception);
  }
}