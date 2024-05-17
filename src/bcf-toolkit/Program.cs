using System;
using System.Threading.Tasks;
using System.CommandLine;

namespace BcfToolkit;

internal static class Program {
  private static async Task Main(string[] args) {
    await HandleArguments(args);
  }
  private static async Task HandleArguments(string[] args) {
    var sourcePathOption = new Option<string>(
      name: "--source",
      description: "The absolute path of the source file.") { IsRequired = true };
    sourcePathOption.AddAlias("-s");

    var targetOption = new Option<string>(
      name: "--target",
      description: "The absolute path of the target.") { IsRequired = true };
    targetOption.AddAlias("-t");

    // var versionOption = new Option<string>(
    //     name: "--bcfVersion",
    //     description:
    //     "The target version of the bcf parser, by default 2.1 is used.") { IsRequired = false };
    // versionOption.AddAlias("-b");
    // versionOption.SetDefaultValue("2.1");

    var rootCommand = new RootCommand {
      Description =
      "Bcf toolkit is command line utility for converting BCF " +
      "(Building Collaboration Format) files into json and vice versa. " +
      "The tool converts BCF information across formats and versions."
    };

    rootCommand.AddOption(sourcePathOption);
    rootCommand.AddOption(targetOption);
    // rootCommand.AddOption(versionOption);

    rootCommand.SetHandler(
      async arguments => { await DoRootCommand(arguments); },
      new InputArgumentsBinder(
        sourcePathOption,
        targetOption));
    // versionOption));
    await rootCommand.InvokeAsync(args);
  }

  private static async Task DoRootCommand(InputArguments arguments) {
    try {
      var worker = new Worker();
      await worker.Convert(arguments.SourcePath, arguments.Target);
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