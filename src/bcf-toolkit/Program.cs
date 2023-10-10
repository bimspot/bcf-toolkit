using System;
using System.Threading.Tasks;
using BcfToolkit.Converter;
using BcfToolkit.Model;

namespace BcfToolkit;

internal static class Program {
  private static async Task Main(string[] args) {
    if (args.Length < 2) {
      // TODO: make a proper arg parser
      Console.WriteLine(
        "Please specify the path to the source and target files.");
      Console.WriteLine(@"
        Usage:

        $ bcf-toolkit /path/to/source.bcfzip /path/to/target/folder 2.1

        $ bcf-toolkit /path/to/json/folder /path/to/target/bcf.bcfzip 2.1

      ");
      Environment.Exit(1);
    }

    var sourcePath = args[0].TrimEnd('/');
    var targetFolder = args[1].TrimEnd('/');

    // TODO: read bcf.version and decide on the parser version
    // by default 2.1 version is used
    var targetVersion = args.Length > 2 ? args[2] : "2.1";

    try {
      var version =
        (BcfVersionEnum)Enum.Parse(typeof(BcfVersionEnum), targetVersion);
      var context = new ConverterContext(version);
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