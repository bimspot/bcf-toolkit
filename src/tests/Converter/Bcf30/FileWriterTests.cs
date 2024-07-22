
using System.Linq;
using System.Threading.Tasks;
using BcfToolkit.Builder.Bcf30;
using BcfToolkit.Converter.Bcf30;
using BcfToolkit.Model;
using NUnit.Framework;

namespace Tests.Converter.Bcf30;
[TestFixture]
public class FileWriterTests {
  [Test]
  public async Task WriteBcfWithCreatingZipEntryFromStreamTest() {
    var builder = new BcfBuilder();
    var documentData = new FileData {
      Mime = "data:text/plain;base64",
      Data = "SGVsbG8="
    };
    var bcf =
      builder
        .WithDefaults()
        .SetDocument(documentInfo => documentInfo
          .AddDocument(document => document
            .SetGuid("6eead889-88b3-46eb-9c30-75bfd2394d84")
            .SetFileName("SampleFile.txt")
            .SetDocumentData(documentData)))
        .Build();

    var stream = await FileWriter.SerializeAndWriteBcf(bcf);

    var bcfResultBuilder = new BcfBuilder();
    var bcfResult = await bcfResultBuilder
      .BuildFromStream(stream);

    Assert.That(
      bcf.Markups.FirstOrDefault()?.Topic.Title,
      Is.EqualTo(bcfResult.Markups.FirstOrDefault()?.Topic.Title));
    Assert.That(
      bcf.Document?.Documents?.FirstOrDefault()?.DocumentData?.Mime,
      Is.EqualTo("data:text/plain;base64"));
  }

  [Test]
  public async Task WriteBcfWithSavingXmlTmpTest() {
    var builder = new BcfBuilder();
    var bcf =
      builder.WithDefaults()
        .Build();

    var stream = await FileWriter.SerializeAndWriteBcf(bcf);

    var bcfResultBuilder = new BcfBuilder();
    var bcfResult = await bcfResultBuilder
      .BuildFromStream(stream);

    Assert.That(
      bcf.Markups.FirstOrDefault()?.Topic.Title,
      Is.EqualTo(bcfResult.Markups.FirstOrDefault()?.Topic.Title));
  }
}