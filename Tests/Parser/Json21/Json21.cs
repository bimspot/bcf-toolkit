using System.Threading.Tasks;
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
    public async Task convertsJsonFilesToBcf() {
      var json21 = new bcf_converter.Parser.Json21.Json21(jsonSerializer);
      await json21.JsonToBcf("Resources/json", "Resources/output/bcf/bcf-out.bcfzip");
    }
  }
}