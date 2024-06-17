using System.Linq;
using System.Threading.Tasks;
using BcfToolkit.Builder.Bcf21;
using BcfToolkit.Converter.Bcf21;
using NUnit.Framework;

namespace Tests.Converter.Bcf21;

[TestFixture]
public class FileWriterTests {
  [Test]
  public async Task WriteBcfWithCreatingZipEntryFromStreamTest() {
    var builder = new BcfBuilder();
    var bcf =
      builder.WithDefaults()
        .Build();

    var stream = await FileWriter.SerializeAndWriteBcf(bcf, null);

    var bcfResultBuilder = new BcfBuilder();
    var bcfResult = await bcfResultBuilder
      .BuildFromStream(stream);

    Assert.That(
      bcf.Markups.FirstOrDefault()?.Topic.Title,
      Is.EqualTo(bcfResult.Markups.FirstOrDefault()?.Topic.Title));
  }

  [Test]
  public async Task WriteBcfWithSavingXmlTmpTest() {
    var builder = new BcfBuilder();
    var bcf =
      builder.WithDefaults()
        .Build();

    var stream = await FileWriter.SerializeAndWriteBcf(bcf, null);

    var bcfResultBuilder = new BcfBuilder();
    var bcfResult = await bcfResultBuilder
      .BuildFromStream(stream);

    Assert.That(
      bcf.Markups.FirstOrDefault()?.Topic.Title,
      Is.EqualTo(bcfResult.Markups.FirstOrDefault()?.Topic.Title));
  }
}