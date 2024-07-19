using System.Threading.Tasks;
using BcfToolkit.Model.Interfaces;

namespace BcfToolkit.Model.Bcf21;

public class Root : IRoot {
  public ProjectExtension? Project { get; set; }

  public Task WriteBcf(string folder) {
    return Task.Run(async () => {
      await Utils.BcfExtensions.SerializeAndWriteXmlFile(folder, "project.bcfp", Project);
    });
  }
}