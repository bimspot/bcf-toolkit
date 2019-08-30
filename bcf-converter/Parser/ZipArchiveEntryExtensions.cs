using System;
using System.IO.Compression;

namespace bcf_converter.Parser {
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
    public static bool isBcf(this ZipArchiveEntry entry) {
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
    public static bool isBcfViewpoint(this ZipArchiveEntry entry) {
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
    public static bool isSnapshot(this ZipArchiveEntry entry) {
      return entry.FullName.EndsWith(".png",
               StringComparison.OrdinalIgnoreCase) ||
             entry.FullName.EndsWith(".jpg",
               StringComparison.OrdinalIgnoreCase);
    }
  }
}