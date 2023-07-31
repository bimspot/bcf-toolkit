using System;
using System.Threading.Tasks;

namespace bcf_converter; 

internal class Program {
  private static async Task Main(string[] args) {
    if (args.Length < 2) {
      Console.WriteLine(
        "Please specify the path to the source and target files.");
      Console.WriteLine(@"
          Usage:

          $ bcf-converter /path/to/source.bcfzip /path/to/target/folder 2.1

          $ bcf-converter /path/to/json/folder /path/to/target/bcf.bcfzip 2.1

        ");
      Environment.Exit(1);
    }

    var sourcePath = args[0].TrimEnd('/');
    var targetFolder = args[1].TrimEnd('/');
      
    // TODO: read bcf.version and decide on the parser version
    // by default 2.1 version is used
    var targetVersion = args.Length > 2 ? args[2] : "2.1";
      
    try {
      var context = new Context();
      context.SetVersion(targetVersion);
      await context.Convert(sourcePath, targetFolder);
    }
    catch (Exception e) {
      var errorWriter = Console.Error;
      await errorWriter.WriteLineAsync(e.Message);
      await errorWriter.WriteLineAsync(e.StackTrace);
      Environment.Exit(9);
    }
    Environment.Exit(0);
  }
}