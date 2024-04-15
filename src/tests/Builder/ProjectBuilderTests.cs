using BcfToolkit.Builder.Bcf30;
using BcfToolkit.Model.Bcf30;
using NUnit.Framework;

namespace Tests.Builder;

public class ProjectBuilderTests {
  private readonly ProjectBuilder _builder = new();

  [Test]
  public void BuildProject() {
    var project = (ProjectInfo)_builder
      .SetProjectId("3ZSh2muKX7S8MCESk95seC")
      .SetProjectName("Project")
      .Build();

    Assert.AreEqual(
      "3ZSh2muKX7S8MCESk95seC",
      project.Project.ProjectId);
  }

  [Test]
  public void BuildProjectWithoutRequiredFields() {
    _builder
      .SetProjectName("Project");
    Assert.That(() => _builder.Build(), Throws.ArgumentException);
  }

  [Test]
  public void BuildProjectWithEmptyId() {
    _builder
      .SetProjectId("")
      .SetProjectName("Project");
    Assert.That(() => _builder.Build(), Throws.ArgumentException);
  }
}