using System;
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

  public IBcf Build() {
    return BuilderUtils.ValidateItem(_bcf);
  }


}