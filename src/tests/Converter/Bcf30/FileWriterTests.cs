
using System.Linq;
using System.Threading.Tasks;
using BcfToolkit.Builder.Bcf30;
using BcfToolkit.Converter.Bcf30;
using NUnit.Framework;

namespace Tests.Converter.Bcf30;
[TestFixture]
public class FileWriterTests {
  // [Test]
  // public async Task WriteBcfWithStreamTest() {
  //   var builder = new BcfBuilder();
  //
  //   var bcf =
  //     builder.WithDefaults()
  //     .Build();
  //
  //   var stream = await FileWriter.WriteBcfToStream(bcf);
  //
  //   stream.Position = 0;
  //
  //   var bcfResultBuilder = new BcfBuilder();
  //
  //   var bcfResult =
  //     await bcfResultBuilder
  //     .BuildFromStream(stream);
  //
  //   Assert.That(bcf.Markups.FirstOrDefault()?.Topic.Title, Is.EqualTo(bcfResult.Markups.FirstOrDefault()?.Topic.Title));
  //
  // }

}