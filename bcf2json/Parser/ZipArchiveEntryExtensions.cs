using System;
using System.IO.Compression;

namespace bcf2json.Parser {
  public static class ZipArchiveEntryExtensions {
    public static bool isBcf(this ZipArchiveEntry entry) {
      return entry.FullName.EndsWith(".bcf",
        StringComparison.OrdinalIgnoreCase);
    }
    
    public static bool isBcfViewpoint(this ZipArchiveEntry entry) {
      return entry.FullName.EndsWith(".bcfv",
        StringComparison.OrdinalIgnoreCase);
    }
    
    public static bool isSnapshot(this ZipArchiveEntry entry) {
      return entry.FullName.EndsWith(".png",
               StringComparison.OrdinalIgnoreCase) || 
             entry.FullName.EndsWith(".jpg",
               StringComparison.OrdinalIgnoreCase);
    }
  }
}