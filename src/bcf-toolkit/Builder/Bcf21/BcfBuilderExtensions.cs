using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf21;
using BcfToolkit.Utils;

namespace BcfToolkit.Builder.Bcf21;

public partial class BcfBuilder {
  public async Task<Bcf> BuildFromStream(Stream source) {
    _bcf.Markups =
      await BcfExtensions.ParseMarkups<Markup, VisualizationInfo>(source);
    _bcf.Project = await BcfExtensions.ParseProject<ProjectExtension>(source);
    SetDocumentDataFromReferences(source);
    return BuilderUtils.ValidateItem(_bcf);
  }

  public BcfBuilder AddMarkups(List<Markup> markups) {
    markups.ForEach(m => _bcf.Markups.Add(m));
    return this;
  }

  public BcfBuilder SetProject(ProjectExtension? project) {
    _bcf.Project = project;
    return this;
  }

  /// <summary>
  ///   It sets the document file name and base64 data from the specified
  ///   document data dictionary.
  /// </summary>
  /// <param name="documentData">
  ///   Dictionary contains document guid as the key and a tuple of document
  ///   file name and file data as the value
  /// </param>
  /// <returns></returns>
  public BcfBuilder SetDocumentData(
    Dictionary<string, Tuple<string, FileData>>? documentData) {
    if (documentData is null) return this;
    var documentReferences =
      _bcf.Markups.SelectMany(m => m.Topic.DocumentReference).ToList();
    foreach (var data in documentData) {
      var documentReference =
        documentReferences.FirstOrDefault(r =>
          r.ReferencedDocument.Equals(data.Key));
      if (documentReference is null) continue;
      //It is recommended to put files in a folder called Documents in the root
      //folder of the zip archive.
      documentReference.ReferencedDocument = $"../documents/{data.Value.Item1}";
      documentReference.DocumentData = data.Value.Item2;
    }
    return this;
  }

  /// <summary>
  ///   It turns the additional file data that are referenced as internal
  ///   document in markups into the DocumentInfo.
  /// </summary>
  /// <param name="stream">The file stream of the BCFzip.</param>
  /// <returns></returns>
  /// <exception cref="ArgumentException"></exception>
  private void SetDocumentDataFromReferences(Stream stream) {
    if (stream is null || !stream.CanRead)
      throw new ArgumentException("Source stream is not readable.");

    Log.Debug($"\nProcessing documents\n");

    using var archive = new ZipArchive(stream, ZipArchiveMode.Read, true);

    // These additional files can be referenced by other files via their
    // relative paths. It is recommended to put them in a folder called
    // Documents in the root folder of the zip archive.
    var documentReferences = _bcf
      .Markups
      .SelectMany(m => m.Topic.DocumentReference)
      .ToList();

    documentReferences
      .Where(d => !d.IsExternal)
      .ToList()
      .ForEach(d => {
        var fileName = Path.GetFileName(d.ReferencedDocument);
        var entry = archive.DocumentEntry(fileName);
        if (entry is null) return;
        Log.Debug(entry.FullName);
        d.SetDocumentData(entry);
      });

    // Stream must be positioned back to 0 in order to use it again
    stream.Position = 0;
  }
}