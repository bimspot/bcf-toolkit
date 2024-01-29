using BcfToolkit.Builder.Bcf30;
using NUnit.Framework;

namespace Tests.Builder;

public class MarkupBuilderTests {
  private MarkupBuilder _builder;

  [SetUp]
  public void Setup() {
    _builder = new MarkupBuilder();
  }

  [Test]
  public void BuildMarkupWithComplexFields() {
    // TODO add more complex
    var markup = _builder
      .SetTitle("Title")
      .SetGuid("3ffb4df2-0187-49a9-8a4a-23992696bafd")
      .SetCreationAuthor("Creator")
      .SetTopicType("Issue")
      .SetTopicStatus("Open")
      .Build();
    Assert.AreEqual(
      "3ffb4df2-0187-49a9-8a4a-23992696bafd",
      markup.GetTopic()!.Guid);
  }

  [Test]
  public void BuildMarkupWithRequiredFields() {

    var markup = _builder
      .SetTitle("Title")
      .SetGuid("3ffb4df2-0187-49a9-8a4a-23992696bafd")
      .SetCreationAuthor("Creator")
      .SetTopicType("Issue")
      .SetTopicStatus("Open")
      .Build();
    Assert.AreEqual(
      "3ffb4df2-0187-49a9-8a4a-23992696bafd",
      markup.GetTopic()!.Guid);
  }

  [Test]
  public void BuildMarkupWithoutRequiredFields() {
    _builder
      .SetTitle("Title")
      .SetGuid("3ffb4df2-0187-49a9-8a4a-23992696bafd")
      .SetTopicStatus("Open");
    Assert.That(() => _builder.Build(), Throws.ArgumentException);
  }

  [Test]
  public void BuildMarkupWithNonConformId() {
    _builder
      .SetTitle("Title")
      .SetGuid("guid")
      .SetCreationAuthor("Creator")
      .SetTopicType("Issue")
      .SetTopicStatus("Open");
    Assert.That(() => _builder.Build(), Throws.ArgumentException);
  }
}