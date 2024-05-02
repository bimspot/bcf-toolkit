using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BcfToolkit.Builder.Bcf30.Interfaces;
using BcfToolkit.Builder.Interfaces;
using BcfToolkit.Utils;
using BcfToolkit.Model.Bcf30;
using Bcf = BcfToolkit.Model.Bcf30.Bcf;
using Markup = BcfToolkit.Model.Bcf30.Markup;
using VisualizationInfo = BcfToolkit.Model.Bcf30.VisualizationInfo;

namespace BcfToolkit.Builder.Bcf30;

public partial class BcfBuilder : IBcfBuilder<
    BcfBuilder,
    MarkupBuilder,
    ProjectBuilder>,
  IDefaultBuilder<BcfBuilder> {

  private readonly Bcf _bcf = new();

  public BcfBuilder AddMarkup(Action<MarkupBuilder> builder) {
    var markup =
      BuilderUtils.BuildItem<MarkupBuilder, Markup>(builder);
    _bcf.Markups.Add(markup);
    return this;
  }

  public BcfBuilder SetProject(Action<ProjectBuilder> builder) {
    var projectInfo =
      BuilderUtils.BuildItem<ProjectBuilder, ProjectInfo>(builder);
    _bcf.Project = projectInfo;
    return this;
  }

  public BcfBuilder WithDefaults() {
    this
      .AddMarkup(m => m.WithDefaults())
      .SetExtensions(e => e.WithDefaults());
    return this;
  }

  public async Task<Bcf> BuildFromStream(Stream source) {
    _bcf.Markups = await BcfExtensions.ParseMarkups<Markup, VisualizationInfo>(source);
    _bcf.Extensions = await BcfExtensions.ParseExtensions<Extensions>(source);
    _bcf.Project = await BcfExtensions.ParseProject<ProjectInfo>(source);
    _bcf.Document = await BcfExtensions.ParseDocuments<DocumentInfo>(source);
    return BuilderUtils.ValidateItem(_bcf);
  }

  public Bcf Build() {
    return BuilderUtils.ValidateItem(_bcf);
  }
}