using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using BcfToolkit.Utils;
using NUnit.Framework;

namespace tests.Utils;

[TestFixture]
public class ZipArchiveEntryExtensionsTests {
  [Test]
  public async Task CreateSnapshotTest() {
    await using var stream = new FileStream(
      "Resources/Bcf/v2.1/AllPartsVisible.bcfzip",
      FileMode.Open,
      FileAccess.Read);
    using var archive = new ZipArchive(stream, ZipArchiveMode.Read);
    var entry = archive.Entries.Where(ZipArchiveEntryExtensions.IsSnapshot).FirstOrDefault();
    var res = entry?.Snapshot();
    Assert.That(res?.Key, Is.EqualTo("snapshot.png"));
  }
}