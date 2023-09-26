using System.Threading.Tasks;
using bcf.Converter;

namespace bcf.bcf30;

public class Root : IRoot {
  public DocumentInfo? document { get; set; }

  [System.ComponentModel.DataAnnotations.RequiredAttribute()]
  public Extensions extensions { get; set; }

  public ProjectInfo? project { get; set; }

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