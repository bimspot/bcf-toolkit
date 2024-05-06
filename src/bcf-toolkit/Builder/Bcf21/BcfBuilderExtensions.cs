using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BcfToolkit.Model.Bcf21;
using BcfToolkit.Utils;

namespace BcfToolkit.Builder.Bcf21;

public partial class BcfBuilder {
  public async Task<Bcf> BuildFromStream(Stream source) {
    _bcf.Markups =
      await BcfExtensions.ParseMarkups<Markup, VisualizationInfo>(source);
    _bcf.Project = await BcfExtensions.ParseProject<ProjectExtension>(source);
    return BuilderUtils.ValidateItem(_bcf);
  }

  public BcfBuilder AddMarkups(List<Markup> markups) {
    markups.ForEach(m => _bcf.Markups.Add(m));
    return this;
  }
}