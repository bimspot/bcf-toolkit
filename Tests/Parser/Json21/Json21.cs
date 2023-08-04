using System.Threading.Tasks;
using bcf.Parser;
using bcf21 = bcf.bcf21;
using bcf30 = bcf.bcf30;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;

namespace Tests.Parser.Json21 {
  [TestFixture]
  public class Json21Tests {
    private JsonSerializer jsonSerializer = null!;

    [SetUp]
    public void setup() {
      var contractResolver = new DefaultContractResolver {
        NamingStrategy = new SnakeCaseNamingStrategy()
      };

      this.jsonSerializer = new JsonSerializer {
        NullValueHandling = NullValueHandling.Ignore,
        ContractResolver = contractResolver
      };
    }


    [Test]
    public async Task convertsJsonFilesToBcf21() {
      var json21 = new JsonConverter<bcf21.Markup, bcf21.Version>(jsonSerializer);
      await json21.JsonToBcf("Resources/json", "Resources/output/21/bcf/bcf-out.bcfzip");
    }
    
    [Test]
    public async Task convertsJsonFilesToBcf30() {
      var json21 = new JsonConverter<bcf30.Markup, bcf30.Version>(jsonSerializer);
      await json21.JsonToBcf("Resources/json", "Resources/output/30/bcf/bcf-out.bcfzip");
    }
  }
}