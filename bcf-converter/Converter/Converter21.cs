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
  ///
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
    // TODO generate the project.xsd
    //var projectInfo = await BcfParser.ParseProject<ProjectInfo>(source);
    // var roots = new {
    //   ProjectInfo = projectInfo,
    // };
    // await _jsonConverter.WriteObjectJson(roots, target);
    
    // Parsing topics folder (markups)
    var markups = await BcfConverter.ParseMarkups<Markup, VisualizationInfo>(source);
    await JsonConverter.WriteMarkupsJson(markups, target);
  }

  /// <summary>
  ///   The method reads the JSON files of version 2.1 and creates BCF.
  ///   The json folder must contain files which are named using the
  ///   `uuid` of the `Topic` within, and `bcfRoot.json`.
  /// </summary>
  /// <param name="source">The source folder to the JSON files.</param>
  /// <param name="target">The target path where the BCF is written.</param>
  public async Task JsonToBcf(string source, string target) {
    await JsonConverter.JsonToBcf<Markup,Version>(source, target);
  }
}