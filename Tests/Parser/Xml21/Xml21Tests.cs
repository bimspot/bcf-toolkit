using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;

namespace Tests.Parser.Xml21 {
  [TestFixture]
  public class Xml21Tests {
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
    public async Task convertsBcfToJsonFiles() {
      var parser = new bcf_converter.Parser.Xml21.Xml21();
      var json21 = new bcf_converter.Parser.Json21.Json21(jsonSerializer);
      var markups = await parser.Parse("Resources/Test.zip");
      await json21.WriteJson(markups, "Resources/output/json");
    }
  }
}