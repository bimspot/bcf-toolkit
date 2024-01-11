using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BcfToolkit.Converter;
using BcfToolkit.Model.Bcf21;
using NUnit.Framework;

namespace Tests.Converter;

[TestFixture]
public class Converter21Tests {
  [SetUp]
  public void Setup() {
    _converter = new BcfToolkit.Converter.Bcf21.Converter();
  }

  private IConverter _converter = null!;

  [Test]
  public void BcfToJsonSampleFilesTest() {
    _converter.BcfToJson("Resources/Bcf/v2.1/AllPartsVisible.bcfzip",
      "Resources/output/json/v2.1/AllPartsVisible");
    _converter.BcfToJson(
      "Resources/Bcf/v2.1/ComponentSelection.bcfzip",
      "Resources/output/json/v2.1/ComponentSelection");
    _converter.BcfToJson(
      "Resources/Bcf/v2.1/ExternalBIMSnippet.bcfzip",
      "Resources/output/json/v2.1/ExternalBIMSnippet");
    _converter.BcfToJson(
      "Resources/Bcf/v2.1/MaximumInformation.bcfzip",
      "Resources/output/json/v2.1/MaximumInformation");
    _converter.BcfToJson(
      "Resources/Bcf/v2.1/UserAssignment.bcfzip",
      "Resources/output/json/v2.1/UserAssignment");
  }

  [Test]
  public Task JsonToBcfSampleFilesTest() {
    var tasks = new List<Task> {
      _converter.JsonToBcf(
        "Resources/Json/v2.1/AllPartsVisible",
        "Resources/output/bcf/v2.1/AllPartsVisible.bcfzip")
    };

    return Task.WhenAll(tasks);
  }

  [Test]
  public void BcfToJsonMissingRequiredPropertyTest() {
    Assert.That(async () => await _converter.BcfToJson(
      "Resources/Bcf/v2.1/RelatedTopics.bcfzip",
       "Resources/output/json/v2.1/RelatedTopics"), Throws.ArgumentException);
  }

  [Test]
  public void BcfToJsonWrongPathTest() {
    Assert.That(async () => await _converter.BcfToJson(
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
    return _converter.ToBcf("Resources/output/Bcf/v2.1/MinimumInformation.bcfzip", bcf);
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
    return _converter.ToBcf("Resources/output/Bcf/v2.1/WithoutTopicGuid.bcfzip", bcf);
  }
}