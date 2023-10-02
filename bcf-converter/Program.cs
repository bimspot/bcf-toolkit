using System;
using System.Threading.Tasks;
using bcf.Builder;

namespace bcf;

internal static class Program {
  private static async Task Main(string[] args) {
    if (args.Length < 2) {
      // TODO: make a proper arg parser
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
      var version =
        (BcfVersionEnum)Enum.Parse(typeof(BcfVersionEnum), targetVersion);
      // var context = new ConverterContext(version);
      // await context.Convert(sourcePath, targetFolder);


      var builder = MarkupBuilderCreator.CreateBuilder();
      // var builder = new Builder.Bcf21.MarkupBuilder();

      var markup = builder.AddHeaderFile(b => b
          .AddFileName("f"))
        .AddServerAssignedId("")
        .Build();
      // .AddHeaderFile("p1", "s1", true, "f1", DateTime
      // .Now, "r1")
      // .AddHeaderFile("p2", "s2", true, "f2", DateTime
      //   .Now, "r2").Build();
      // var markup = builder
      //   .AddViewPoint(i => i
      //     .AddOrthogonalCamera(c => c
      //       .AddViewPoint(10,10,10)
      //       .AddDirection(10,10,10)
      //       .AddUpVector(10,10,10),
      //       10, 10))
      //   .Build();
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