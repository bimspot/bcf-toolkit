using BcfToolkit.Builder;
using BcfToolkit.Model.Bcf30;
using NUnit.Framework;

namespace Tests.Builder;

public class BuilderCreatorTests {

  [Test]
  public void BuildMarkup() {
    var builder = BcfBuilder.Markup();
    var markup = (Markup)builder
      .SetTitle("Title")
      .SetGuid("3ffb4df2-0187-49a9-8a4a-23992696bafd")
      .SetCreationAuthor("Meszaros")
      .SetTopicType("Issue")
      .SetTopicStatus("Open")
      .Build();
    Assert.AreEqual(
      "3ffb4df2-0187-49a9-8a4a-23992696bafd",
      markup.Topic.Guid);
  }

  [Test]
  public void BuildProject() {
    var builder = BcfBuilder.Project();
    var project = (ProjectInfo)builder
      .SetProjectId("3ZSh2muKX7S8MCESk95seC")
      .SetProjectName("Project")
      .Build();

    Assert.AreEqual(
      "3ZSh2muKX7S8MCESk95seC",
      project.Project.ProjectId);
  }

  [Test]
  public void BuildExtensions() {
    var builder = BcfBuilder.Extensions();
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
    var builder = BcfBuilder.Document();
    var document = (DocumentInfo)builder
      .AddDocument(doc => doc
        .SetFileName("File")
        .SetGuid("3ffb4df2-0187-49a9-8a4a-23992696bafd"))
      .Build();

    Assert.AreEqual(
      true,
      document.DocumentsSpecified);
  }
}