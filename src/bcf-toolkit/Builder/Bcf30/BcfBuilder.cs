using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BcfToolkit.Utils;
using BcfToolkit.Model;
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
      (Markup)BuilderUtils.BuildItem<MarkupBuilder, IMarkup>(builder);
    _bcf.Markups.Add(markup);
    return this;
  }

  public BcfBuilder AddMarkups(List<Markup> markups) {
    markups.ForEach(m => _bcf.Markups.Add(m));
    return this;
  }

  public BcfBuilder SetProject(Action<ProjectBuilder> builder) {
    var project =
      (ProjectInfo)BuilderUtils.BuildItem<ProjectBuilder, IProject>(builder);
    _bcf.Project = project;
    return this;
  }

  public BcfBuilder WithDefaults() {
    this
      .AddMarkup(m => m.WithDefaults())
      .SetExtensions(e => e.WithDefaults());
    return this;
  }

  public async Task<IBcf> BuildFromStream(Stream source) {
    _bcf.Markups = await BcfExtensions.ParseMarkups<Markup, VisualizationInfo>(source);
    _bcf.Extensions = await BcfExtensions.ParseExtensions<Extensions>(source);
    _bcf.Project = await BcfExtensions.ParseProject<ProjectInfo>(source);
    _bcf.Document = await BcfExtensions.ParseDocuments<DocumentInfo>(source);
    return BuilderUtils.ValidateItem(_bcf);
  }

  public IBcf Build() {
    return BuilderUtils.ValidateItem(_bcf);
  }
}