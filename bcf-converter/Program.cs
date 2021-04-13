using System;
using System.IO;
using System.Threading.Tasks;
using bcf_converter.Parser.Json21;
using bcf_converter.Parser.Xml21;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace bcf_converter {
  internal class Program {
    private static async Task Main(string[] args) {
      if (args.Length < 2) {
        Console.WriteLine(
          "Please specify the path to the source and target files.");
        Console.WriteLine(@"
          Usage:

          $ ifc-converter /path/to/source.bcfzip /path/to/target/folder 2.1

          $ ifc-converter /path/to/json/folder /path/to/target/bcf.bcfzip 2.1

        ");
        Environment.Exit(1);
      }

      var sourcePath = args[0].TrimEnd('/');
      var targetFolder = args[1].TrimEnd('/');
      // TODO: read bcf.version and decide on the parser version
      var targetVersion = args.Length > 2 ? args[2] : "2.1";

      var contractResolver = new DefaultContractResolver {
        NamingStrategy = new SnakeCaseNamingStrategy()
      };
      var jsonSerializer = new JsonSerializer {
        NullValueHandling = NullValueHandling.Ignore,
        ContractResolver = contractResolver
      };
      var json21 = new Json21(jsonSerializer);

      if (sourcePath.EndsWith("bcfzip")) {
        var parser = new Xml21();
        var markups = await parser.parse(sourcePath);
        await json21.writeJson(markups, targetFolder);
      }
      else {
        await json21.json2bcf(sourcePath, targetFolder);
      }

      Environment.Exit(0);
    }
  }
}