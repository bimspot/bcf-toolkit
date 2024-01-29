#nullable disable

using System;
using System.Threading.Tasks;
using BcfToolkit.Model;
using BcfToolkit.Worker;

namespace BcfToolkit;

/// <summary>
///   The `ConverterContext` class defines the converter strategy for a specific
///   version of BCF and performs the conversion according to the chosen type of
///   conversion.
///   Types of the conversion:
///   - BCF to json
///   - json to BCF
///   - in memory model to BCF
///   - in memory model to JSON
/// </summary>
public class ConverterContext {
  /// <summary>
  ///   The converter maintains a reference to one of the Strategy objects.
  ///   It does not know the concrete class of a strategy.
  ///   It should work with all strategies via the converter strategy interface.
  /// </summary>
  private IConverterWorker ConverterWorker { get; set; }

  //public IBuilder<IMarkup> Builder { get; set; }

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
    ConverterWorker = version switch {
      BcfVersionEnum.Bcf21 => new Worker.Bcf21.ConverterWorker(),
      BcfVersionEnum.Bcf30 => new Worker.Bcf30.ConverterWorker(),
      _ => throw new ArgumentException($"Unsupported BCF version: {version}")
    };
  }

  /// <summary>
  ///   Converts the specified source data to the given destination.
  /// </summary>
  /// <param name="source">
  ///   Source path of the file which must be converted.
  /// </param>
  /// <param name="target">Target destination for the converted results.</param>
  public Task Convert(string source, string target) {
    return source.EndsWith("bcfzip")
      ? ConverterWorker.BcfZipToJson(source, target)!
      : ConverterWorker.JsonToBcfZip(source, target)!;
  }

  /// <summary>
  ///   The method writes the specified BCF models to BCF files.
  /// </summary>
  /// <param name="target">The target path where the BCF is written.</param>
  /// <param name="bcf">The `IBcf` interface of the BCF.
  /// </param>
  /// <returns></returns>
  public Task ToBcf(string target, IBcf bcf) {
    return ConverterWorker.ToBcfZip(target, bcf);
  }

  /// <summary>
  ///   The method writes the specified BCF models to JSON files.
  /// </summary>
  /// <param name="target">The target path where the JSON is written.</param>
  /// <param name="bcf">The `IBcf` interface of the BCF.
  /// </param>
  /// <returns></returns>
  public Task ToJson(string target, IBcf bcf) {
    return ConverterWorker.ToJson(target, bcf);
  }
}