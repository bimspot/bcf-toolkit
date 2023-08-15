using System.Threading.Tasks;
using bcf.Converter;
using NUnit.Framework;

namespace Tests.Converter;

[TestFixture]
public class Converter21Tests {
  [SetUp]
  public void Setup() {
    _converter = new Converter21();
  }

  private IConverter _converter = null!;


  [Test]
  public async Task BcfToJsonSimpleTest() {
    await _converter.BcfToJson(
      "Resources/Bcf/v2.1/AllPartsVisible.bcfzip",
      "Resources/output/json/v2.1/AllPartsVisible");
  }

  [Test]
  public async Task BcfToJsonWrongVersionTest() {
    await _converter.BcfToJson(
      "Resources/Bcf/v3.0/ComponentSelection.bcfzip",
      "Resources/output/json/v2.1/ComponentSelection");
  }

  [Test]
  public void BcfToJsonWrongPathTest() {
    Assert.That(async () => await _converter.BcfToJson(
      "Resources/Bcf/v3.0/Meszaros.bcfzip",
      "Resources/output/json/v2.1/Meszaros"), Throws.ArgumentException);
  }
}