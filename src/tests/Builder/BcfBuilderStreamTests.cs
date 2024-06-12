using System;
using System.IO;
using System.Threading.Tasks;
using BcfToolkit;
using NUnit.Framework;
using BcfToolkit.Builder.Bcf30;
using Moq;

namespace Tests.Builder;

using BcfToolkit.Builder.Bcf30.Interfaces;
using BcfToolkit.Model.Bcf30;

public class BcfBuilderStreamTests {
  private BcfBuilder _streamBuilder = null!;
  
  // private class BcfBuilderDelegate : IBcfBuilderDelegate {
  //   public IBcfBuilderDelegate.OnMarkupCreated<Markup>
  //     OnMarkupCreated { get; } = Console.WriteLine;
  //   
  //   public IBcfBuilderDelegate.OnExtensionsCreated<Extensions>
  //     ExtensionsCreated { get; } = Console.WriteLine;
  //   
  //   public IBcfBuilderDelegate.OnProjectCreated<ProjectInfo>
  //     ProjectCreated { get; } = Console.WriteLine;
  //   
  //   public IBcfBuilderDelegate.OnDocumentCreated<DocumentInfo>
  //     DocumentCreatedCreated { get; } = Console.WriteLine;
  // }
  
  [Test]
  public async Task ProcessStreamTest() {
    var bcfBuilderDelegateMock = new Mock<IBcfBuilderDelegate>();
    _streamBuilder = new BcfBuilder(bcfBuilderDelegateMock.Object);
    
    await using var stream = new FileStream(
      "Resources/Bcf/v3.0/UserAssignment.bcfzip",
      FileMode.Open,
      FileAccess.Read);
    await _streamBuilder.ProcessStream(stream);
    
    bcfBuilderDelegateMock
      .Verify(d => d.MarkupCreated, Times.Once);
  }
}