using System.Threading.Tasks;

namespace BcfToolkit.Model.Bcf21;

public class Root : IRoot {
  public ProjectExtension? Project { get; set; }

  public Task WriteBcf(string folder) {
    return Task.Run(async () => {
      await Converter.BcfConverter.WriteBcfFile(folder, "project.bcfp", Project);
    });
  }
}