using System;
using BcfToolkit.Builder.Bcf30;
using NUnit.Framework;

namespace Tests.Builder;

public class FileBuilderTest {
  private FileBuilder _builder = null!;

  [SetUp]
  public void Setup() {
    _builder = new FileBuilder();
  }

  [Test]
  public void BuildHeaderFileWithComplexFields() {
    var file = _builder
      .SetIfcProject("3MD_HkJ6X2EwpfIbCFm0g_")
      .SetFileName("File.ifc")
      .SetIsExternal(false)
      .SetIfcSpatialStructureElement("3MD_HkJ6X2EwpfIbCFm0hh")
      .SetDate(DateTime.Parse("2012-10-15T16:41:27+04:00").ToUniversalTime())
      .Build();
    Assert.That(
      "3MD_HkJ6X2EwpfIbCFm0g_",
      Is.EqualTo(file.IfcProject));
    Assert.That(
      DateTime.Parse("2012-10-15T12:41:27"),
      Is.EqualTo(file.Date));
  }
}