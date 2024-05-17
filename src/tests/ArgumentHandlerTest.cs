using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Builder;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Tests;

public abstract class ArgumentHandlerTest {

  [TestFixture]
  public class CommandLineTests {
    [Test]
    public void CommandLineArgumentsLongVersionTest() {
      // Arrange
      var rootCommand = BuildCommandLine();

      // Act & Assert
      var parser = new CommandLineBuilder(rootCommand).UseDefaults().Build();

      // Test case 1: valid arguments with default bcfVersion value
      var input1 = new List<string>
        {"--source", "sourcePath", "--target", "target"};
      var result1 = parser.Parse(input1);
      Assert.That(0, Is.EqualTo(result1.Errors.Count));
      var bcfVersionOption = rootCommand.Options.FirstOrDefault(opt => opt.Aliases.Contains("--bcfVersion"));

      if (bcfVersionOption != null)
        Assert.That("2.1", Is.EqualTo(result1.GetValueForOption(bcfVersionOption)));
      else {
        Assert.Fail();
      }

      // Test case 2: missing required argument (--source)
      var input2 = new List<string> { "--target", "target" };
      var result2 = parser.Parse(input2);
      Assert.That(1, Is.EqualTo(result2.Errors.Count));
      StringAssert.Contains("Option '--source' is required.", result2.Errors[0].Message);

      // Test case 3: valid arguments with optional argument (--bcfVersion)
      var input3 = new List<string> {
        "--source", "sourcePath", "--target", "target", "--bcfVersion",
        "3.0"
      };
      var result3 = parser.Parse(input3);
      Assert.That(0, Is.EqualTo(result3.Errors.Count));

      // Test case 4: missing required argument (--target)
      var input4 = new List<string> { "--source", "sourcePath" };
      var result4 = parser.Parse(input4);
      Assert.That(1, Is.EqualTo(result4.Errors.Count));
      StringAssert.Contains("Option '--target' is required.", result4.Errors[0].Message);
    }

    [Test]
    public void CommandLineArgumentsShortVersionTest() {
      // Arrange
      var rootCommand = BuildCommandLine();

      // Act & Assert
      var parser = new CommandLineBuilder(rootCommand).UseDefaults().Build();

      // Test case 1: valid arguments with default bcfVersion value
      var input1 = new List<string>
        {"-s", "sourcePath", "-t", "target"};
      var result1 = parser.Parse(input1);
      Assert.That(0, Is.EqualTo(result1.Errors.Count));
      var bcfVersionOption = rootCommand.Options.FirstOrDefault(opt => opt.Aliases.Contains("-b"));

      if (bcfVersionOption != null)
        Assert.That("2.1", Is.EqualTo(result1.GetValueForOption(bcfVersionOption)));
      else {
        Assert.Fail();
      }

      // Test case 2: missing required argument (--source)
      var input2 = new List<string> { "-t", "target" };
      var result2 = parser.Parse(input2);
      Assert.That(1, Is.EqualTo(result2.Errors.Count));
      StringAssert.Contains("Option '--source' is required.", result2.Errors[0].Message);

      // Test case 3: valid arguments with optional argument (--bcfVersion)
      var input3 = new List<string> {
        "-s", "sourcePath", "-t", "target", "-b",
        "3.0"
      };
      var result3 = parser.Parse(input3);
      Assert.That(0, Is.EqualTo(result3.Errors.Count));

      // Test case 4: missing required argument (--target)
      var input4 = new List<string> { "-s", "sourcePath" };
      var result4 = parser.Parse(input4);
      Assert.That(1, Is.EqualTo(result4.Errors.Count));
      StringAssert.Contains("Option '--target' is required.", result4.Errors[0].Message);
    }
  }
  private static RootCommand BuildCommandLine() {
    var sourcePathOption = new Option<string>(
      aliases: new[] { "--source", "-s" },
      description: "The absolute path of the source file.") {
      IsRequired = true
    };

    var targetOption = new Option<string>(
      aliases: new[] { "--target", "-t" },
      description: "The absolute path of the target.") {
      IsRequired = true
    };

    var versionOption = new Option<string>(
      aliases: new[] { "--bcfVersion", "-b" },
      description:
      "The target version of the bcf parser, by default 2.1 is used.") {
      IsRequired = false
    };
    versionOption.SetDefaultValue("2.1");

    var rootCommand = new RootCommand {
      Description =
        "A .NET library and a command line utility for converting BCF (Building Collaboration Format) files into json and vice versa. The tool converts BCF information across formats and versions."
    };

    rootCommand.AddOption(sourcePathOption);
    rootCommand.AddOption(targetOption);
    rootCommand.AddOption(versionOption);

    return rootCommand;
  }
}