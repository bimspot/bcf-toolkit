using System.Threading.Tasks;
using BcfToolkit.Model.Interfaces;

namespace BcfToolkit.Model.Bcf30;

public class Root : IRoot {
  public DocumentInfo? Document { get; set; }

  [System.ComponentModel.DataAnnotations.RequiredAttribute]
  public Extensions Extensions { get; set; }

  public ProjectInfo? Project { get; set; }

  public Task WriteBcf(string folder) {
    return Task.WhenAll(
      Task.Run(() =>
        Utils.BcfExtensions.SerializeAndWriteXmlFile(folder, "extensions.xml", Extensions)),
      Task.Run(() =>
        Utils.BcfExtensions.SerializeAndWriteXmlFile(folder, "project.bcfp", Project)),
      Task.Run(() =>
        Utils.BcfExtensions.SerializeAndWriteXmlFile(folder, "documents.xml", Document))
    );
  }
}