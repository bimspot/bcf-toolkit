using System.IO;
using System.Threading.Tasks;
using BcfToolkit.Model;

namespace BcfToolkit.Converter;

/// <summary>
///   Converter worker interface.
/// </summary>
public interface IConverter {
  /// <summary>
  ///   The method parses the BCFzip archive, then writes to a json file.
  /// </summary>
  /// <param name="source">The source path to the BCFzip.</param>
  /// <param name="target">The target path where the JSON is written.</param>
  /// <returns></returns>
  Task BcfToJson(string source, string target);

  /// <summary>
  ///   The method parses the BCFzip archive stream, then writes to a json file.
  /// </summary>
  /// <param name="source">The file stream of the source BCFzip.</param>
  /// <param name="target">The target path where the JSON is written.</param>
  /// <returns></returns>
  Task BcfToJson(Stream source, string target);

  /// <summary>
  ///   The method reads the json, then creates and writes to a BCFzip file.
  /// </summary>
  /// <param name="source">The source folder to the JSON files.</param>
  /// <param name="target">The target path where the BCF is written.</param>
  /// <returns></returns>
  Task JsonToBcf(string source, string target);

  /// <summary>
  ///   The method converts the specified BCF object to the given version, then
  ///   returns a stream from the BCF zip archive. The method saves the bcf files
  ///   locally into a tmp folder or creates a zip entry from a memory stream
  ///   based on the input.
  ///
  ///   WARNING: Disposing of the stream is the responsibility of the user.
  /// </summary>
  /// <param name="bcf">The BCF object.</param>
  /// <param name="targetVersion">The BCF version.</param>
  /// <returns>Returns the file stream of the BCF zip archive.</returns>
  Task<Stream> ToBcf(IBcf bcf, BcfVersionEnum targetVersion);

  /// <summary>
  ///   The method converts the specified BCF object to the given version, then
  ///   writes it to the specified stream.
  /// </summary>
  /// <param name="bcf">The BCF object.</param>
  /// <param name="targetVersion">The BCF version.</param>
  /// <param name="stream">The output stream.</param>
  void ToBcf(IBcf bcf, BcfVersionEnum targetVersion, Stream stream);

  /// <summary>
  ///   The method writes the specified BCF model to BCFzip files.
  /// </summary>
  /// <param name="bcf">The `IBcf` interface of the BCF.</param>
  /// <param name="target">The target path where the BCF is written.</param>
  /// <returns></returns>
  Task ToBcf(IBcf bcf, string target);

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
  /// <returns>
  ///   Returns the `Bcf` object which is specified as a type parameter.
  /// </returns>
  Task<T> BcfFromStream<T>(Stream stream);
}