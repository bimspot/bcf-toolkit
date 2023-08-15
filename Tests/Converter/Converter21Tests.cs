using System.Threading.Tasks;
using bcf.Converter;
using NUnit.Framework;

namespace Tests.Converter;

[TestFixture]
public class Converter21Tests {
  private IConverter _converter = null!;

  [SetUp]
  public void Setup() {
    _converter = new Converter21();
  }


  [Test]
  public async Task BcfToJsonSimpleTest() {
    await _converter.BcfToJson(
      "Resources/Bcf/v2.1/AllPartsVisible.zip", 
      "Resources/output/json/v2.1/AllPartsVisible");
  }
    
  [Test]
  public async Task BcfToJsonWrongVersionTest() {
    await _converter.BcfToJson(
      "Resources/Bcf/v3.0/ComponentSelection.zip", 
      "Resources/output/json/v2.1/ComponentSelection");
  }
  
  [Test]
  public void BcfToJsonWrongPathTest() {
    Assert.That(async () => await _converter.BcfToJson(
      "Resources/Bcf/v3.0/Meszaros.zip",
      "Resources/output/json/v2.1/Meszaros"), Throws.ArgumentException);
  }
}