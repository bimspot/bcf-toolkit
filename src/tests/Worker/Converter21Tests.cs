using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BcfToolkit.Model.Bcf21;
using BcfToolkit.Worker;
using NUnit.Framework;

namespace tests.Worker;

[TestFixture]
public class Converter21Tests {
  [SetUp]
  public void Setup() {
    _converterWorker = new BcfToolkit.Worker.Bcf21.ConverterWorker();
  }

  private IConverterWorker _converterWorker = null!;

  [Test]
  public void BcfToJsonSampleFilesTest() {
    _converterWorker.BcfZipToJson("Resources/Bcf/v2.1/AllPartsVisible.bcfzip",
      "Resources/output/json/v2.1/AllPartsVisible");
    _converterWorker.BcfZipToJson(
      "Resources/Bcf/v2.1/ComponentSelection.bcfzip",
      "Resources/output/json/v2.1/ComponentSelection");
    _converterWorker.BcfZipToJson(
      "Resources/Bcf/v2.1/ExternalBIMSnippet.bcfzip",
      "Resources/output/json/v2.1/ExternalBIMSnippet");
    _converterWorker.BcfZipToJson(
      "Resources/Bcf/v2.1/MaximumInformation.bcfzip",
      "Resources/output/json/v2.1/MaximumInformation");
    _converterWorker.BcfZipToJson(
      "Resources/Bcf/v2.1/UserAssignment.bcfzip",
      "Resources/output/json/v2.1/UserAssignment");
  }

  [Test]
  public Task JsonToBcfSampleFilesTest() {
    var tasks = new List<Task> {
      _converterWorker.JsonToBcfZip(
        "Resources/Json/v2.1/AllPartsVisible",
        "Resources/output/bcf/v2.1/AllPartsVisible.bcfzip")
    };

    return Task.WhenAll(tasks);
  }

  [Test]
  public async Task BcfStream_ShouldReturnFileStream() {
    var markup = new Markup {
      Topic = new Topic {
        Guid = "3ffb4df2-0187-49a9-8a4a-23992696bafd",
        Title = "This is a new topic",
        CreationDate = new DateTime(),
        CreationAuthor = "Meszaros"
      }
    };
    var markups = new ConcurrentBag<Markup> { markup };
    var bcf = new BcfToolkit.Model.Bcf21.Bcf {
      Markups = markups
    };

    var stream = await _converterWorker.BcfStream(bcf);

    Assert.IsNotNull(stream);
    Assert.IsTrue(stream.CanRead);

    await stream.DisposeAsync();
  }

  [Test]
  public void BcfToJsonMissingRequiredPropertyTest() {
    Assert.That(async () => await _converterWorker.BcfZipToJson(
      "Resources/Bcf/v2.1/RelatedTopics.bcfzip",
       "Resources/output/json/v2.1/RelatedTopics"), Throws.ArgumentException);
  }

  [Test]
  public void BcfToJsonWrongPathTest() {
    Assert.That(async () => await _converterWorker.BcfZipToJson(
      "Resources/Bcf/v3.0/Meszaros.bcfzip",
      "Resources/output/json/v2.1/Meszaros"), Throws.ArgumentException);
  }

  /// <summary>
  ///   It should generate the bcfzip with the minimum information.
  /// </summary>
  [Test]
  [Category("BCF v2.1")]
  public Task WriteBcfWithMinimumInformationTest() {
    var markup = new Markup {
      Topic = new Topic {
        Guid = "3ffb4df2-0187-49a9-8a4a-23992696bafd",
        Title = "This is a new topic",
        CreationDate = new DateTime(),
        CreationAuthor = "Meszaros"
      }
    };
    var markups = new ConcurrentBag<Markup> { markup };
    var bcf = new BcfToolkit.Model.Bcf21.Bcf {
      Markups = markups
    };
    return _converterWorker.ToBcfZip("Resources/output/Bcf/v2.1/MinimumInformation.bcfzip", bcf);
  }

  /// <summary>
  ///   It should generate a bcf skipping the markup file.
  /// </summary>
  [Test]
  [Category("BCF v2.1")]
  public Task WriteBcfWithoutTopicGuidTest() {
    var markup = new Markup {
      Topic = new Topic {
        Title = "This is a new topic",
        CreationDate = new DateTime(),
        CreationAuthor = "Meszaros"
      }
    };
    var markups = new ConcurrentBag<Markup> { markup };
    var bcf = new BcfToolkit.Model.Bcf21.Bcf {
      Markups = markups
    };
    return _converterWorker.ToBcfZip("Resources/output/Bcf/v2.1/WithoutTopicGuid.bcfzip", bcf);
  }
}