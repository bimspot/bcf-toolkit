using System;
using System.IO.Compression;
using System.Linq;
using bcf_converter.Model;

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

    /// <summary>
    ///   The convenience extension method extracts the contents of the zipped
    ///   image and converts it into a base64 string.
    ///   Returns the created Snapshot struct.
    /// </summary>
    /// <param name="entry">The ZipArchiveEntry containing the image.</param>
    /// <returns>Returns an instance of a Snapshot.</returns>
    public static Snapshot snapshot(this ZipArchiveEntry entry) {
      var extension = entry.FullName.Split(".").Last();
      var mime = $"data:image/{extension};base64";
      var type = (Snapshot.Type) Enum.Parse(typeof(Snapshot.Type), extension);
      var buffer = new byte[entry.Length];

      var image = entry
        .Open()
        .Read(buffer, 0, buffer.Length);
      var base64String = Convert.ToBase64String(buffer);

      return new Snapshot {
        snapshotType = type,
        snapshotData = $"{mime},{base64String}"
      };
    }
  }
}