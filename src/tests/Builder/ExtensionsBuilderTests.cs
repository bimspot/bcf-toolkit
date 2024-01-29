using BcfToolkit.Builder.Bcf30;
using BcfToolkit.Model.Bcf30;
using NUnit.Framework;

namespace Tests.Builder;

public class ExtensionsBuilderTests {
  private ExtensionsBuilder _builder;

  [SetUp]
  public void Setup() {
    _builder = new ExtensionsBuilder();
  }

  [Test]
  public void BuildExtensionsWithComplexFields() {
    var extensions = (Extensions)_builder
      .AddTopicType("ERROR")
      .AddTopicStatus("OPEN")
      .AddPriority("HIGH")
      .Build();
    Assert.AreEqual(true, extensions.TopicTypesSpecified);
    Assert.AreEqual(true, extensions.TopicStatusesSpecified);
    Assert.AreEqual(true, extensions.PrioritiesSpecified);
  }
}