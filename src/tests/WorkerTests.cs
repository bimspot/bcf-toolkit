using System.IO;
using System.Threading.Tasks;
using BcfToolkit;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class WorkerTests {
  [SetUp]
  public void Setup() {
    _worker = new Worker();
  }

  private Worker _worker = null!;

  [Test]
  public async Task BuildBcfFromV21StreamTest() {
    await using var stream = new FileStream("Resources/Bcf/v2.1/AllPartsVisible.bcfzip", FileMode.Open, FileAccess.Read);
    var bcf = await _worker.BuildBcfFromStream(stream);
  }

  [Test]
  public async Task BuildBcfFromV30StreamTest() {
    await using var stream = new FileStream("Resources/Bcf/v3.0/DocumentReferenceExternal.bcfzip", FileMode.Open, FileAccess.Read);
    var bcf = await _worker.BuildBcfFromStream(stream);

  }
}