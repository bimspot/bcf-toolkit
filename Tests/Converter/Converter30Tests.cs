using System.Threading.Tasks;
using bcf.Converter;
using NUnit.Framework;

namespace Tests.Converter;

[TestFixture]
public class Converter30Tests {
  [SetUp]
  public void Setup() {
    _converter = new Converter30();
  }

  private IConverter _converter = null!;

  [Test]
  public async Task BcfToJsonSimpleTest() {
    await _converter.BcfToJson(
      "Resources/Bcf/v3.0/ComponentSelection.bcfzip",
      "Resources/output/json/v3.0/ComponentSelection");
  }

  [Test]
  public void BcfToJsonBackwardCompatibilityTest() {
    // 2.1 -> 3.0 is not backward compatible
    Assert.That(async () => await _converter.BcfToJson(
      "Resources/Bcf/v2.1/AllPartsVisible.bcfzip",
      "Resources/output/json/v3.0/AllPartsVisible"), Throws.Exception);
  }

  [Test]
  public void BcfToJsonWrongPathTest() {
    Assert.That(async () => await _converter.BcfToJson(
      "Resources/Bcf/v3.0/Meszaros.bcfzip",
      "Resources/output/json/v3.0/Meszaros"), Throws.ArgumentException);
  }
}