using System.Collections.Concurrent;
using System.Threading.Tasks;
using BcfToolkit.Model;

namespace BcfToolkit.Converter;

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

  /// <summary>
  ///   The method writes the specified BCF models to BCF files.
  /// </summary>
  /// <param name="target">The target path where the BCF is written.</param>
  /// <param name="bcf">The `IBcf` interface of the BCF.
  /// </param>
  /// <returns></returns>
  Task ToBcf(string target, IBcf bcf);

  /// <summary>
  ///   The method writes the specified BCF models to JSON files.
  /// </summary>
  /// <param name="target">The target path where the JSON is written.</param>
  /// <param name="bcf">The `IBcf` interface of the BCF.
  /// </param>
  /// <returns></returns>
  Task ToJson(string target, IBcf bcf);
}