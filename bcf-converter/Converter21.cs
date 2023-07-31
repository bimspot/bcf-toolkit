using System.Threading.Tasks;
using bcf_converter.Parser;
using bcf_converter.Parser.Json21;
using bcf_converter.Parser.Xml21;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace bcf_converter; 

public class Converter21 : IConverter {
  public IJsonToBcfConverter json() {
    var contractResolver = new DefaultContractResolver {
        NamingStrategy = new SnakeCaseNamingStrategy()
      };
    var jsonSerializer = new JsonSerializer {
      NullValueHandling = NullValueHandling.Ignore,
      ContractResolver = contractResolver
    };
    return new Json21(jsonSerializer);
  }

  public IBcfParser parser() {
    return new Xml21();
  }
}