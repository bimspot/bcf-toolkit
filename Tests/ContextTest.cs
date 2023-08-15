using bcf;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class ContextTests {
  [Test]
  public void WrongBcfVersionTest() {
    Assert.That(() => new Context("1.0"), Throws.ArgumentException);
  }
}