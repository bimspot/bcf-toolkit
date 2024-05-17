using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BcfToolkit.Converter;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf21;
using NUnit.Framework;

namespace Tests.Converter.Bcf21;

[TestFixture]
public class ConverterTests {
  [SetUp]
  public void Setup() {
    _converter = new BcfToolkit.Converter.Bcf21.Converter();
  }

  private IConverter _converter = null!;

  [Test]
  public void BcfToJsonSampleFilesTest() {
    _converter.BcfZipToJson("Resources/Bcf/v2.1/AllPartsVisible.bcfzip",
      "Resources/output/json/v2.1/AllPartsVisible");
    _converter.BcfZipToJson(
      "Resources/Bcf/v2.1/ComponentSelection.bcfzip",
      "Resources/output/json/v2.1/ComponentSelection");
    _converter.BcfZipToJson(
      "Resources/Bcf/v2.1/ExternalBIMSnippet.bcfzip",
      "Resources/output/json/v2.1/ExternalBIMSnippet");
    _converter.BcfZipToJson(
      "Resources/Bcf/v2.1/MaximumInformation.bcfzip",
      "Resources/output/json/v2.1/MaximumInformation");
    _converter.BcfZipToJson(
      "Resources/Bcf/v2.1/UserAssignment.bcfzip",
      "Resources/output/json/v2.1/UserAssignment");
  }

  [Test]
  public Task JsonToBcfSampleFilesTest() {
    var tasks = new List<Task> {
      _converter.JsonToBcfZip(
        "Resources/Json/v2.1/AllPartsVisible",
        "Resources/output/bcf/v2.1/AllPartsVisible.bcfzip")
    };

    return Task.WhenAll(tasks);
  }

  [Test]
  public async Task ShouldReturnBcfFileStreamTest() {
    var markup = new Markup {
      Topic = new Topic {
        Guid = "3ffb4df2-0187-49a9-8a4a-23992696bafd",
        Title = "This is a new topic",
        CreationDate = new DateTime(),
        CreationAuthor = "Creator"
      }
    };
    var markups = new ConcurrentBag<Markup> { markup };
    var bcf = new Bcf {
      Markups = markups
    };

    var stream = await _converter.ToBcfStream(bcf, BcfVersionEnum.Bcf21);

    Assert.IsNotNull(stream);
    Assert.IsTrue(stream.CanRead);

    await stream.DisposeAsync();
  }

  [Test]
  public void BcfToJsonMissingRequiredPropertyTest() {
    Assert.That(async () => await _converter.BcfZipToJson(
      "Resources/Bcf/v2.1/RelatedTopics.bcfzip",
      "Resources/output/json/v2.1/RelatedTopics"), Throws.ArgumentException);
  }

  [Test]
  public void BcfToJsonWrongPathTest() {
    Assert.That(async () => await _converter.BcfZipToJson(
      "Resources/Bcf/v3.0/Wrong.bcfzip",
      "Resources/output/json/v2.1/Wrong"), Throws.ArgumentException);
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
        CreationAuthor = "Creator"
      }
    };
    var markups = new ConcurrentBag<Markup> { markup };
    var bcf = new Bcf {
      Markups = markups
    };
    return _converter.ToBcfZip(bcf, "Resources/output/Bcf/v2.1/MinimumInformation.bcfzip");
  }

  /// <summary>
  ///   It should generate the json with the minimum information.
  /// </summary>
  [Test]
  [Category("BCF v2.1")]
  public Task WriteJsonWithMinimumInformationTest() {
    var markup = new Markup {
      Topic = new Topic {
        Guid = "3ffb4df2-0187-49a9-8a4a-23992696bafd",
        Title = "This is a new topic",
        CreationDate = new DateTime(),
        CreationAuthor = "Creator"
      }
    };
    var markups = new ConcurrentBag<Markup> { markup };
    var bcf = new Bcf {
      Markups = markups
    };
    return _converter.ToJson(bcf, "Resources/output/json/v2.1/MinimumInformation");
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
        CreationAuthor = "Creator"
      }
    };
    var markups = new ConcurrentBag<Markup> { markup };
    var bcf = new Bcf {
      Markups = markups
    };
    return _converter.ToBcfZip(bcf, "Resources/output/Bcf/v2.1/WithoutTopicGuid.bcfzip");
  }

  /// <summary>
  ///   It should generate a bcf v2.1 object from stream.
  /// </summary>
  [Test]
  [Category("BCF v2.1")]
  public async Task BuildSimpleV21BcfFromStreamTest() {
    await using var stream =
      new FileStream(
        "Resources/Bcf/v2.1/MinimumInformation.bcfzip",
        FileMode.Open,
        FileAccess.Read);
    var bcf = await _converter.BuildBcfFromStream<Bcf>(stream);
    Assert.AreEqual(typeof(Bcf), bcf.GetType());
    Assert.AreEqual(1, bcf.Markups.Count);
    Assert.AreEqual("2.1", bcf.Version?.VersionId);
  }

  /// <summary>
  ///   It should generate a bcf v3.0 object converted from bcf v2.1.
  /// </summary>
  [Test]
  [Category("BCF v2.1")]
  public async Task BuildSimpleV30BcfFromStreamTest() {
    await using var stream =
      new FileStream(
        "Resources/Bcf/v2.1/MinimumInformation.bcfzip",
        FileMode.Open,
        FileAccess.Read);
    var bcf = await _converter.BuildBcfFromStream<BcfToolkit.Model.Bcf30.Bcf>(stream);
    Assert.AreEqual(typeof(BcfToolkit.Model.Bcf30.Bcf), bcf.GetType());
    Assert.AreEqual(1, bcf.Markups.Count);
    Assert.AreEqual("OPEN", bcf.Extensions.TopicStatuses.FirstOrDefault());
    Assert.AreEqual("3.0", bcf.Version?.VersionId);
  }
}