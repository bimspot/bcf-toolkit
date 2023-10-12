using BcfToolkit.Builder.Bcf30;
using NUnit.Framework;

namespace Tests.Builder; 

public class DocumentReferenceBuilderTests {
  private DocumentReferenceBuilder _builder;

  [SetUp]
  public void Setup() {
    _builder = new DocumentReferenceBuilder();
  }

  // [Test]
  // public void BuildDocumentReferenceWithComplexFields() {
  //   // TODO add more complex
  //   var markup = _builder
  //     .SetGuid("asdf")
  //     .Build();
  // }
}