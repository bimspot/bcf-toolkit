using System.IO;
using System.Threading.Tasks;
using bcf.bcf21;

namespace bcf.Converter;

/// <summary>
///   Converter strategy class for converting BCF 2.1 files to JSON
///   and back.
/// </summary>
public class Converter21 : IConverter {
  /// <summary>
  ///   The method parses the BCF file of version 2.1 and writes into JSON.
  ///   The root of the BCF zip contains the following files:
  ///   - project.bcfp (optional)
  ///   - bcf.version
  ///   Topic folder structure inside a BCFzip archive:
  ///   - markup.bcf
  ///   Additionally:
  ///   - Viewpoint files (BCFV)
  ///   - Snapshot files (PNG/JPEG)
  ///   - Bitmaps
  /// </summary>
  /// <param name="source">The source path to the BCFzip.</param>
  /// <param name="target">The target path where the JSON is written.</param>
  public async Task BcfToJson(string source, string target) {
    // Parsing BCF root file structure
    var project = await BcfConverter.ParseProject<ProjectExtension>(source);
    var root = new Root {
      project = project
    };

    // Parsing topics folder (markups)
    var markups =
      await BcfConverter.ParseMarkups<Markup, VisualizationInfo>(source);

    // Writing json files
    await JsonConverter.WriteJson(target, markups, root);
  }

  /// <summary>
  ///   The method reads the JSON files and creates BCF 2.1 version.
  ///   The json folder must contain files which are named using the
  ///   `uuid` of the `Topic` within, and `bcfRoot.json`.
  /// </summary>
  /// <param name="source">The source folder to the JSON files.</param>
  /// <param name="target">The target path where the BCF is written.</param>
  public async Task JsonToBcf(string source, string target) {
    // Parsing BCF root - it is an optional file
    var rootPath = $"{source}/bcfRoot.json";
    var root = Path.Exists(rootPath)
      ? await JsonConverter.ParseObject<Root>(rootPath)
      : new Root();

    // Parsing markups
    var markups = await JsonConverter.ParseMarkups<Markup>(source);

    // Writing bcf files
    await BcfConverter.WriteBcf<Markup, VisualizationInfo, Root, Version>(target, markups, root);
  }
}