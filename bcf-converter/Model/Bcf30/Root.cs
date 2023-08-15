using System.Threading.Tasks;
using bcf.Converter;

namespace bcf.bcf30;

public class Root : IRoot {
  public DocumentInfo? document;
  public Extensions extensions;
  public ProjectInfo? project;

  public Task WriteBcf(string folder) {
    return Task.WhenAll(
      Task.Run(() =>
        BcfConverter.WriteBcfFile(folder, "extensions.xml", extensions)),
      Task.Run(() =>
        BcfConverter.WriteBcfFile(folder, "project.bcfp", project)),
      Task.Run(() =>
        BcfConverter.WriteBcfFile(folder, "documents.xml", document))
    );
  }
}