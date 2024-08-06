using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using BcfToolkit.Model;

namespace BcfToolkit.Utils;

public static class ZipArchiveEntryExtensions {
  /// <summary>
  ///   A convenience method returns true if the file in the entry has the
  ///   extension of `.bcf`.
  /// </summary>
  /// <param name="entry">The ZipArchiveEntry.</param>
  /// <returns>
  ///   Returns true if the file in the entry has the
  ///   extension of `.bcf`.
  /// </returns>
  public static bool IsBcf(this ZipArchiveEntry entry) {
    return entry.FullName.EndsWith(".bcf",
      StringComparison.OrdinalIgnoreCase);
  }

  /// <summary>
  ///   A convenience method returns true if the file in the entry has the
  ///   extension of `.bcfv`.
  /// </summary>
  /// <param name="entry">The ZipArchiveEntry.</param>
  /// <returns>
  ///   Returns true if the file in the entry has the
  ///   extension of `.bcfv`.
  /// </returns>
  public static bool IsBcfViewpoint(this ZipArchiveEntry entry) {
    return entry.FullName.EndsWith(".bcfv",
      StringComparison.OrdinalIgnoreCase);
  }

  /// <summary>
  ///   A convenience method returns true if the file in the entry has the
  ///   extension of `.png` or `.jpg`.
  /// </summary>
  /// <param name="entry">The ZipArchiveEntry.</param>
  /// <returns>
  ///   Returns true if the file in the entry has the
  ///   extension of `.png` or `.jpg`
  /// </returns>
  public static bool IsSnapshot(this ZipArchiveEntry entry) {
    return entry.FullName.EndsWith(".png",
             StringComparison.OrdinalIgnoreCase) ||
           entry.FullName.EndsWith(".jpg",
             StringComparison.OrdinalIgnoreCase);
  }

  /// <summary>
  ///   A convenience method returns true if the file in the entry is
  ///   `extensions.xml` exactly.
  /// </summary>
  /// <param name="entry">The ZipArchiveEntry.</param>
  /// <returns>
  ///   Returns true if the file in the entry is `extensions.xml` exactly.
  /// </returns>
  public static bool IsExtensions(this ZipArchiveEntry entry) {
    return entry.FullName.Equals("extensions.xml",
      StringComparison.OrdinalIgnoreCase);
  }

  /// <summary>
  ///   A convenience method returns true if the file in the entry has the
  ///   extension of `.bcfp`.
  /// </summary>
  /// <param name="entry">The ZipArchiveEntry.</param>
  /// <returns>
  ///   Returns true if the file in the entry has the
  ///   extension of `.bcfp`.
  /// </returns>
  public static bool IsBcfProject(this ZipArchiveEntry entry) {
    return entry.FullName.EndsWith(".bcfp",
      StringComparison.OrdinalIgnoreCase);
  }

  /// <summary>
  ///   A convenience method returns true if the file in the entry is
  ///   `documents.xml` exactly.
  /// </summary>
  /// <param name="entry">The ZipArchiveEntry.</param>
  /// <returns>
  ///   Returns true if the file in the entry is `documents.xml` exactly.
  /// </returns>
  public static bool IsDocuments(this ZipArchiveEntry entry) {
    return entry.FullName.Equals("documents.xml",
      StringComparison.OrdinalIgnoreCase);
  }

  public static bool IsDocumentsFolder(this ZipArchiveEntry entry) {
    return entry.FullName.Split("/")[0].Equals("documents",
      StringComparison.OrdinalIgnoreCase);
  }

  /// <summary>
  ///   The convenience extension method extracts the contents of the zipped
  ///   file and converts it into a base64 string.
  ///   Returns the created key value pair with file name and data.
  /// </summary>
  /// <param name="entry">The ZipArchiveEntry containing the file.</param>
  /// <returns>
  ///   Returns the key value pair where the key is the file name and
  ///   the value is the base64 encoded file data as a string.
  /// </returns>
  public static KeyValuePair<string, FileData> FileData(this ZipArchiveEntry entry) {
    var fileName = entry.Name;
    var mime = $"data:{MimeTypes.GetMimeType(fileName)};base64";
    var fileData = new FileData {
      Mime = mime,
      Data = entry.Data()
    };
    return new KeyValuePair<string, FileData>(fileName, fileData);
  }

  public static string Data(this ZipArchiveEntry entry) {
    var buffer = new byte[entry.Length];
    entry
      .Open()
      .ReadExactly(buffer, 0, buffer.Length);
    return Convert.ToBase64String(buffer);
  }

  /// <summary>
  ///   A convenience method returns true if the file in the entry is
  ///   `bcf.version` exactly.
  /// </summary>
  /// <param name="entry">The ZipArchiveEntry.</param>
  /// <returns>
  ///   Returns true if the file in the entry is `bcf.version` exactly.
  /// </returns>
  public static bool IsVersion(this ZipArchiveEntry entry) {
    return entry.FullName.Equals("bcf.version",
      StringComparison.OrdinalIgnoreCase);
  }
}