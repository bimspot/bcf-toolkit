using BcfToolkit.Builder.Bcf30;
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
    // TODO add more complex
    var extensions = _builder
      // .AddTopicType("ERROR")
      // .AddTopicStatus("OPEN")
      // .AddPriority("HIGH")
      .Build();
  }

  // [Test]
  // public void BuildMarkupWithRequiredFields() {
  //
  //   var markup = _builder
  //     .SetTitle("Title")
  //     .SetGuid("3ffb4df2-0187-49a9-8a4a-23992696bafd")
  //     .SetCreationAuthor("Meszaros")
  //     .SetTopicType("Issue")
  //     .SetTopicStatus("Open")
  //     .Build();
  //   Assert.AreEqual(
  //     "3ffb4df2-0187-49a9-8a4a-23992696bafd",
  //     markup.GetTopic()!.Guid);
  // }
  //
  // [Test]
  // public void BuildMarkupWithoutRequiredFields() {
  //   _builder
  //     .SetTitle("Title")
  //     .SetGuid("3ffb4df2-0187-49a9-8a4a-23992696bafd")
  //     .SetTopicStatus("Open");
  //   Assert.That(() => _builder.Build(), Throws.ArgumentException);
  // }
  //
  // [Test]
  // public void BuildMarkupWithNonConformId() {
  //   _builder
  //     .SetTitle("Title")
  //     .SetGuid("guid")
  //     .SetCreationAuthor("Meszaros")
  //     .SetTopicType("Issue")
  //     .SetTopicStatus("Open");
  //   Assert.That(() => _builder.Build(), Throws.ArgumentException);
  // }
}