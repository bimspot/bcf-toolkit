using System.Threading.Tasks;
using bcf.Converter;

namespace bcf.bcf21; 

public class Root : IRoot {
  //TODO generate code
  //public ProjectInfo? project;
  
  public Task WriteBcf(string folder) {
    return Task.Run(async () => {
      //TODO
      //await BcfConverter.WriteBcf(folder, "project.bcfp", this.project);
    });
  }
}