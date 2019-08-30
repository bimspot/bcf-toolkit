using System.IO;
using System.Threading.Tasks;
using bcf_converter.Parser.Xml20;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;

namespace Tests {
  public class Tests {
    [SetUp]
    public void Setup() { }

    [Test]
    public async Task Test1() {
      var bcfzipPath = "Resources/Test.zip";
      var jsonPath = "Resources/Test.json";

      var parser = new Xml20();
      var topics = await parser.parse(bcfzipPath);

      var contractResolver = new DefaultContractResolver {
        NamingStrategy = new SnakeCaseNamingStrategy()
      };

      var json = JsonConvert
        .SerializeObject(
          topics,
          Formatting.Indented, new JsonSerializerSettings {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = contractResolver
          });

      using (var writer = File.CreateText(jsonPath)) {
        writer.Write(json);
      }

      Assert.Pass();
    }
  }
}