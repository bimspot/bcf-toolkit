using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Xml.Serialization;
using BcfToolkit.Model.Interfaces;
using BcfToolkit.Utils;
using Newtonsoft.Json;

namespace BcfToolkit.Model.Bcf30;

public partial class DocumentInfo : IDocumentInfo {
  public void SetDocumentsData(Dictionary<string, string>? documentsData) {
    this.Documents.ToList().ForEach(document => {
      if (documentsData is null) return;
      // For uniqueness, the filename of a document in the BCF must be the
      // document guid
      documentsData.TryGetValue(document.Guid, out var documentData);
      // document.DocumentData = documentData;
    });
  }

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
  public FileData? DocumentData { get; set; }
}