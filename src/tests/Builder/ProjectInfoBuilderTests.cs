using BcfToolkit.Builder.Bcf30;
using BcfToolkit.Model.Bcf30;
using NUnit.Framework;

namespace Tests.Builder;

public class ProjectInfoBuilderTests {
  private readonly ProjectInfoBuilder _infoBuilder = new();

  [Test]
  public void BuildProject() {
    var project = (ProjectInfo)_infoBuilder
      .SetProjectId("3ZSh2muKX7S8MCESk95seC")
      .SetProjectName("Project")
      .Build();

    Assert.AreEqual(
      "3ZSh2muKX7S8MCESk95seC",
      project.Project.ProjectId);
  }

  [Test]
  public void BuildProjectWithoutRequiredFields() {
    _infoBuilder
      .SetProjectName("Project");
    Assert.That(() => _infoBuilder.Build(), Throws.ArgumentException);
  }

  [Test]
  public void BuildProjectWithEmptyId() {
    _infoBuilder
      .SetProjectId("")
      .SetProjectName("Project");
    Assert.That(() => _infoBuilder.Build(), Throws.ArgumentException);
  }
}