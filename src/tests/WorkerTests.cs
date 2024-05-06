using System.Collections.Generic;
using System.IO;
using System.Linq;
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

  private Worker _worker;

  [Test]
  public async Task BuildBcfFromV21StreamSampleTests() {
    var samples = new List<string> {
      // "Resources/Bcf/v2.1/AllPartsVisible.bcfzip", // assigned to is empty
      "Resources/Bcf/v2.1/ComponentSelection.bcfzip",
      "Resources/Bcf/v2.1/ExternalBIMSnippet.bcfzip",
      "Resources/Bcf/v2.1/MaximumInformation.bcfzip",
      "Resources/Bcf/v2.1/MinimumInformation.bcfzip",
      // "Resources/Bcf/v2.1/RelatedTopics.bcfzip", // comment property is empty
      // "Resources/Bcf/v2.1/SingleVisibleWall.bcfzip", // comment property is empty
      // "Resources/Bcf/v2.1/UserAssignment.bcfzip" // description is empty
    };
    var tasks = samples.Select(async path => {
      await using var stream =
        new FileStream(path, FileMode.Open, FileAccess.Read);
      await _worker.BuildBcfFromStream(stream);
    }).ToArray();

    await Task.WhenAll(tasks);
  }

  [Test]
  public async Task BuildBcfFromV30StreamTest() {
    await using var stream = new FileStream(
      "Resources/Bcf/v3.0/DocumentReferenceExternal.bcfzip", 
      FileMode.Open, 
      FileAccess.Read);
    await _worker.BuildBcfFromStream(stream);
  }
}