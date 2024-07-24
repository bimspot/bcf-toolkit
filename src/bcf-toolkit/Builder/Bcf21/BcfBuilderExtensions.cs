using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using BcfToolkit.Model.Bcf21;
using BcfToolkit.Utils;

namespace BcfToolkit.Builder.Bcf21;

public partial class BcfBuilder {
  public async Task<Bcf> BuildFromStream(Stream source) {
    _bcf.Markups =
      await BcfExtensions.ParseMarkups<Markup, VisualizationInfo>(source);
    _bcf.Project = await BcfExtensions.ParseProject<ProjectExtension>(source);
    SetDocumentData(source);
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
  ///   It turns the additional file data that are referenced as internal
  ///   document in markups into the DocumentInfo.
  /// </summary>
  /// <param name="stream">The file stream of the BCFzip.</param>
  /// <returns></returns>
  /// <exception cref="ArgumentException"></exception>
  private void SetDocumentData(
    Stream stream) {
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