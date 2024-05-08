using BcfToolkit.Builder.Bcf30;
using NUnit.Framework;

namespace Tests.Builder;

public class ExtensionsBuilderTests {
  private readonly ExtensionsBuilder _builder = new();

  [Test]
  public void BuildExtensionsWithComplexFields() {
    var extensions = (BcfToolkit.Model.Bcf30.Extensions)_builder
      .AddTopicType("ERROR")
      .AddTopicStatus("OPEN")
      .AddPriority("HIGH")
      .Build();
    Assert.AreEqual(true, extensions.TopicTypesSpecified);
    Assert.AreEqual(true, extensions.TopicStatusesSpecified);
    Assert.AreEqual(true, extensions.PrioritiesSpecified);
  }
}