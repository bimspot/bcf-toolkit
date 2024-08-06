using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BcfToolkit.Model.Bcf21;
using BcfToolkit.Utils;
using IBcfBuilderDelegate = BcfToolkit.Builder.Bcf21.Interfaces.IBcfBuilderDelegate;

namespace BcfToolkit.Builder.Bcf21;

public partial class BcfBuilder {
  private readonly IBcfBuilderDelegate? _delegate;
  
  public BcfBuilder(IBcfBuilderDelegate? builderDelegate = null) {
    this._delegate = builderDelegate;
    
    _bcf.Version = new VersionBuilder()
      .WithDefaults()
      .Build();
  }
  public async Task ProcessStream(Stream source) {
    if (_delegate is null) {
      Console.WriteLine("IBcfBuilderDelegate is not set.");
      return;
    }

    // await BcfExtensions.ParseMarkups<Markup, VisualizationInfo>(source, _delegate.MarkupCreated);

    // var extensions = await BcfExtensions.ParseExtensions<Extensions>(source);
    // _delegate.ExtensionsCreated(extensions);
    //
    // _bcf.Project = await BcfExtensions.ParseProject<ProjectInfo>(source);
    // _bcf.Document = await BcfExtensions.ParseDocuments<DocumentInfo>(source);
  }

  public async Task<Bcf> BuildInMemoryFromStream(Stream source) {
    _bcf.Markups =
      await BcfExtensions.ParseMarkups<Markup, VisualizationInfo>(source);
    _bcf.Project = await BcfExtensions.ParseProject<ProjectExtension>(source);
    return BuilderUtils.ValidateItem(_bcf);
  }

  public BcfBuilder AddMarkups(List<Markup> markups) {
    markups.ForEach(m => _bcf.Markups.Add(m));
    return this;
  }

  public BcfBuilder SetProject(ProjectExtension? project) {
    _bcf.Project = project;
    return this;
  }
}