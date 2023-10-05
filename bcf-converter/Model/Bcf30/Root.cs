using System.Threading.Tasks;
using bcf.Converter;

namespace bcf.bcf30;

public class Root : IRoot {
  public DocumentInfo? Document { get; set; }

  [System.ComponentModel.DataAnnotations.RequiredAttribute]
  public Extensions Extensions { get; set; }

  public ProjectInfo? Project { get; set; }

  public Task WriteBcf(string folder) {
    return Task.WhenAll(
      Task.Run(() =>
        BcfConverter.WriteBcfFile(folder, "extensions.xml", Extensions)),
      Task.Run(() =>
        BcfConverter.WriteBcfFile(folder, "project.bcfp", Project)),
      Task.Run(() =>
        BcfConverter.WriteBcfFile(folder, "documents.xml", Document))
    );
  }
}