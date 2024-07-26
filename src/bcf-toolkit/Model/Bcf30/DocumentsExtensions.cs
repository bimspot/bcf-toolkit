using System.IO.Compression;
using System.Linq;
using System.Xml.Serialization;
using BcfToolkit.Model.Interfaces;
using BcfToolkit.Utils;
using Newtonsoft.Json;

namespace BcfToolkit.Model.Bcf30;

public partial class DocumentInfo : IDocumentInfo {
  public void SetDocumentData(ZipArchiveEntry entry) {
    var fileName = entry.Name;
    var document = this.Documents
      .FirstOrDefault(doc => doc.Guid.Equals(fileName));
    if (document is null) return;
    var mime = $"data:{MimeTypes.GetMimeType(document.Filename)};base64";
    document.DocumentData = new FileData {
      Mime = mime,
      Data = entry.Data()
    };
  }
}

public partial class Document : IDocument {
  /// <summary>
  ///   The document file data as base64 encoded string.
  /// </summary>
  [XmlIgnore]
  [JsonProperty("document_data")]
  public required FileData DocumentData { get; set; }
}