using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BcfToolkit.Builder.Bcf21;
using BcfToolkit.Utils;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf21;
using Version = BcfToolkit.Model.Bcf21.Version;

namespace BcfToolkit.Converter.Bcf21;

/// <summary>
///   Converter strategy class for converting BCF 2.1 to different versions,
///   JSON, and BCFzip.
/// </summary>
public class Converter : IConverter {
  private BcfBuilder _builder = new();

  /// <summary>
  ///   Defines the converter function, which must be used for converting the
  ///   BCF object to the targeted version.
  /// </summary>
  private readonly Dictionary<BcfVersionEnum, Func<Bcf, IBcf>> _converterFn =
    new() {
      [BcfVersionEnum.Bcf21] = b => b,
      [BcfVersionEnum.Bcf30] = SchemaConverterToBcf30.Convert
    };

  /// <summary>
  ///   Defines the file writer function which must be used for write the BCF
  ///   object to the targeted version.
  /// </summary>
  private readonly Dictionary<
      BcfVersionEnum,
      Func<IBcf, CancellationToken?, Task<Stream>>>
    _writerFn =
      new() {
        [BcfVersionEnum.Bcf21] = FileWriter.SerializeAndWriteBcf,
        [BcfVersionEnum.Bcf30] = Bcf30.FileWriter.SerializeAndWriteBcf
      };
  
  /// <summary>
  ///   Defines the stream writer function which must be used for write the BCF
  ///   object to the targeted version.
  /// </summary>
  private readonly Dictionary<
      BcfVersionEnum,
      Action<IBcf, ZipArchive, CancellationToken?>>
    _streamWriterFn =
      new() {
        [BcfVersionEnum.Bcf21] = FileWriter.SerializeAndWriteBcfToStream,
        [BcfVersionEnum.Bcf30] = Bcf30.FileWriter.SerializeAndWriteBcfToStream
      };

  public async Task BcfToJson(Stream source, string targetPath) {
    var bcf = await _builder.BuildInMemoryFromStream(source);
    await FileWriter.WriteJson(bcf, targetPath);
  }

  public async Task BcfToJson(string sourcePath, string targetPath) {
    try {
      await using var fileStream =
        new FileStream(sourcePath, FileMode.Open, FileAccess.Read);
      await BcfToJson(fileStream, targetPath);
    }
    catch (Exception ex) {
      throw new ArgumentException($"Source path is not readable. {ex.Message}",
        ex);
    }
  }

  public async Task JsonToBcf(string source, string target) {
    // Project is optional
    var projectPath = $"{source}/project.json";
    var project = Path.Exists(projectPath)
      ? await JsonExtensions.ParseObject<ProjectExtension>(projectPath)
      : null;

    var markups = await JsonExtensions.ParseMarkups<Markup>(source);

    var bcf = _builder
      .AddMarkups(markups.ToList())
      .SetProject(project)
      .Build();

    await FileWriter.SerializeAndWriteBcfToFolder(bcf, target);
  }

  public async Task<Stream> ToBcf(IBcf bcf, BcfVersionEnum targetVersion) {
    return await this.ToBcf(
      bcf: bcf,
      targetVersion: targetVersion,
      cancellationToken: null);
  }

  public async Task<Stream> ToBcf(
    IBcf bcf,
    BcfVersionEnum targetVersion,
    CancellationToken? cancellationToken) {
    var converterFn = _converterFn[targetVersion];
    var convertedBcf = converterFn((Bcf)bcf);

    if (cancellationToken is { IsCancellationRequested: true }) {
      return Stream.Null;
    }

    var writerFn = _writerFn[targetVersion];
    return await writerFn(convertedBcf, cancellationToken);
  }

  public void ToBcf(IBcf bcf, BcfVersionEnum targetVersion, Stream stream) {
    this.ToBcf(bcf, targetVersion, stream, null);
  }

  public void ToBcf(
    IBcf bcf,
    BcfVersionEnum targetVersion,
    Stream stream,
    CancellationToken? cancellationToken) {
    if (!stream.CanWrite) {
      throw new ArgumentException("Stream is not writable.");
    }

    var converterFn = _converterFn[targetVersion];
    var convertedBcf = converterFn((Bcf)bcf);

    if (cancellationToken is { IsCancellationRequested: true }) {
      return;
    }

    var writerFn = _streamWriterFn[targetVersion];
    var zip = new ZipArchive(stream, ZipArchiveMode.Create, true);
    writerFn(convertedBcf, zip, cancellationToken);
  }

  public Task ToBcf(IBcf bcf, string target) {
    return FileWriter.SerializeAndWriteBcfToFolder((Bcf)bcf, target);
  }

  public Task ToJson(IBcf bcf, string target) {
    return FileWriter.WriteJson((Bcf)bcf, target);
  }
  
  public async Task<T> BcfFromStream<T>(Stream stream) {
    var bcf = await _builder.BuildInMemoryFromStream(stream);
    var targetVersion = BcfVersion.TryParse(typeof(T));
    var converterFn = _converterFn[targetVersion];
    return (T)converterFn(bcf);
  }
}