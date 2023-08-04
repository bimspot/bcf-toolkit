using System.Threading.Tasks;
using bcf.Parser;
using bcf.bcf30;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace bcf; 

/// <summary>
///   Converter strategy class for converting BCF 3.0 files to JSON
///   and back.
/// </summary>
public class Converter30 : IConverter { 
  private readonly JsonConverter<Markup, Version> _jsonConverter;
  
  /// <summary>
  ///   Creates and returns an instance of `Converter30`.
  /// </summary>
  public Converter30() {
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
  ///   The method parses the BCF file of version 3.0 and writes into JSON.
  ///
  ///   The root of the BCF zip contains the following files:
  ///   - extensions.xml
  ///   - project.bcfp (optional)
  ///   - documents.xml (optional)
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
    var extensions = await BcfParser.ParseExtensions<Extensions>(source);
    var projectInfo = await BcfParser.ParseProject<ProjectInfo>(source);
    var documentInfo = await BcfParser.ParseDocuments<DocumentInfo>(source);
    var roots = new {
      Extensions = extensions, 
      ProjectInfo = projectInfo, 
      DocumentInfo = documentInfo
    };
    await _jsonConverter.WriteBcfRootsJson(roots, target);
    
    // Parsing topics folder (markups)
    var markups = await BcfParser.ParseMarkups<Markup,VisualizationInfo>(source);
    await _jsonConverter.WriteMarkupsJson(markups, target);
  }

  /// <summary>
  ///   The method reads the JSON files of version 3.0 and creates BCF.
  ///   The json folder must contain files which are named using the
  ///   `uuid` of the `Topic` within, and `bcfRoot.json`.
  /// </summary>
  /// <param name="source">The source folder to the JSON files.</param>
  /// <param name="target">The target path where the BCF is written.</param>
  public async Task JsonToBcf(string source, string target) {
    await _jsonConverter.JsonToBcf(source, target);
  }
}