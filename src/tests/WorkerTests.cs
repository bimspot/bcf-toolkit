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
  public async Task BuildBcfFromV21StreamTest() {
    const string path = "Resources/Bcf/v2.1/MiniSolibri.bcfzip";
    await using var stream =
      new FileStream(path, FileMode.Open, FileAccess.Read);
    await _worker.BcfFromStream(stream);
  }

  [Test]
  [Category("BCF v2.1")]
  public async Task BuildBcfFromV21StreamSamplesTests() {
    var samples = new List<string> {
      // "Resources/Bcf/v2.1/AllPartsVisible.bcfzip", // assigned to is empty
      "Resources/Bcf/v2.1/ComponentSelection.bcfzip",
      "Resources/Bcf/v2.1/ExternalBIMSnippet.bcfzip",
      "Resources/Bcf/v2.1/MaximumInformation.bcfzip",
      "Resources/Bcf/v2.1/MinimumInformation.bcfzip",
      "Resources/Bcf/v2.1/MiniSolibri.bcfzip",
      // "Resources/Bcf/v2.1/RelatedTopics.bcfzip", // comment property is empty
      // "Resources/Bcf/v2.1/SingleVisibleWall.bcfzip", // comment property is empty
      // "Resources/Bcf/v2.1/UserAssignment.bcfzip" // description is empty
    };

    var tasks = samples.Select(async path => {
      await using var stream =
        new FileStream(path, FileMode.Open, FileAccess.Read);
      await _worker.BcfFromStream(stream);
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
    await _worker.BcfFromStream(stream);
  }

  [Test]
  [Category("BCF v2.1")]
  public async Task BcfV30StreamFromV21ObjectTest() {
    var builder = new BcfBuilder();
    var bcf = builder
      .AddMarkup(m => m
        .SetGuid("3ffb4df2-0187-49a9-8a4a-23992696bafd")
        .SetTitle("This is a new topic")
        .SetCreationDate(new DateTime())
        .SetCreationAuthor("Creator"))
      .Build();
    await CheckBcfStreamVersion(bcf, BcfVersionEnum.Bcf30);
  }

  [Test]
  [Category("BCF v2.1")]
  public async Task BcfV21StreamFromV21ObjectTest() {
    var builder = new BcfBuilder();
    var bcf = builder
      .AddMarkup(m => m
        .SetGuid("3ffb4df2-0187-49a9-8a4a-23992696bafd")
        .SetTitle("This is a new topic")
        .SetCreationDate(new DateTime())
        .SetCreationAuthor("Creator"))
      .Build();
    await CheckBcfStreamVersion(bcf, BcfVersionEnum.Bcf21);
  }

  [Test]
  [Category("BCF v3.0")]
  public async Task BcfV30StreamFromV30ObjectTest() {
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
    await CheckBcfStreamVersion(bcf, BcfVersionEnum.Bcf30);
  }

  [Test]
  [Category("BCF v3.0")]
  public async Task BcfV21StreamFromV30ObjectTest() {
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
    await CheckBcfStreamVersion(bcf, BcfVersionEnum.Bcf21);
  }

  private async Task CheckBcfStreamVersion(
    IBcf bcf,
    BcfVersionEnum expectedVersion) {
    var stream = await _worker.ToBcf(bcf, expectedVersion);
    var version = await BcfExtensions.GetVersionFromStreamArchive(stream);
    Assert.That(expectedVersion, Is.EqualTo(version));
    await stream.FlushAsync();
  }

  [Test]
  [Category("BCF v3.0")]
  public async Task BcfV30ToV21StreamSamplesTests() {
    var samples = new List<string> {
      "Resources/Bcf/v3.0/ComponentSelection.bcfzip",
      "Resources/Bcf/v3.0/DocumentReferenceExternal.bcfzip",
      "Resources/Bcf/v3.0/DocumentReferenceInternal.bcfzip",
      "Resources/Bcf/v3.0/DueDate.bcfzip",
      "Resources/Bcf/v3.0/Labels.bcfzip",
      "Resources/Bcf/v3.0/Milestone.bcfzip",
      "Resources/Bcf/v3.0/RelatedTopics.bcfzip",
      "Resources/Bcf/v3.0/SingleInvisibleWall.bcfzip",
      "Resources/Bcf/v3.0/TestBcf30.bcfzip",
      "Resources/Bcf/v3.0/TopicsWithDifferentModelsVisible.bcfzip",
      "Resources/Bcf/v3.0/UserAssignment.bcfzip"
    };

    var tasks = samples.Select(async path => {
      var builder = new BcfToolkit.Builder.Bcf30.BcfBuilder();
      await using var inputStream =
        new FileStream(path, FileMode.Open, FileAccess.Read);
      var bcf = await builder.BuildInMemoryFromStream(inputStream);
      var stream = await _worker.ToBcf(bcf, BcfVersionEnum.Bcf21);
      var version = await BcfExtensions.GetVersionFromStreamArchive(stream);
      Assert.That(BcfVersionEnum.Bcf21, Is.EqualTo(version));
      await stream.FlushAsync();
    }).ToArray();

    await Task.WhenAll(tasks);
  }

  [Test]
  [Category("BCF v2.1")]
  public async Task ConvertBcfZipToJsonV21SamplesTests() {
    var samples = new List<string> {
      "Resources/Bcf/v2.1/ComponentSelection.bcfzip",
      "Resources/Bcf/v2.1/ExternalBIMSnippet.bcfzip",
      "Resources/Bcf/v2.1/MaximumInformation.bcfzip",
      "Resources/Bcf/v2.1/MinimumInformation.bcfzip",
    };
    var tasks = samples.Select(async source => {
      var target =
        $"Resources/output/json/{Directory.GetParent(source)?.Name}/{Path.GetFileNameWithoutExtension(source)}";
      await _worker.Convert(source, target);
    }).ToArray();

    await Task.WhenAll(tasks);
  }

  [Test]
  [Category("BCF v3.0")]
  public async Task ConvertBcfZipToJsonV30SamplesTests() {
    var samples = new List<string> {
      "Resources/Bcf/v3.0/ComponentSelection.bcfzip",
      "Resources/Bcf/v3.0/DocumentReferenceExternal.bcfzip",
      "Resources/Bcf/v3.0/DocumentReferenceInternal.bcfzip",
      "Resources/Bcf/v3.0/DueDate.bcfzip",
      "Resources/Bcf/v3.0/Labels.bcfzip",
      "Resources/Bcf/v3.0/Milestone.bcfzip",
      "Resources/Bcf/v3.0/RelatedTopics.bcfzip",
      "Resources/Bcf/v3.0/SingleInvisibleWall.bcfzip",
      "Resources/Bcf/v3.0/TestBcf30.bcfzip",
      "Resources/Bcf/v3.0/TopicsWithDifferentModelsVisible.bcfzip",
      "Resources/Bcf/v3.0/UserAssignment.bcfzip"
    };
    var tasks = samples.Select(async source => {
      var target =
        $"Resources/output/json/{Directory.GetParent(source)?.Name}/{Path.GetFileNameWithoutExtension(source)}";
      await _worker.Convert(source, target);
    }).ToArray();

    await Task.WhenAll(tasks);
  }

  [Test]
  [Category("BCF v2.1")]
  public async Task ConvertJsonToBcfZipV21SamplesTests() {
    var samples = new List<string> {
      "Resources/Json/v2.1/AllPartsVisible",
      "Resources/Json/v2.1/MissingBcfRoot",
      "Resources/Json/v2.1/SkippingFiles"
    };
    var tasks = samples.Select(async source => {
      var target =
        $"Resources/output/bcf/{Directory.GetParent(source)?.Name}/{Path.GetFileNameWithoutExtension(source)}";
      await _worker.Convert(source, target);
    }).ToArray();

    await Task.WhenAll(tasks);
  }

  [Test]
  [Category("BCF v3.0")]
  public async Task ConvertJsonToBcfZipV30SamplesTests() {
    var samples = new List<string> {
      "Resources/Json/v3.0/DocumentReferenceInternal",
    };
    var tasks = samples.Select(async source => {
      var target =
        $"Resources/output/bcf/{Directory.GetParent(source)?.Name}/{Path.GetFileNameWithoutExtension(source)}";
      await _worker.Convert(source, target);
    }).ToArray();

    await Task.WhenAll(tasks);
  }

  [Test]
  [Category("BCF v3.0")]
  public void ConvertJsonToBcfZipWrongVersionTests() {
    const string source = "Resources/Json/v3.0/WrongVersion";
    var target =
      $"Resources/output/bcf/{Directory.GetParent(source)?.Name}/{Path.GetFileNameWithoutExtension(source)}";
    Assert.That(
      async () => await _worker.Convert(source, target),
      Throws.Exception);
  }

  [Test]
  [Category("BCF v2.1")]
  public async Task ToBcfZipV21Tests() {
    var builder = new BcfBuilder();
    var bcf = builder
      .AddMarkup(m => m
        .SetGuid("3ffb4df2-0187-49a9-8a4a-23992696bafd")
        .SetTitle("This is a new topic")
        .SetCreationDate(new DateTime())
        .SetCreationAuthor("Creator"))
      .Build();

    await _worker.ToBcf(bcf, "Resources/output/bcf/v2.1/BcfZipTest21.bcfzip");
  }

  [Test]
  [Category("BCF v3.0")]
  public async Task ToBcfZipV30Tests() {
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

    await _worker.ToBcf(bcf, "Resources/output/bcf/v3.0/BcfZipTest30.bcfzip");
  }

  [Test]
  [Category("BCF v2.1")]
  public async Task ToJsonV21Tests() {
    var builder = new BcfBuilder();
    var bcf = builder
      .AddMarkup(m => m
        .SetGuid("3ffb4df2-0187-49a9-8a4a-23992696bafd")
        .SetTitle("This is a new topic")
        .SetCreationDate(new DateTime())
        .SetCreationAuthor("Creator"))
      .Build();

    await _worker.ToJson(bcf, "Resources/output/json/v2.1/JsonTest21");
  }

  [Test]
  [Category("BCF v3.0")]
  public async Task ToJsonV30Tests() {
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

    await _worker.ToJson(bcf, "Resources/output/json/v3.0/JsonTest30");
  }
}