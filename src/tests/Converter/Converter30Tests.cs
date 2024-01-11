using System.Collections.Generic;
using System.Threading.Tasks;
using BcfToolkit.Converter;
using NUnit.Framework;

namespace Tests.Converter;

[TestFixture]
public class Converter30Tests {
  [SetUp]
  public void Setup() {
    _converter = new BcfToolkit.Converter.Bcf30.Converter();
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