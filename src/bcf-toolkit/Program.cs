using System;
using System.Threading.Tasks;
using System.CommandLine;
using System.IO;
using BcfToolkit.Builder.Bcf21;
using BcfToolkit.Builder.Bcf21.Interfaces;
using BcfToolkit.Model.Bcf21;

namespace BcfToolkit;



internal static class Program {
  private static async Task Main(string[] args) {
    await using var stream = new FileStream(
      "/Users/balintbende/Developer/test/bcf/bcftoolkittest.bcfzip",
      FileMode.Open,
      FileAccess.Read);

    // var bcfBuilderDelegate = new BcfBuilderDelegate();
    // var streamBuilder = new BcfBuilder(bcfBuilderDelegate);
    // await streamBuilder.ProcessStream(stream);

    var builder = new BcfBuilder();
    var bcf = await builder.BuildInMemoryFromStream(stream);
    Console.WriteLine(bcf.Markups.Count);

    stream.Close();

    // await HandleArguments(args);
    Environment.Exit(0);
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
      // var worker = new Worker();
      // await worker.Convert(arguments.SourcePath, arguments.Target);
      await using var stream = new FileStream(
        "/Users/balintbende/Developer/test/bcf/NBHU_BT_BEHF.bcfzip",
        FileMode.Open,
        FileAccess.Read);

      // var bcfBuilderDelegate = new BcfBuilderDelegate();
      // var streamBuilder = new BcfBuilder(bcfBuilderDelegate);
      // await streamBuilder.ProcessStream(stream);



      var builder = new BcfBuilder();
      var bcf = await builder.BuildInMemoryFromStream(stream);
      Console.WriteLine(bcf.Markups.Count);

      builder = null;
      bcf = null;

      stream.Close();
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