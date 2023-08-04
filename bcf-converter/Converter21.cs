using System.Threading.Tasks;
using bcf.Parser;
using bcf.bcf21;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Version = bcf.bcf30.Version;

namespace bcf; 

public class Converter21 : IConverter {
  private readonly JsonConverter<Markup, Version> _jsonConverter;

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
  public async Task BcfToJson(string source, string target) {
    // Parsing BCF file structure
    // TODO generate the project.xsd
    //var projectInfo = await BcfParser.ParseProject<ProjectInfo>(source);
    // var roots = new {
    //   ProjectInfo = projectInfo,
    // };
    // await _jsonConverter.WriteObjectJson(roots, target);
    
    // Parsing topics (markups)
    var markups = await BcfParser.ParseMarkups<Markup, VisualizationInfo>(source);
    await _jsonConverter.WriteMarkupsJson(markups, target);
  }

  public async Task JsonToBcf(string source, string target) {
    await _jsonConverter.JsonToBcf(source, target);
  }
}