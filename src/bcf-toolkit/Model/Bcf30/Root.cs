using System.Threading.Tasks;

namespace BcfToolkit.Model.Bcf30;

public class Root : IRoot {
  public DocumentInfo? Document { get; set; }

  [System.ComponentModel.DataAnnotations.RequiredAttribute]
  public Extensions Extensions { get; set; }

  public ProjectInfo? Project { get; set; }

  public Task WriteBcf(string folder) {
    return Task.WhenAll(
      Task.Run(() =>
        Utils.BcfExtensions.WriteBcfFile(folder, "extensions.xml", Extensions)),
      Task.Run(() =>
        Utils.BcfExtensions.WriteBcfFile(folder, "project.bcfp", Project)),
      Task.Run(() =>
        Utils.BcfExtensions.WriteBcfFile(folder, "documents.xml", Document))
    );
  }
}