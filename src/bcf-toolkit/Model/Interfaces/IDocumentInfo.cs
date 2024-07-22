using System.Collections.Generic;
using System.IO.Compression;

namespace BcfToolkit.Model.Interfaces;

public interface IDocumentInfo {
  public void SetDocumentData(ZipArchiveEntry entry);
}