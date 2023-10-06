using System.Collections.Generic;
using System.Threading.Tasks;
using BcfConverter.Converter;
using NUnit.Framework;

namespace Tests.Converter;

[TestFixture]
public class Converter21Tests {
  [SetUp]
  public void Setup() {
    _converter = new BcfConverter.Converter.Bcf21.Converter();
  }

  private IConverter _converter = null!;

  [Test]
  public async Task BcfToJsonSampleFilesTest() {
    var tasks = new List<Task> {
      _converter.BcfToJson(
        "Resources/Bcf/v2.1/AllPartsVisible.bcfzip",
        "Resources/output/json/v2.1/AllPartsVisible"),
      _converter.BcfToJson(
        "Resources/Bcf/v2.1/ComponentSelection.bcfzip",
        "Resources/output/json/v2.1/ComponentSelection"),
      _converter.BcfToJson(
        "Resources/Bcf/v2.1/ExternalBIMSnippet.bcfzip",
        "Resources/output/json/v2.1/ExternalBIMSnippet"),
      _converter.BcfToJson(
        "Resources/Bcf/v2.1/MaximumInformation.bcfzip",
        "Resources/output/json/v2.1/MaximumInformation"),
      _converter.BcfToJson(
        "Resources/Bcf/v2.1/UserAssignment.bcfzip",
        "Resources/output/json/v2.1/UserAssignment")
    };

    await Task.WhenAll(tasks);
  }

  [Test]
  public async Task JsonToBcfSampleFilesTest() {
    var tasks = new List<Task> {
      _converter.JsonToBcf(
        "Resources/Json/v2.1/AllPartsVisible",
        "Resources/output/bcf/v2.1/AllPartsVisible.bcfzip")
    };

    await Task.WhenAll(tasks);
  }

  [Test]
  public void BcfToJsonMissingRequiredPropertyTest() {
    Assert.That(async () => await _converter.BcfToJson(
      "Resources/Bcf/v2.1/RelatedTopics.bcfzip",
       "Resources/output/json/v2.1/RelatedTopics"), Throws.ArgumentException);
  }

  [Test]
  public void BcfToJsonWrongPathTest() {
    Assert.That(async () => await _converter.BcfToJson(
      "Resources/Bcf/v3.0/Meszaros.bcfzip",
      "Resources/output/json/v2.1/Meszaros"), Throws.ArgumentException);
  }
}