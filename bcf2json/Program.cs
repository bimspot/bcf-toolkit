using System;
using System.IO;
using System.Threading.Tasks;
using bcf2json.Parser;
using Newtonsoft.Json;

namespace bcf2json {
  internal class Program {
    private static async Task Main(string[] args) {
      if (args.Length < 1) {
        Console
          .WriteLine("Please specify the path to the BCFZIP.");
        Console.WriteLine(@"
          Usage:
          
          $ bcf2json /path/to/some.bcfzip

        ");
        Environment.Exit(1);
      }

      var bcfzipPath = args[0];
      var parser = new Xml20();
      var topics = await parser.parse(bcfzipPath);
      var json = JsonConvert
        .SerializeObject(
          topics, 
          Formatting.Indented, new JsonSerializerSettings { 
            NullValueHandling = NullValueHandling.Ignore
          });
      using (StreamWriter writer = File.CreateText("bcf.json")) {
        await writer.WriteAsync(json);
      }

      Environment.Exit(0);
    }
  }
}