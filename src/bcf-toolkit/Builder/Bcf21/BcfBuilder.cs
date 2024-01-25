using System;
using System.IO;
using System.Threading.Tasks;
using BcfToolkit.Converter;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Builder.Bcf21;

public class BcfBuilder : IBcfBuilder<
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

  public BcfBuilder SetProject(Action<ProjectBuilder> builder) {
    var project =
      (ProjectExtension)BuilderUtils.BuildItem<ProjectBuilder, IProject>(builder);
    _bcf.Project = project;
    return this;
  }

  public BcfBuilder WithDefaults() {
    this
      .AddMarkup(m => m.WithDefaults());
    return this;
  }

  public async Task<IBcf> BuildFromStream(Stream source) {
    _bcf.Markups = await BcfConverter.ParseMarkups<Markup, VisualizationInfo>(source);
    _bcf.Project = await BcfConverter.ParseProject<ProjectExtension>(source);
    return BuilderUtils.ValidateItem(_bcf);
  }

  public IBcf Build() {
    return BuilderUtils.ValidateItem(_bcf);
  }
}