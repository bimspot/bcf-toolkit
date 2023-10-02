#nullable disable

using System;
using System.Threading.Tasks;
using bcf.Builder;
using bcf.Converter;

namespace bcf;

/// <summary>
///   The `ConverterContext` class defines the converter strategy for a specific
///   version of BCF and performs the conversion according to the chosen type of
///   conversion.
///   Types of the conversion:
///   - BCF to json
///   - json to BCF
///   - model to BCF
/// </summary>
public class ConverterContext {
  /// <summary>
  ///   The converter maintains a reference to one of the Strategy objects.
  ///   It does not know the concrete class of a strategy.
  ///   It should work with all strategies via the converter strategy interface.
  /// </summary>
  private IConverter _converter;

  // private IMarkupBuilder _markupBuilder;

  /// <summary>
  ///   Creates and returns an instance of `ConverterContext`. The version of
  ///   the bcf must be specified here.
  /// </summary>
  /// <param name="version">The version of the BCF.</param>
  public ConverterContext(BcfVersionEnum version) {
    SetVersion(version);
  }

  /// <summary>
  ///   Sets the converter strategy by the specified version.
  /// </summary>
  /// <param name="version">The version of the BCF.</param>
  /// <exception cref="ArgumentException"></exception>
  private void SetVersion(BcfVersionEnum version) {
    switch (version) {
      case BcfVersionEnum.Bcf21:
        SetConverter(new Converter21());
        break;
      case BcfVersionEnum.Bcf30:
        SetConverter(new Converter30());
        break;
      default:
        throw new ArgumentException($"Unsupported BCF version: {version}");
    }
  }

  /// <summary>
  ///   Sets the converter strategy.
  /// </summary>
  /// <param name="converter"></param>
  private void SetConverter(IConverter converter) {
    _converter = converter;
  }

  // /// <summary>
  // ///   Sets the builder strategy.
  // /// </summary>
  // /// <param name="builder"></param>
  // private void SetMarkupBuilder(IMarkupBuilder builder) {
  //   _markupBuilder = builder;
  // }

  /// <summary>
  ///   Converts the specified source data to the given destination.
  /// </summary>
  /// <param name="source">Source path of the file which must be converted.</param>
  /// <param name="target">Target destination for the converted results.</param>
  public async Task Convert(string source, string target) {
    if (source.EndsWith("bcfzip"))
      await _converter.BcfToJson(source, target)!;
    else
      await _converter.JsonToBcf(source, target)!;
  }

  // public Task ToBcf() {
  //   return _converter.ToBcf();
  // }
}