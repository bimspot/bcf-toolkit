using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BcfToolkit.Builder.Bcf30;
using NUnit.Framework;

namespace Tests.Builder;

public class BcfBuilderTests {
  private BcfBuilder _builder = null!;

  [SetUp]
  public void Setup() {
    _builder = new BcfBuilder();
  }

  [Test]
  public void BuildBcfWithComplexFields() {
    var bcf = _builder
      .AddMarkup(m => m
        .SetTitle("Title")
        .SetGuid("3ffb4df2-0187-49a9-8a4a-23992696bafd")
        .SetCreationAuthor("Creator")
        .SetTopicType("Issue")
        .SetTopicStatus("Open"))
      .SetExtensions(e => e
        .AddTopicType("ERROR")
        .AddTopicStatus("OPEN")
        .AddPriority("HIGH"))
      .SetProjectInfo(p => p
        .SetProjectId("3ZSh2muKX7S8MCESk95seC")
        .SetProjectName("Project"))
      .Build();
    Assert.That(1, Is.EqualTo(bcf.Markups.Count));
    Assert.That(true, Is.EqualTo(bcf.Extensions.TopicTypesSpecified));
    Assert.That("Project", Is.EqualTo(bcf.Project?.Project.Name));
  }

  [Test]
  public void BuildBcfWithMissingRequiredFields() {
    Assert.That(() => _builder.Build(), Throws.ArgumentException);
  }

  [Test]
  public async Task BuildBcfFromStream() {
    await using var stream = new FileStream(
      "Resources/Bcf/v3.0/UserAssignment.bcfzip",
      FileMode.Open,
      FileAccess.Read);
    var bcf = await _builder.BuildFromStream(stream);
    Assert.That(1, Is.EqualTo(bcf.Markups.Count));
    Assert.That(
      "Architect@example.com",
      Is.EqualTo(bcf.Markups.FirstOrDefault()?.Topic.AssignedTo));
  }
  [Test]
  public async Task BuildEmptyBcfFromStream() {
    await using var stream = new FileStream(
      "Resources/Bcf/v3.0/Empty.bcfzip",
      FileMode.Open,
      FileAccess.Read);
    Assert.That(() => _builder.BuildFromStream(stream), Throws.ArgumentException);
  }
}