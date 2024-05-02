#nullable disable

using System;
using System.IO;
using System.Threading.Tasks;
using BcfToolkit.Converter;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf30;
using BcfToolkit.Utils;

namespace BcfToolkit;

/// <summary>
///   The `Worker` class defines the converter strategy for a specific
///   version of BCF and performs the conversion according to the chosen type of
///   conversion.
///   Types of the conversion:
///   - BCF to json
///   - json to BCF
///   - in memory model to BCF
///   - in memory model to JSON
///   - file stream to in memory model
/// </summary>
public class Worker {
  /// <summary>
  ///   The converter maintains a reference to one of the Strategy objects.
  ///   It does not know the concrete class of a strategy.
  ///   It should work with all strategies via the converter strategy interface.
  /// </summary>
  private IConverter _converter { get; set; }

  /// <summary>
  ///   Creates and returns a default instance of `Worker`.
  /// </summary>
  public Worker() { }
  
  /// <summary>
  ///   Creates and returns an instance of `Worker` with the specified version.
  /// </summary>
  public Worker(BcfVersionEnum version) {
    Init(version);
  }

  /// <summary>
  ///   Sets the converter strategy by the specified version.
  /// </summary>
  /// <param name="version">The version of the BCF.</param>
  /// <exception cref="ArgumentException"></exception>
  private void Init(BcfVersionEnum? version) {
    _converter = version switch {
      BcfVersionEnum.Bcf21 => new Converter.Bcf21.Converter(),
      BcfVersionEnum.Bcf30 => new Converter.Bcf30.Converter(),
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
      ? _converter.BcfZipToJson(source, target)!
      : _converter.JsonToBcfZip(source, target)!;

  }

  /// <summary>
  ///   The method writes the specified BCF models to BCF files.
  /// </summary>
  /// 
  /// <param name="bcf">The `IBcf` interface of the BCF.</param>
  /// <param name="target">The target path where the BCF is written.</param>
  /// <returns></returns>
  public Task ToBcfZip(IBcf bcf, string target) {
    return _converter?.ToBcfZip(bcf, target);
  }

  /// <summary>
  ///   The method writes the specified BCF models to JSON files.
  /// </summary>
  /// <param name="bcf">The `IBcf` interface of the BCF.</param>
  /// <param name="target">The target path where the JSON is written.</param>
  /// <returns></returns>
  public Task ToJson(IBcf bcf, string target) {
    return _converter?.ToJson(bcf, target);
  }

  /// <summary>
  ///   The method builds and returns an instance of BCF 3.0 object from the
  ///   specified file stream, independently the input version.
  /// </summary>
  /// <param name="stream"></param>
  /// <returns></returns>
  public async Task<Bcf> BuildBcfFromStream(Stream stream) {
    var currentVersion = await BcfExtensions.GetVersion(stream);
    Init(currentVersion);
    return await _converter.BuildBcfFromStream<Bcf>(stream);
  }
}