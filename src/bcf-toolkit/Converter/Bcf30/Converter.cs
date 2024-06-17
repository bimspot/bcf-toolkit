using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;
using BcfToolkit.Builder.Bcf30;
using BcfToolkit.Utils;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf30;
using Version = BcfToolkit.Model.Bcf30.Version;

namespace BcfToolkit.Converter.Bcf30;

/// <summary>
///   Converter strategy class for converting BCF 3.0 files to JSON
///   and back.
/// </summary>
public class Converter : IConverter {
  private readonly BcfBuilder _builder = new();

  /// <summary>
  ///   Defines the converter function, which must be used for converting the
  ///   BCF object to the targeted version.
  /// </summary>
  private readonly Dictionary<BcfVersionEnum, Func<Bcf, IBcf>> _converterFn =
    new() {
      [BcfVersionEnum.Bcf21] = SchemaConverterToBcf21.Convert,
      [BcfVersionEnum.Bcf30] = b => b
    };

  /// <summary>
  ///   Defines the file writer function which must be used for write the BCF
  ///   object to the targeted version.
  /// </summary>
  private readonly Dictionary<BcfVersionEnum,
      Func<IBcf, CancellationToken?, Task<Stream>>>
    _fileWriterFn =
      new() {
        [BcfVersionEnum.Bcf21] = Bcf21.FileWriter.SerializeAndWriteBcf,
        [BcfVersionEnum.Bcf30] = FileWriter.SerializeAndWriteBcf
      };

  /// <summary>
  ///   Defines the stream writer function which must be used for write the BCF
  ///   object to the targeted version.
  /// </summary>
  private readonly Dictionary<BcfVersionEnum,
      Action<IBcf, ZipArchive, CancellationToken?>>
    _streamWriterFn =
      new() {
        [BcfVersionEnum.Bcf21] = Bcf21.FileWriter.SerializeAndWriteBcfToStream,
        [BcfVersionEnum.Bcf30] = FileWriter.SerializeAndWriteBcfToStream
      };

  public async Task BcfToJson(Stream source, string target) {
    var builder = new BcfBuilder();
    var bcf = await builder.BuildInMemoryFromStream(source);
    await FileWriter.WriteJson(bcf, target);
  }

  public async Task BcfToJson(string source, string target) {
    await using var fileStream =
      new FileStream(source, FileMode.Open, FileAccess.Read);
    await BcfToJson(fileStream, target);
  }

  public async Task JsonToBcf(string source, string target) {
    // Project and DocumentInfo are optional
    var extensions =
      await JsonExtensions.ParseObject<Extensions>($"{source}/extensions.json");
    var project =
      await JsonExtensions.ParseObject<ProjectInfo>($"{source}/project.json");
    var documents =
      await JsonExtensions.ParseObject<DocumentInfo>(
        $"{source}/documents.json");
    var markups = await JsonExtensions.ParseMarkups<Markup>(source);

    var bcf = new Bcf {
      Markups = markups,
      Extensions = extensions,
      Project = project,
      Document = documents,
      Version = new Version()
    };

    await FileWriter.SerializeAndWriteBcfToFolder(bcf, target);
  }

  public async Task<Stream> ToBcf(IBcf bcf, BcfVersionEnum targetVersion) {
    return await this.ToBcf(bcf: bcf, targetVersion: targetVersion,
      cancellationToken: null);
  }

  public async Task<Stream> ToBcf(IBcf bcf, BcfVersionEnum targetVersion,
    CancellationToken? cancellationToken) {
    var converterFn = _converterFn[targetVersion];
    var convertedBcf = converterFn((Bcf)bcf);

    var writerFn = _fileWriterFn[targetVersion];
    return await writerFn(convertedBcf, cancellationToken);
  }

  public void ToBcf(IBcf bcf, BcfVersionEnum targetVersion, Stream stream) {
    this.ToBcf(bcf: bcf, targetVersion: targetVersion, stream: stream,
      cancellationToken: null);
  }

  public void ToBcf(IBcf bcf, BcfVersionEnum targetVersion, Stream stream,
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