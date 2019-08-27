using System;
using System.IO;
using System.Threading.Tasks;
using bcf2json.Parser;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace bcf2json {
  internal class Program {
    private static async Task Main(string[] args) {
      if (args.Length < 2) {
        Console.WriteLine(
          "Please specify the path to the BCFZIP and to the output json.");
        Console.WriteLine(@"
          Usage:
          
          $ bcf2json /path/to/some.bcfzip /path/to/some.json

        ");
        Environment.Exit(1);
      }

      var bcfzipPath = args[0];
      var jsonPath = args[1];
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
            ContractResolver = contractResolver,
          });
      
      using (StreamWriter writer = File.CreateText(jsonPath)) {
        await writer.WriteAsync(json);
      }

      Environment.Exit(0);
    }
  }
}