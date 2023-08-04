using System.Threading.Tasks;

namespace bcf.Converter; 

/// <summary>
///   Converter strategy interface.
/// </summary>
public interface IConverter {
  /// <summary>
  ///   The method parses the BCFzip archive, then writes to json.
  /// </summary>
  /// <param name="source">The source path to the BCFzip.</param>
  /// <param name="target">The target path where the JSON is written.</param>
  /// <returns></returns>
  Task BcfToJson(string source, string target);
  
  /// <summary>
  ///   The method reads the json, then creates and writes to BCF.
  /// </summary>
  /// <param name="source">The source folder to the JSON files.</param>
  /// <param name="target">The target path where the BCF is written.</param>
  /// <returns></returns>
  Task JsonToBcf(string source, string target);
}