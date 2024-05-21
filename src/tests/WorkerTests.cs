using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using BcfToolkit;
using BcfToolkit.Builder.Bcf21;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf30;
using BcfToolkit.Utils;
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
  [Category("BCF v2.1")]
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
  [Category("BCF v3.0")]
  public async Task BuildBcfFromV30StreamTest() {
    await using var stream = new FileStream(
      "Resources/Bcf/v3.0/DocumentReferenceExternal.bcfzip",
      FileMode.Open,
      FileAccess.Read);
    await _worker.BuildBcfFromStream(stream);
  }

  [Test]
  [Category("BCF v2.1")]
  public async Task GetBcfV30StreamFromV21ObjectTest() {
    var builder = new BcfBuilder();
    var bcf = builder
      .AddMarkup(m => m
        .SetGuid("3ffb4df2-0187-49a9-8a4a-23992696bafd")
        .SetTitle("This is a new topic")
        .SetCreationDate(new DateTime())
        .SetCreationAuthor("Creator"))
      .Build();
    var stream = await _worker.ToBcfStream(bcf, BcfVersionEnum.Bcf30);
    var version = await BcfExtensions.GetVersionFromStreamArchive(stream);
    Assert.That(BcfVersionEnum.Bcf30, Is.EqualTo(version));
    await stream.FlushAsync();
  }

  [Test]
  [Category("BCF v3.0")]
  public async Task GetBcfV30StreamFromV30ObjectTest() {
    var builder = new BcfToolkit.Builder.Bcf30.BcfBuilder();
    var bcf = builder
      .AddMarkup(m => m
        .SetGuid("3ffb4df2-0187-49a9-8a4a-23992696bafd")
        .SetTitle("This is a new topic")
        .SetCreationDate(new DateTime())
        .SetCreationAuthor("Creator")
        .SetTopicType("Issue")
        .SetTopicStatus("Open"))
      .SetExtensions(e => e.WithDefaults())
      .Build();
    var stream = await _worker.ToBcfStream(bcf, BcfVersionEnum.Bcf30);
    var version = await BcfExtensions.GetVersionFromStreamArchive(stream);
    Assert.That(BcfVersionEnum.Bcf30, Is.EqualTo(version));
    await stream.FlushAsync();
  }

  [Test]
  [Category("BCF v3.0")]
  public async Task GetBcfV21StreamFromV30ObjectTest() {
    var builder = new BcfToolkit.Builder.Bcf30.BcfBuilder();
    var bcf = builder
      .AddMarkup(m => m
        .SetGuid("3ffb4df2-0187-49a9-8a4a-23992696bafd")
        .SetTitle("This is a new topic")
        .SetTopicStatus("Open")
        .SetTopicType("Issue")
        .SetCreationDate(new DateTime())
        .SetCreationAuthor("Creator"))
      .SetExtensions(e => e.WithDefaults())
      .Build();
    var stream = await _worker.ToBcfStream(bcf, BcfVersionEnum.Bcf21);
    var version = await BcfExtensions.GetVersionFromStreamArchive(stream);
    Assert.That(BcfVersionEnum.Bcf21, Is.EqualTo(version));
    await stream.FlushAsync();
  }
}