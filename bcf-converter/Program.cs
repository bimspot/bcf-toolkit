using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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

          $ ifc-converter /path/to/source.json /path/to/target/folder 2.1

        ");
        Environment.Exit(1);
      }

      var sourcePath = args[0];
      var targetPath = args[1];
      var targetVersion = args.Length > 2 ? args[2] : "2.1";

      var contractResolver = new DefaultContractResolver {
        NamingStrategy = new SnakeCaseNamingStrategy()
      };

      var jsonSerializerSettings = new JsonSerializerSettings {
        NullValueHandling = NullValueHandling.Ignore,
        ContractResolver = contractResolver
      };

      if (sourcePath.EndsWith("json")) {
        // TODO: clean this up
        using var file = File.OpenText(sourcePath);
        JsonSerializer serializer =
          new JsonSerializer {ContractResolver = contractResolver};

        Markup markup = (Markup) serializer.Deserialize(file, typeof(Markup));
        var guid = markup.Topic.Guid;

        var targetFolder = $"{targetPath}/{guid}";
        if (Directory.Exists(targetFolder)) {
          Directory.Delete(targetFolder, true);
        }

        Directory.CreateDirectory(targetFolder);

        await using var markupWriter =
          File.CreateText($"{targetPath}/{guid}/markup.bcf");
        new XmlSerializer(typeof(Markup)).Serialize(markupWriter, markup);

        await using var viewpointWriter =
          File.CreateText($"{targetPath}/{guid}/viewpoint.bcfv");
        new XmlSerializer(typeof(VisualizationInfo)).Serialize(viewpointWriter,
          markup.Viewpoints.First().VisualizationInfo);

        var snapshotFileName = markup.Viewpoints.First().Snapshot;
        var base64String = markup.Viewpoints.First().SnapshotData;
        string result = Regex.Replace(base64String,
          @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
        await File.WriteAllBytesAsync($"{targetPath}/{guid}/{snapshotFileName}",
          Convert.FromBase64String(result));
      }
      else if (sourcePath.EndsWith("bcfzip")) {
        // TODO: read bcf.version and decide on the parser version
        var parser = new Xml20();
        var markups = await parser.parse(sourcePath);


        var json = JsonConvert
          .SerializeObject(
            markups,
            Formatting.None, jsonSerializerSettings);

        await using var writer = File.CreateText(targetPath);
        await writer.WriteAsync(json);
      }

      Environment.Exit(0);
    }
  }
}