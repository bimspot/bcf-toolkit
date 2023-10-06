#nullable disable

using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using BcfConverter.Builder;
using BcfConverter.Model;

namespace BcfConverter.Converter;

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
  private IConverter Converter { get; set; }

  private IBuilder<IMarkup> Builder { get; set; }

  // private IMarkupBuilder _markupBuilder;

  /// <summary>
  ///   Creates and returns an instance of `ConverterContext`. The version of
  ///   the bcf must be specified here.
  /// </summary>
  /// <param name="version">The version of the BCF.</param>
  public ConverterContext(BcfVersionEnum version) {
    Init(version);
  }

  /// <summary>
  ///   Sets the converter strategy by the specified version.
  /// </summary>
  /// <param name="version">The version of the BCF.</param>
  /// <exception cref="ArgumentException"></exception>
  private void Init(BcfVersionEnum version) {
    switch (version) {
      case BcfVersionEnum.Bcf21:
        Converter = new Converter.Bcf21.Converter();
        Builder = new Builder.Bcf21.MarkupBuilder();
        break;
      case BcfVersionEnum.Bcf30:
        Converter = new Converter.Bcf30.Converter();
        Builder = new Builder.Bcf30.MarkupBuilder();
        break;
      default:
        throw new ArgumentException($"Unsupported BCF version: {version}");
    }
  }

  /// <summary>
  ///   Converts the specified source data to the given destination.
  /// </summary>
  /// <param name="source">Source path of the file which must be converted.</param>
  /// <param name="target">Target destination for the converted results.</param>
  public async Task Convert(string source, string target) {
    if (source.EndsWith("bcfzip"))
      await Converter.BcfToJson(source, target)!;
    else
      await Converter.JsonToBcf(source, target)!;
  }

  internal Task ToBcf(string target, ConcurrentBag<IMarkup> markups) {
    return Converter.ToBcf(target, markups);
  }
}