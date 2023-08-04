using System.Threading.Tasks;
using bcf.Parser;
using bcf.bcf21;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace bcf; 

/// <summary>
///   Converter strategy class for converting BCF 2.1 files to JSON
///   and back.
/// </summary>
public class Converter21 : IConverter {
  private readonly JsonConverter<Markup, Version> _jsonConverter;

  /// <summary>
  ///   Creates and returns an instance of `Converter21`.
  /// </summary>
  public Converter21() {
    var contractResolver = new DefaultContractResolver {
      NamingStrategy = new SnakeCaseNamingStrategy()
    };
    var jsonSerializer = new JsonSerializer {
      NullValueHandling = NullValueHandling.Ignore,
      ContractResolver = contractResolver
    };
    _jsonConverter = new JsonConverter<Markup, Version>(jsonSerializer);
  }
  
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
    var markups = await BcfParser.ParseMarkups<Markup, VisualizationInfo>(source);
    await _jsonConverter.WriteMarkupsJson(markups, target);
  }

  /// <summary>
  ///   The method reads the JSON files of version 2.1 and creates BCF.
  ///   The json folder must contain files which are named using the
  ///   `uuid` of the `Topic` within, and `bcfRoot.json`.
  /// </summary>
  /// <param name="source">The source folder to the JSON files.</param>
  /// <param name="target">The target path where the BCF is written.</param>
  public async Task JsonToBcf(string source, string target) {
    await _jsonConverter.JsonToBcf(source, target);
  }
}