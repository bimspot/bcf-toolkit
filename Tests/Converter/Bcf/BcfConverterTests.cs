using System.Linq;
using System.Threading.Tasks;
using bcf.bcf21;
using bcf.Converter;
using NUnit.Framework;

namespace Tests.Converter.Bcf;

[TestFixture]
public class BcfConverterTests {
  [Test]
  public async Task Parse21BcfAllPartsVisibleTest() {
    var markups =
      await BcfConverter.ParseMarkups<Markup, VisualizationInfo>(
        "Resources/Bcf/v2.1/AllPartsVisible.bcfzip");
    var markup = markups.FirstOrDefault();
    Assert.AreEqual(1, markups.Count);
    Assert.AreEqual("All components of curtain wall visible",
      markup?.Topic.Title);
    Assert.AreEqual("pasi.paasiala@solibri.com", markup?.Topic.ModifiedAuthor);
  }
}