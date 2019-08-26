using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using bcf2json.Model;
using bcf2json.Parser;

namespace bcf2json {
  class Program {
    static async Task Main(string[] args) {
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
      var json = await parser.jsonFrom(bcfzipPath);
      Console.WriteLine(json);
      
      Environment.Exit(0);
    }
  }
}