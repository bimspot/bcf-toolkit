using System.Threading.Tasks;
using bcf.Parser;
using bcf.bcf30;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace bcf; 

public class Converter30 : IConverter { 
  private readonly JsonConverter<Markup, Version> _jsonConverter;
  
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
  /// <param name="source">The path to the BCFzip.</param>
  /// <param name="target"></param>
  public async Task BcfToJson(string source, string target) {
    // Parsing BCF file structure
    var extensions = await BcfParser.ParseExtensions<Extensions>(source);
    var projectInfo = await BcfParser.ParseProject<ProjectInfo>(source);
    var documentInfo = await BcfParser.ParseDocuments<DocumentInfo>(source);
    var roots = new {
      Extensions = extensions, 
      ProjectInfo = projectInfo, 
      DocumentInfo = documentInfo
    };
    await _jsonConverter.WriteObjectJson(roots, target);
    
    // Parsing topics (markups)
    var markups = await BcfParser.ParseMarkups<Markup,VisualizationInfo>(source);
    await _jsonConverter.WriteMarkupsJson(markups, target);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="source"></param>
  /// <param name="target"></param>
  public async Task JsonToBcf(string source, string target) {
    await _jsonConverter.JsonToBcf(source, target);
  }
}