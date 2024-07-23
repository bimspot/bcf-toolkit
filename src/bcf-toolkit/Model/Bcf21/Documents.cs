using System.IO;
using System.IO.Compression;
using System.Linq;
using BcfToolkit.Utils;

namespace BcfToolkit.Model.Bcf21;

/// <summary>
///   Representation of a document in BCF.
///
///   NOTIFICATION: This class is not part of the BCF 2.1 model schema!
/// </summary>
public class Document {
  public string FileName { get; set; }
  public FileData DocumentData { get; set; }
}

/// <summary>
///   It stores the referenced internal documents.
///
///   NOTIFICATION: This class is not part of the BCF 2.1 model schema!
/// </summary>
public class DocumentInfo {
  public System.Collections.ObjectModel.Collection<Document> Documents { get; set; }
  
  public void SetDocumentData(ZipArchiveEntry entry) {
    var fileName = entry.Name;
    var document = this.Documents
      .FirstOrDefault(doc => Path.GetFileName(doc.FileName).Equals(fileName));
    if (document is null) return;
    var mime = $"data:{MimeTypes.GetMimeType(document.FileName)};base64";
    document.DocumentData = new FileData {
      Mime = mime,
      Data = entry.Data()
    };
  }
}

