using System;
using BcfToolkit.Builder.Bcf30.Interfaces;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Model.Bcf30;
using Bcf = BcfToolkit.Model.Bcf30.Bcf;
using Markup = BcfToolkit.Model.Bcf30.Markup;

namespace BcfToolkit.Builder.Bcf30;

public partial class BcfBuilder : IBcfBuilder<
    BcfBuilder,
    MarkupBuilder,
    ProjectInfoBuilder,
    ExtensionsBuilder,
    DocumentInfoBuilder>,
  IDefaultBuilder<BcfBuilder> {
  private readonly Bcf _bcf = new();

  public BcfBuilder() {
    _bcf.Version = new VersionBuilder()
      .WithDefaults()
      .Build();
  }

  public BcfBuilder AddMarkup(Action<MarkupBuilder> builder) {
    var markup =
      BuilderUtils.BuildItem<MarkupBuilder, Markup>(builder);
    _bcf.Markups.Add(markup);
    return this;
  }

  public BcfBuilder SetProjectInfo(Action<ProjectInfoBuilder> builder) {
    var projectInfo =
      BuilderUtils.BuildItem<ProjectInfoBuilder, ProjectInfo>(builder);
    _bcf.Project = projectInfo;
    return this;
  }

  public BcfBuilder SetExtensions(Action<ExtensionsBuilder> builder) {
    var extensions =
      BuilderUtils.BuildItem<ExtensionsBuilder, Extensions>(builder);
    _bcf.Extensions = extensions;
    return this;
  }

  public BcfBuilder SetDocumentInfo(Action<DocumentInfoBuilder> builder) {
    var documentInfo =
      BuilderUtils.BuildItem<DocumentInfoBuilder, DocumentInfo>(builder);
    _bcf.Document = documentInfo;
    return this;
  }

  public BcfBuilder WithDefaults() {
    this
      .AddMarkup(m => m.WithDefaults())
      .SetExtensions(e => e.WithDefaults());
    return this;
  }

  public Bcf Build() {
    return BuilderUtils.ValidateItem(_bcf);
  }
}