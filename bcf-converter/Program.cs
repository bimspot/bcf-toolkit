using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using bcf_converter.Model;
using bcf_converter.Parser.Xml20;
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

          $ ifc-converter /path/to/source.bcfzip /path/to/target.json 2.1

          $ ifc-converter /path/to/source.json /path/to/target.bcfzip 2.1

        ");
        Environment.Exit(1);
      }

      var sourcePath = args[0];
      var targetPath = args[1];
      var targetVersion = args.Length > 2 ? args[2] : "2.1";

      if (sourcePath.EndsWith("json")) {
        using var file = File.OpenText(sourcePath);
        JsonSerializer serializer = new JsonSerializer();
        Topic topic = (Topic)serializer.Deserialize(file, typeof(Topic));
        await using var writer = File.CreateText(targetPath);
        new XmlSerializer(typeof(Topic)).Serialize(writer, topic);
      } else if (sourcePath.EndsWith("bcfzip")) {
        // TODO: read bcf.version and decide on the parser version
        var parser = new Xml20();
        var topics = await parser.parse(sourcePath);

        var contractResolver = new DefaultContractResolver {
          NamingStrategy = new SnakeCaseNamingStrategy()
        };

        var json = JsonConvert
          .SerializeObject(
            topics,
            Formatting.None, new JsonSerializerSettings {
              NullValueHandling = NullValueHandling.Ignore,
              ContractResolver = contractResolver
            });

          await using var writer = File.CreateText(targetPath);
          await writer.WriteAsync(json);
        }
      

      Environment.Exit(0);
    }
  }
}