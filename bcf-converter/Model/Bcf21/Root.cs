using System.Threading.Tasks;
using bcf.Converter;

namespace bcf.bcf21;

public class Root : IRoot {
  public ProjectExtension? project { get; set; }

  public Task WriteBcf(string folder) {
    return Task.Run(async () => {
      await BcfConverter.WriteBcfFile(folder, "project.bcfp", project);
    });
  }
}