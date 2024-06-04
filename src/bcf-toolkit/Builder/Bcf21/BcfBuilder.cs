using System;
using BcfToolkit.Builder.Bcf21.Interfaces;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Bcf21;

public partial class BcfBuilder : IBcfBuilder<
    BcfBuilder,
    MarkupBuilder,
    ProjectExtensionBuilder>,
  IDefaultBuilder<BcfBuilder> {
  private readonly Bcf _bcf = new();
  
  private readonly IBcfBuilderDelegate? _delegate;

  public BcfBuilder(IBcfBuilderDelegate? builderDelegate = null) {
    this._delegate = builderDelegate;
    
    _bcf.Version = new VersionBuilder()
      .WithDefaults()
      .Build();
  }

  public BcfBuilder AddMarkup(Action<MarkupBuilder> builder) {
    var markup =
      (Markup)BuilderUtils.BuildItem<MarkupBuilder, IMarkup>(builder);
    _bcf.Markups.Add(markup);
    return this;
  }

  public BcfBuilder SetProject(Action<ProjectExtensionBuilder> builder) {
    var project =
      (ProjectExtension)BuilderUtils.BuildItem<ProjectExtensionBuilder, IProject>(
        builder);
    _bcf.Project = project;
    return this;
  }

  public BcfBuilder WithDefaults() {
    this.AddMarkup(m => m.WithDefaults());
    return this;
  }

  public Bcf Build() {
    return BuilderUtils.ValidateItem(_bcf);
  }
}