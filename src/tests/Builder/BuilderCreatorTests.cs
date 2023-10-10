using BcfToolkit.Builder;
using BcfToolkit.Model.Bcf30;
using NUnit.Framework;

namespace Tests.Builder;

public class BuilderCreatorTests {

  [Test]
  public void BuildMarkup() {
    var builder = BuilderCreator.CreateMarkupBuilder();
    var markup = (Markup)builder
      .AddTitle("Title")
      .AddGuid("3ffb4df2-0187-49a9-8a4a-23992696bafd")
      .AddCreationAuthor("Meszaros")
      .AddTopicType("Issue")
      .AddTopicStatus("Open")
      .Build();
    Assert.AreEqual(
      "3ffb4df2-0187-49a9-8a4a-23992696bafd",
      markup.Topic.Guid);
  }

  [Test]
  public void BuildProject() {
    var builder = BuilderCreator.CreateProjectBuilder();
    var project = (ProjectInfo)builder
      .AddProjectId("3ZSh2muKX7S8MCESk95seC")
      .AddProjectName("Project")
      .Build();

    Assert.AreEqual(
      "3ZSh2muKX7S8MCESk95seC",
      project.Project.ProjectId);
  }

  [Test]
  public void BuildExtensions() {
    var builder = BuilderCreator.CreateExtensionsBuilder();
    var extensions = (Extensions)builder
      .AddTopicType("Fault")
      .AddTopicType("Clash")
      .AddTopicType("Remark")
      .Build();

    Assert.AreEqual(
      true,
      extensions.TopicTypesSpecified);
  }

  [Test]
  public void BuildDocument() {
    var builder = BuilderCreator.CreateDocumentBuilder();
    var document = (DocumentInfo)builder
      .AddDocument(doc => doc
        .AddFileName("File")
        .AddGuid("3ffb4df2-0187-49a9-8a4a-23992696bafd"))
      .Build();

    Assert.AreEqual(
      true,
      document.DocumentsSpecified);
  }
}