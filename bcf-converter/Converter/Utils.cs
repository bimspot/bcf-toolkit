using System.IO;

namespace bcf.Converter;

public static class Utils {
  /// <summary>
  ///   Creates a temporary folder for the intermediate files.
  /// </summary>
  /// <param name="targetFolder"></param>
  /// <returns></returns>
  public static string CreateTmpFolder(string targetFolder) {
    // Will create a tmp folder for the intermediate files.
    var tmpFolder = $"{targetFolder}/tmp";
    if (Directory.Exists(tmpFolder)) Directory.Delete(tmpFolder, true);

    return tmpFolder;
  }
}