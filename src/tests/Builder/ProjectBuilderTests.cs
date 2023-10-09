using BcfConverter.Builder.Bcf30;
using BcfConverter.Model.Bcf30;
using NUnit.Framework;

namespace Tests.Builder;

public class ProjectBuilderTests {
  private ProjectBuilder _builder;

  [SetUp]
  public void Setup() {
    _builder = new ProjectBuilder();
  }

  [Test]
  public void BuildProject() {
    var project = (ProjectInfo)_builder
      .AddProjectId("3ZSh2muKX7S8MCESk95seC")
      .AddProjectName("Project")
      .Build();

    Assert.AreEqual(
      "3ZSh2muKX7S8MCESk95seC",
      project.Project.ProjectId);
  }

  [Test]
  public void BuildProjectWithoutRequiredFields() {
    _builder
      .AddProjectName("Project");
    Assert.That(() => _builder.Build(), Throws.ArgumentException);
  }

  [Test]
  public void BuildProjectWithEmptyId() {
    _builder
      .AddProjectId("")
      .AddProjectName("Project");
    Assert.That(() => _builder.Build(), Throws.ArgumentException);
  }
}