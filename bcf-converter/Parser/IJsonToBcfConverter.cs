using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using bcf.bcf21;

namespace bcf_converter.Parser {
  /// <summary>
  ///   Converting all JSON representations of Markups in a folder to a
  ///   BCFZIP in XML format
  /// </summary>
  public interface IJsonToBcfConverter {
    /// <summary>
    ///   Reads all JSON files in the source folder and creates BCF files for
    ///   them at the target folder, then zipping them all.
    /// </summary>
    /// <param name="sourceFolder">The folder where the JSON files are.</param>
    /// <param name="targetFile">The path to the target bcf file.</param>
    /// <returns></returns>
    Task JsonToBcf(string sourceFolder, string targetFile);

    /// <summary>
    ///   Writes the Markups to json files at the specified target, one json
    ///   file per Markup instance.
    /// </summary>
    /// <param name="markups">The list of Markups in the memory.</param>
    /// <param name="targetFolder">The output folder.</param>
    /// <returns></returns>
    Task WriteJson(ConcurrentBag<Markup> markups, string targetFolder);
  }
}