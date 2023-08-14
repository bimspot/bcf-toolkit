using System.Threading.Tasks;
using bcf.Converter;

namespace bcf.bcf30; 

public class Root : IRoot {
  public Extensions extensions;
  public ProjectInfo? project;
  public DocumentInfo? document;

  public Task WriteBcf(string folder) {
    return Task.WhenAll(
      Task.Run(() => BcfConverter.WriteBcfFile(folder, "extensions.xml", this.extensions)),
      Task.Run(() => BcfConverter.WriteBcfFile(folder, "project.bcfp", this.project)),
      Task.Run(() => BcfConverter.WriteBcfFile(folder, "documents.xml", this.document))
    );
  }
}