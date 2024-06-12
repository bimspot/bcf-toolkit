using System.IO;
using System.IO.Compression;
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
  /// <typeparam name="T">Generic type parameter.</typeparam>
  public static void SerializeAndCreateEntry<T>(this ZipArchive @this, string entry, T? obj) {
    if (obj == null) return;
    var zipEntry = @this.CreateEntry(entry);
    using var entryStream = zipEntry.Open();
    using var writer = new StreamWriter(entryStream);
    new XmlSerializer(typeof(T)).Serialize(writer, obj);
  }
}