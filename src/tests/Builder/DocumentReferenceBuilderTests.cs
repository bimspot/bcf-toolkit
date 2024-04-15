using BcfToolkit.Builder.Bcf30;
using NUnit.Framework;

namespace Tests.Builder;

public class DocumentReferenceBuilderTests {
  private DocumentReferenceBuilder _builder = null!;

  [SetUp]
  public void Setup() {
    _builder = new DocumentReferenceBuilder();
  }

  [Test]
  public void BuildDocumentReferenceWithComplexFields() {
    var document = _builder
      .SetDescription("this is a document")
      .SetUrl("http://www.example.com")
      .SetDocumentGuid("3ffb4df2-0187-49a9-8a4a-23992696bafd")
      .SetGuid("3ffb4df2-0187-49a9-8a4a-23992696bafd")
      .Build();
  }

  [Test]
  public void BuildDocumentReferenceWithoutRequiredFields() {
    Assert.That(() => _builder.Build(), Throws.ArgumentException);
  }
}