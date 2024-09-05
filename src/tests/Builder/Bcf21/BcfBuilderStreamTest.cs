using System;
using System.IO;
using System.Threading.Tasks;
using BcfToolkit.Builder.Bcf21;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model.Bcf21;
using BcfToolkit.Model.Interfaces;
using NUnit.Framework;

namespace Tests.Builder.Bcf21;

public class BcfBuilderDelegate : IBcfBuilderDelegate {
  public IBcfBuilderDelegate.OnMarkupCreated<IMarkup>
    MarkupCreated { get; } = m => {
      var markup = (Markup)m;
      Console.WriteLine(markup.Topic.Guid);
    };

  public IBcfBuilderDelegate.OnExtensionsCreated<IExtensions>
    ExtensionsCreated { get; } = Console.WriteLine;

  public IBcfBuilderDelegate.OnProjectCreated<IProject>
    ProjectCreated { get; } = Console.WriteLine;

  public IBcfBuilderDelegate.OnDocumentCreated<IDocumentInfo>
    DocumentCreatedCreated { get; } = Console.WriteLine;
}

public class BcfBuilderStreamTest {
  private BcfBuilder _streamBuilder = null!;

  [SetUp]
  public void Setup() {
    var bcfBuilderDelegate = new BcfBuilderDelegate();
    _streamBuilder = new BcfBuilder();
    _streamBuilder.SetDelegate(bcfBuilderDelegate);
  }

  [Test]
  public async Task ProcessBcfStreamTest() {
    await using var stream = new FileStream(
      "Resources/Bcf/v2.1/MaximumInformation.bcfzip",
      FileMode.Open,
      FileAccess.Read);

    await _streamBuilder.ProcessStream(stream);
  }
}