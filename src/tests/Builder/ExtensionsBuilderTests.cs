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
    Assert.That(true, Is.EqualTo(extensions.TopicTypesSpecified));
    Assert.That(true, Is.EqualTo(extensions.TopicStatusesSpecified));
    Assert.That(true, Is.EqualTo(extensions.PrioritiesSpecified));
  }
}