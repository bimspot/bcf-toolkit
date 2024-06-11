using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BcfToolkit.Converter;
using BcfToolkit.Model;
using BcfToolkit.Utils;
using BcfToolkit.Model.Bcf30;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Tests.Converter.Bcf30;

[TestFixture]
public class ConverterTests {
  [SetUp]
  public void Setup() {
    _converter = new BcfToolkit.Converter.Bcf30.Converter();
  }

  private IConverter _converter = null!;

  [Test]
  [Category("BCF v3.0")]
  public void BcfToJsonSampleFilesTest() {
    _converter.BcfZipToJson(
      "Resources/Bcf/v3.0/ComponentSelection.bcfzip",
      "Resources/output/json/v3.0/ComponentSelection");
    _converter.BcfZipToJson(
      "Resources/Bcf/v3.0/DocumentReferenceExternal.bcfzip",
      "Resources/output/json/v3.0/DocumentReferenceExternal");
    _converter.BcfZipToJson(
      "Resources/Bcf/v3.0/DocumentReferenceInternal.bcfzip",
      "Resources/output/json/v3.0/DocumentReferenceInternal");
    _converter.BcfZipToJson(
      "Resources/Bcf/v3.0/DueDate.bcfzip",
      "Resources/output/json/v3.0/DueDate");
    _converter.BcfZipToJson(
      "Resources/Bcf/v3.0/Labels.bcfzip",
      "Resources/output/json/v3.0/Labels");
    _converter.BcfZipToJson(
      "Resources/Bcf/v3.0/Milestone.bcfzip",
      "Resources/output/json/v3.0/Milestone");
    _converter.BcfZipToJson(
      "Resources/Bcf/v3.0/RelatedTopics.bcfzip",
      "Resources/output/json/v3.0/RelatedTopics");
    _converter.BcfZipToJson(
      "Resources/Bcf/v3.0/SingleInvisibleWall.bcfzip",
      "Resources/output/json/v3.0/SingleInvisibleWall");
    _converter.BcfZipToJson(
      "Resources/Bcf/v3.0/TestBcf30.bcfzip",
      "Resources/output/json/v3.0/TestBcf30");
    _converter.BcfZipToJson(
      "Resources/Bcf/v3.0/TopicsWithDifferentModelsVisible.bcfzip",
      "Resources/output/json/v3.0/TopicsWithDifferentModelsVisible");
    _converter.BcfZipToJson(
      "Resources/Bcf/v3.0/UserAssignment.bcfzip",
      "Resources/output/json/v3.0/UserAssignment");
  }

  [Test]
  [Category("BCF v3.0")]
  public Task JsonToBcfSampleFilesTest() {
    var tasks = new List<Task> {
      _converter.JsonToBcfZip(
        "Resources/Json/v3.0/DocumentReferenceInternal",
        "Resources/output/Bcf/v3.0/DocumentReferenceInternal.bcfzip"),
    };

    return Task.WhenAll(tasks);
  }

  [Test]
  [Category("BCF v3.0")]
  public async Task ShouldReturnBcfFileStreamTest() {
    await using var stream = new FileStream("Resources/Bcf/v3.0/DocumentReferenceExternal.bcfzip", FileMode.Open, FileAccess.Read);

    var extensions =
      await BcfExtensions.ParseExtensions<Extensions>(stream);
    var projectInfo = await BcfExtensions.ParseProject<ProjectInfo>(stream);
    var documentInfo = await BcfExtensions.ParseDocuments<DocumentInfo>(stream);

    var markups =
      await BcfExtensions.ParseMarkups<Markup, VisualizationInfo>(stream);

    var bcf = new Bcf {
      Markups = markups,
      Extensions = extensions,
      Project = projectInfo,
      Document = documentInfo
    };

    var streamResult = await _converter.ToBcfStream(bcf, BcfVersionEnum.Bcf30, false);

    ClassicAssert.IsNotNull(streamResult);
    ClassicAssert.IsTrue(streamResult.CanRead);

    await streamResult.DisposeAsync();
  }

  [Test]
  [Category("BCF v3.0")]
  public void BcfToJsonBackwardCompatibilityTest() {
    // 2.1 -> 3.0 is not backward compatible
    Assert.That(async () => await _converter.BcfZipToJson(
      "Resources/Bcf/v2.1/AllPartsVisible.bcfzip",
      "Resources/output/Json/v3.0/AllPartsVisible"), Throws.Exception);
  }

  [Test]
  [Category("BCF v3.0")]
  public void BcfToJsonWrongPathTest() {
    Assert.That(async () => await _converter.BcfZipToJson(
      "Resources/Bcf/v3.0/Wrong.bcfzip",
      "Resources/output/json/v3.0/Wrong"), Throws.Exception);
  }

  [Test]
  [Category("BCF v3.0")]
  public void JsonToBcfMissingBcfRootTest() {
    Assert.That(async () => await _converter.BcfZipToJson(
      "Resources/json/v3.0/MissingBcfRoot.bcfzip",
      "Resources/output/Bcf/v3.0/MissingBcfRoot"), Throws.Exception);
  }

  /// <summary>
  ///   It should generate the bcfzip with the minimum information.
  /// </summary>
  [Test]
  [Category("BCF v3.0")]
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
    return _converter.ToBcfZip(bcf, "Resources/output/Bcf/v3.0/MinimumInformation.bcfzip");
  }

  /// <summary>
  ///   It should generate the json with the minimum information.
  /// </summary>
  [Test]
  [Category("BCF v3.0")]
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
    return _converter.ToJson(bcf, "Resources/output/json/v3.0/MinimumInformation");
  }

  /// <summary>
  ///   It should generate a bcf skipping the markup file.
  /// </summary>
  [Test]
  [Category("BCF v3.0")]
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
    return _converter.ToBcfZip(bcf, "Resources/output/Bcf/v3.0/WithoutTopicGuid.bcfzip");
  }

  /// <summary>
  ///   It should generate a bcf v2.1 object from stream.
  /// </summary>
  [Test]
  [Category("BCF v3.0")]
  public async Task BuildSimpleV30BcfFromStreamTest() {
    await using var stream =
      new FileStream(
        "Resources/Bcf/v3.0/ComponentSelection.bcfzip",
        FileMode.Open,
        FileAccess.Read);
    var bcf = await _converter.BuildBcfFromStream<Bcf>(stream);
    Assert.That(typeof(Bcf), Is.EqualTo(bcf.GetType()));
    Assert.That(1, Is.EqualTo(bcf.Markups.Count));
    Assert.That("3.0", Is.EqualTo(bcf.Version?.VersionId));
  }

  // /// <summary>
  // ///   It should generate a bcf v2.1 object downgraded from bcf v3.0.
  // /// </summary>
  // [Test]
  // [Category("BCF v3.0")]
  // public async Task BuildSimpleV21BcfFromStreamTest() {
  //   await using var stream =
  //     new FileStream(
  //       "Resources/Bcf/v3.0/ComponentSelection.bcfzip",
  //       FileMode.Open,
  //       FileAccess.Read);
  //   var bcf = await _converter.BuildBcfFromStream<BcfToolkit.Model.Bcf21.Bcf>(stream);
  //   Assert.AreEqual(typeof(BcfToolkit.Model.Bcf21.Bcf),bcf.GetType());
  //   Assert.AreEqual(1, bcf.Markups.Count);
  //   Assert.AreEqual("OPEN", bcf.Extensions.TopicStatuses.FirstOrDefault());
  //   Assert.AreEqual("2.1", bcf.Version?.VersionId);
  // }
}