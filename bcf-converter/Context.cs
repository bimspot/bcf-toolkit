using System;
using System.Threading.Tasks;

namespace bcf_converter; 

/// <summary>
/// The Context class defines the converter strategy for a specific version
/// of BCF and performs the conversion according to the chosen type of conversion.
/// Types of the conversion:
/// - BCF to json
/// - json to BCF
/// </summary>
public class Context {
  /// <summary>
  /// The `Context` maintains a reference to one of the Strategy objects.
  /// The `Context` does not know the concrete class of a strategy. It should
  /// work with all strategies via the converter strategy interface.
  /// </summary>
  private IConverter converter;

  /// <summary>
  /// Sets the converter strategy by the specified version.
  /// </summary>
  /// <param name="version">The version of the BCF.</param>
  /// <exception cref="ArgumentException"></exception>
  public void SetVersion(string version) {
    switch (version) {
      case "2.1":
        this.SetConverter(new Converter21());
        break;
      case "3.0":
        this.SetConverter(new Converter30());
        break;
      default:
        throw new ArgumentException($"Unsupported BCF version: {version}");
    }
  }

  /// <summary>
  /// Sets the converter strategy.
  /// </summary>
  /// <param name="converter"></param>
  private void SetConverter(IConverter converter) {
    this.converter = converter;
  }

  /// <summary>
  /// Converts the specified source data to the given destination.
  /// </summary>
  /// <param name="source">Source path of the file which must be converted.</param>
  /// <param name="target">Target destination for the converted results.</param>
  public async Task Convert(string source, string target) {
    if (source.EndsWith("bcfzip")) {
      var markups = await converter.parser().Parse(source);
      await converter.json().WriteJson(markups, target);
    }
    else {
      await converter.json().JsonToBcf(source, target);
    }
  }
}