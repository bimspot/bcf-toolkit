using System.Threading.Tasks;
using bcf_converter.Parser;
using bcf_converter.Parser.Json21;
using bcf_converter.Parser.Xml21;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace bcf_converter; 

public class Converter30 : IConverter { 
  public IJsonToBcfConverter json() {
    var contractResolver = new DefaultContractResolver {
        NamingStrategy = new SnakeCaseNamingStrategy()
      };
    var jsonSerializer = new JsonSerializer {
      NullValueHandling = NullValueHandling.Ignore,
      ContractResolver = contractResolver
    };
    // TODO 3.0
    return new Json21(jsonSerializer);
  }

  public IBcfParser parser() {
    // TODO 3.0
    return new Xml21();
  }
}