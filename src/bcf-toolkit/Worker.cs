#nullable disable

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using BcfToolkit.Converter;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf30;
using BcfToolkit.Model.Interfaces;
using BcfToolkit.Utils;

namespace BcfToolkit;

/// <summary>
///   The `Worker` class defines the converter strategy for a specific
///   version of BCF and performs the conversion according to the chosen type of
///   conversion.
///   Types of the conversions:
///   - BCF to json
///   - json to BCF
///   - in-memory model to BCF
///   - in-memory model to JSON
///   - in-memory model to file stream
///   - file stream to in-memory model
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
  ///   Initializes the converter from the file path of the BCF zip archive.
  /// </summary>
  /// <param name="source">The path of the source file.</param>
  private async Task InitConverterFromArchive(string source) {
    await using var stream =
      new FileStream(source, FileMode.Open, FileAccess.Read);
    await InitConverterFromStreamArchive(stream);
  }

  /// <summary>
  ///   Initializes the converter from the file stream of the BCF zip archive.
  /// </summary>
  /// <param name="stream">The stream of the source BCF zip archive.</param>
  private async Task InitConverterFromStreamArchive(Stream stream) {
    var version =
      await BcfExtensions.GetVersionFromStreamArchive(stream);
    InitConverter(version);
  }

  /// <summary>
  ///   Initializes the converter from the json of the BCF content.
  /// </summary>
  /// <param name="source">The json of the BCF content.</param>
  private async Task InitConverterFromJson(string source) {
    var version =
      await JsonExtensions.GetVersionFromJson(source);
    InitConverter(version);
  }

  /// <summary>
  ///   Initializes the converter from an instance of `Bcf` object.
  /// </summary>
  /// <param name="bcf">Instance of `Bcf` object.</param>
  private void InitConverterFromType(IBcf bcf) {
    var version = BcfVersion.TryParse(bcf);
    InitConverter(version);
  }

  /// <summary>
  ///   Sets the converter strategy by the specified version.
  /// </summary>
  /// <param name="version">The version of the BCF.</param>
  /// <exception cref="ArgumentException"></exception>
  private void InitConverter(BcfVersionEnum? version) {
    _converter ??= version switch {
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
  public async Task Convert(string source, string target) {
    if (source.EndsWith("bcfzip")) {
      await InitConverterFromArchive(source);
      await _converter.BcfToJson(source, target);
    }
    else {
      await InitConverterFromJson(source);
      await _converter.JsonToBcf(source, target);
    }
  }

  /// <summary>
  ///   The method writes the specified BCF model to BCF file.
  /// </summary>
  /// <param name="bcf">The `IBcf` interface of the BCF.</param>
  /// <param name="target">The target path where the BCF is written.</param>
  /// <returns></returns>
  public Task ToBcf(IBcf bcf, string target) {
    InitConverterFromType(bcf);
    return _converter.ToBcf(bcf, target);
  }

  /// <summary>
  ///   The method writes the specified BCF model to JSON files.
  /// </summary>
  /// <param name="bcf">The `IBcf` interface of the BCF.</param>
  /// <param name="target">The target path where the JSON is written.</param>
  /// <returns></returns>
  public Task ToJson(IBcf bcf, string target) {
    InitConverterFromType(bcf);
    return _converter.ToJson(bcf, target);
  }

  /// <summary>
  ///   The method builds and returns an instance of the latest (BCF 3.0) object
  ///   from the specified file stream, independently the input version. It
  ///   converts the BCF from the given version to the latest.
  /// </summary>
  /// <param name="stream">The stream of the source BCF zip archive.</param>
  /// <returns>Returns the in-memory BCF model.</returns>
  public async Task<Bcf> BcfFromStream(Stream stream) {
    await InitConverterFromStreamArchive(stream);
    return await _converter.BcfFromStream<Bcf>(stream);
  }

  /// <summary>
  ///   The method converts the specified BCF object to the given version, then
  ///   returns a stream from the BCF zip archive. It saves the bcf files
  ///   locally into a tmp folder.
  ///
  ///   IMPORTANT: It is the user's responsibility to dispose of the stream
  ///   after use. Failure to do so may result in resource leaks and potential
  ///   application instability.
  /// </summary>
  /// <param name="bcf">The BCF object.</param>
  /// <param name="targetVersion">The target BCF version.</param>
  /// <returns>
  ///   Returns the file stream of the BCF zip archive.
  /// </returns>
  public async Task<Stream> ToBcf(IBcf bcf, BcfVersionEnum targetVersion) {
    InitConverterFromType(bcf);
    return await _converter.ToBcf(bcf, targetVersion);
  }

  /// <summary>
  ///   The method converts the specified BCF object to the given version with
  ///   cancellation token, then returns a stream from the BCF zip archive. It
  ///   saves the bcf files locally into a tmp folder.
  ///
  ///   IMPORTANT: It is the user's responsibility to dispose of the stream
  ///   after use. Failure to do so may result in resource leaks and potential
  ///   application instability.
  /// </summary>
  /// <param name="bcf">The BCF object.</param>
  /// <param name="targetVersion">The target BCF version.</param>
  /// <param name="cancellationToken">
  ///   A token to monitor for cancellation requests.
  /// </param>
  /// <returns>
  ///   Returns the file stream of the BCF zip archive.
  /// </returns>
  public async Task<Stream> ToBcf(
    IBcf bcf,
    BcfVersionEnum targetVersion,
    CancellationToken cancellationToken) {
    InitConverterFromType(bcf);
    return await _converter.ToBcf(bcf, targetVersion, cancellationToken);
  }

  /// <summary>
  ///   The method converts the given BCF object to the given version, then
  ///   writes the BCF zip archive to the specified stream.
  ///
  ///   When the stream based approach is used, there is no parallel execution
  ///   and there is less compression, as zip entities are compressed
  ///   individually. 
  /// </summary>
  /// <param name="bcf">The BCF object.</param>
  /// <param name="targetVersion">The target BCF version.</param>
  /// <param name="stream">The output stream, which should be writable.</param>
  public void ToBcf(
    IBcf bcf,
    BcfVersionEnum targetVersion,
    Stream stream) {
    InitConverterFromType(bcf);
    _converter.ToBcf(bcf, targetVersion, stream);
  }

  /// <summary>
  ///   The method converts the given BCF object to the given version, with
  ///   cancellation token, then writes the BCF zip archive to the specified
  ///   stream.
  ///
  ///   When the stream based approach is used, there is no parallel execution
  ///   and there is less compression, as zip entities are compressed
  ///   individually. 
  /// </summary>
  /// <param name="bcf">The BCF object.</param>
  /// <param name="targetVersion">The target BCF version.</param>
  /// <param name="stream">The output stream, which should be writable.</param>
  /// <param name="cancellationToken">
  ///   A token to monitor for cancellation requests.
  /// </param>
  /// <returns>
  ///   Returns the file stream of the BCF zip archive.
  /// </returns>
  public void ToBcf(
    IBcf bcf,
    BcfVersionEnum targetVersion,
    Stream stream,
    CancellationToken cancellationToken) {
    InitConverterFromType(bcf);
    _converter.ToBcf(bcf, targetVersion, stream, cancellationToken);
  }
}