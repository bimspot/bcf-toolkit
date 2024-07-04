using System;
using System.Threading.Tasks;
using System.CommandLine;
using Serilog;
using Serilog.Events;

namespace BcfToolkit;

internal static class Program {
  private static async Task Main(string[] args) {
    await HandleArguments(args);
  }
  private static async Task HandleArguments(string[] args) {
    // Logger setup
    Log.ConfigureDefault();

    Serilog.Log.Logger = new LoggerConfiguration()
      .MinimumLevel.Debug()
      .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
      .Enrich.FromLogContext()
      .CreateLogger();

    Log.Configure(Serilog.Log.Debug, null, null, Serilog.Log.Error);


    var sourcePathOption = new Option<string>(
      name: "--source",
      description: "The absolute path of the source file.") { IsRequired = true };
    sourcePathOption.AddAlias("-s");

    var targetOption = new Option<string>(
      name: "--target",
      description: "The absolute path of the target.") { IsRequired = true };
    targetOption.AddAlias("-t");

    var rootCommand = new RootCommand {
      Description =
      "Bcf toolkit is command line utility for converting BCF " +
      "(Building Collaboration Format) files into json and vice versa. " +
      "The tool converts BCF information across formats and versions."
    };

    rootCommand.AddOption(sourcePathOption);
    rootCommand.AddOption(targetOption);

    rootCommand.SetHandler(
      async arguments => { await DoRootCommand(arguments); },
      new InputArgumentsBinder(
        sourcePathOption,
        targetOption));
    await rootCommand.InvokeAsync(args);
  }

  private static async Task DoRootCommand(InputArguments arguments) {
    try {
      var worker = new Worker();
      await worker.Convert(arguments.SourcePath, arguments.Target);
    }
    catch (Exception e) {
      Log.Error(e.Message);
      Environment.Exit(9);
    }

    Environment.Exit(0);
  }
}