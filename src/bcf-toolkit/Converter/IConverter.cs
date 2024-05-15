using System.IO;
using System.Threading.Tasks;
using BcfToolkit.Model;

namespace BcfToolkit.Converter;

/// <summary>
///   Converter worker interface.
/// </summary>
public interface IConverter {
  /// <summary>
  ///   The method parses the BCFzip archive, then writes to json.
  /// </summary>
  /// <param name="source">The source path to the BCFzip.</param>
  /// <param name="target">The target path where the JSON is written.</param>
  /// <returns></returns>
  Task BcfZipToJson(string source, string target);

  /// <summary>
  ///   The method parses the BCFzip archive stream, then writes to json.
  /// </summary>
  /// <param name="source">The file stream of the source BCFzip.</param>
  /// <param name="target">The target path where the JSON is written.</param>
  /// <returns></returns>
  Task BcfZipToJson(Stream source, string target);

  /// <summary>
  ///   The method reads the json, then creates and writes to BCFzip.
  /// </summary>
  /// <param name="source">The source folder to the JSON files.</param>
  /// <param name="target">The target path where the BCF is written.</param>
  /// <returns></returns>
  Task JsonToBcfZip(string source, string target);

  /// <summary>
  ///   The method handles the BCF content from the given objects to the
  ///   specified stream.
  /// </summary>
  /// <param name="bcf">The BCF object.</param>
  /// <param name="targetVersion">The BCF version.</param>
  /// <returns></returns>
  /// <exception cref="FileNotFoundException"></exception>
  Task<Stream> ToBcfStream(IBcf bcf, BcfVersionEnum targetVersion);

  /// <summary>
  ///   The method writes the specified BCF model to BCFzip files.
  /// </summary>
  /// <param name="bcf">The `IBcf` interface of the BCF.</param>
  /// <param name="target">The target path where the BCF is written.</param>
  /// <returns></returns>
  Task ToBcfZip(IBcf bcf, string target);

  /// <summary>
  ///   The method writes the specified BCF model to JSON files.
  /// </summary>
  /// <param name="bcf">The `IBcf` interface of the BCF.</param>
  /// <param name="target">The target path where the JSON is written.</param>
  /// <returns></returns>
  Task ToJson(IBcf bcf, string target);

  /// <summary>
  ///   The function builds a BCF in memory representation of the specified
  ///   target version from the given stream.
  /// </summary>
  /// <param name="stream">The BCF file stream.</param>
  /// <returns>Returns the `Bcf` object.</returns>
  Task<T> BuildBcfFromStream<T>(Stream stream);
}