using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BcfToolkit.Utils;
using BcfToolkit.Model.Bcf30;
using BcfToolkit.Worker;
using NUnit.Framework;

namespace tests.Worker.Bcf30;

[TestFixture]
public class ConverterWorkerTests {
  [SetUp]
  public void Setup() {
    _converterWorker = new BcfToolkit.Worker.Bcf30.ConverterWorker();
  }

  private IConverterWorker _converterWorker = null!;

  [Test]
  public void BcfToJsonSampleFilesTest() {

    _converterWorker.BcfZipToJson(
      "Resources/Bcf/v3.0/ComponentSelection.bcfzip",
      "Resources/output/json/v3.0/ComponentSelection");
    _converterWorker.BcfZipToJson(
      "Resources/Bcf/v3.0/DocumentReferenceExternal.bcfzip",
      "Resources/output/json/v3.0/DocumentReferenceExternal");
    _converterWorker.BcfZipToJson(
      "Resources/Bcf/v3.0/DocumentReferenceInternal.bcfzip",
      "Resources/output/json/v3.0/DocumentReferenceInternal");
    _converterWorker.BcfZipToJson(
      "Resources/Bcf/v3.0/DueDate.bcfzip",
      "Resources/output/json/v3.0/DueDate");
    _converterWorker.BcfZipToJson(
      "Resources/Bcf/v3.0/Labels.bcfzip",
      "Resources/output/json/v3.0/Labels");
    _converterWorker.BcfZipToJson(
      "Resources/Bcf/v3.0/Milestone.bcfzip",
      "Resources/output/json/v3.0/Milestone");
    _converterWorker.BcfZipToJson(
      "Resources/Bcf/v3.0/RelatedTopics.bcfzip",
      "Resources/output/json/v3.0/RelatedTopics");
    _converterWorker.BcfZipToJson(
      "Resources/Bcf/v3.0/SingleInvisibleWall.bcfzip",
      "Resources/output/json/v3.0/SingleInvisibleWall");
    _converterWorker.BcfZipToJson(
      "Resources/Bcf/v3.0/TestBcf30.bcfzip",
      "Resources/output/json/v3.0/TestBcf30");
    _converterWorker.BcfZipToJson(
      "Resources/Bcf/v3.0/TopicsWithDifferentModelsVisible.bcfzip",
      "Resources/output/json/v3.0/TopicsWithDifferentModelsVisible");
    _converterWorker.BcfZipToJson(
      "Resources/Bcf/v3.0/UserAssignment.bcfzip",
      "Resources/output/json/v3.0/UserAssignment");
  }

  [Test]
  public Task JsonToBcfSampleFilesTest() {
    var tasks = new List<Task> {
      _converterWorker.JsonToBcfZip(
        "Resources/Json/v3.0/DocumentReferenceInternal",
        "Resources/output/Bcf/v3.0/DocumentReferenceInternal.bcfzip"),
    };

    return Task.WhenAll(tasks);
  }

  [Test]
  public async Task BcfStream_ShouldReturnFileStream() {
    await using var stream = new FileStream("Resources/Bcf/v3.0/DocumentReferenceExternal.bcfzip", FileMode.Open, FileAccess.Read);

    var extensions =
      await BcfExtensions.ParseExtensions<Extensions>(stream);
    var projectInfo = await BcfExtensions.ParseProject<ProjectInfo>(stream);
    var documentInfo = await BcfExtensions.ParseDocuments<DocumentInfo>(stream);

    var markups =
      await BcfExtensions.ParseMarkups<Markup, VisualizationInfo>(stream);

    var bcf = new Bcf {
      Markups = markups,
      Extensions = extensions,
      Project = projectInfo,
      Document = documentInfo
    };

    var streamResult = await _converterWorker.ToBcfStream(bcf);

    Assert.IsNotNull(streamResult);
    Assert.IsTrue(streamResult.CanRead);

    await streamResult.DisposeAsync();
  }

  [Test]
  public void BcfToJsonBackwardCompatibilityTest() {
    // 2.1 -> 3.0 is not backward compatible
    Assert.That(async () => await _converterWorker.BcfZipToJson(
      "Resources/Bcf/v2.1/AllPartsVisible.bcfzip",
      "Resources/output/Json/v3.0/AllPartsVisible"), Throws.Exception);
  }

  [Test]
  public void BcfToJsonWrongPathTest() {
    Assert.That(async () => await _converterWorker.BcfZipToJson(
      "Resources/Bcf/v3.0/Wrong.bcfzip",
      "Resources/output/json/v3.0/Wrong"), Throws.Exception);
  }

  [Test]
  public void JsonToBcfMissingBcfRootTest() {
    Assert.That(async () => await _converterWorker.BcfZipToJson(
      "Resources/json/v3.0/MissingBcfRoot.bcfzip",
      "Resources/output/Bcf/v3.0/MissingBcfRoot"), Throws.Exception);
  }
}