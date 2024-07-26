using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml.Serialization;

namespace BcfToolkit.Utils;

public static class ZipArchiveExtensions {
  /// <summary>
  ///   The method serializes the specified type object, creates a new entry
  ///   and writes to the zip archive. 
  /// </summary>
  /// <param name="this">The zip archive which the object is written in.</param>
  /// <param name="entry">The name of the target entry.</param>
  /// <param name="obj">The object which will be written.</param>
  /// <typeparam name="T">Generic object type parameter.</typeparam>
  public static void CreateEntryFromObject<T>(this ZipArchive @this, string entry, T? obj) {
    if (obj == null) return;
    var zipEntry = @this.CreateEntry(entry);
    using var entryStream = zipEntry.Open();
    using var writer = new StreamWriter(entryStream);
    new XmlSerializer(typeof(T)).Serialize(writer, obj);
  }

  /// <summary>
  ///   The method creates a new entry and writes the specified byte array to
  ///   the zip archive.
  /// </summary>
  /// <param name="this">The zip archive which the byte array is written in.</param>
  /// <param name="entry">The name of the target entry.</param>
  /// <param name="bytes">The byte array which will be written.</param>
  public static void CreateEntryFromBytes(
    this ZipArchive @this,
    string entry,
    byte[] bytes) {
    var zipEntry = @this.CreateEntry(entry);
    using var entryStream = zipEntry.Open();
    entryStream.Write(bytes, 0, bytes.Length);
  }

  public static List<ZipArchiveEntry> DocumentEntries(this ZipArchive @this) {
    return @this
      .Entries
      .OrderBy(entry => entry.FullName)
      .Where(entry => entry.IsDocumentsFolder())
      .ToList();
  }

  public static ZipArchiveEntry? DocumentEntry(this ZipArchive @this, string fileName) {
    return @this
      .Entries
      .FirstOrDefault(entry => entry.Name.Equals(fileName));
  }
}