using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BcfToolkit.Builder.Bcf30;
using BcfToolkit.Utils;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf30;
using File = System.IO.File;
using Version = BcfToolkit.Model.Bcf30.Version;

namespace BcfToolkit.Converter.Bcf30;

/// <summary>
///   Converter strategy class for converting BCF 3.0 files to JSON
///   and back.
/// </summary>
public class Converter : IConverter {

  private BcfBuilder _builder = new();

  /// <summary>
  ///   Defines the converter function, which must be used for converting the
  ///   BCF object to the targeted version.
  /// </summary>
  private readonly Dictionary<BcfVersionEnum, Func<IBcf, IBcf>> _converterFnMapper = new();

  /// <summary>
  ///   Defines the file writer function which must be used for write the BCF
  ///   object to the targeted version.
  /// </summary>
  private readonly Dictionary<BcfVersionEnum, Func<IBcf, string, bool, Task<string>>> _writerFnMapper = new();

  public Converter() {
    _converterFnMapper[BcfVersionEnum.Bcf21] = SchemaConverterToBcf21.Convert;
    _converterFnMapper[BcfVersionEnum.Bcf30] = b => b;

    _writerFnMapper[BcfVersionEnum.Bcf21] = Bcf21.FileWriter.WriteBcf;
    _writerFnMapper[BcfVersionEnum.Bcf30] = FileWriter.WriteBcf;
  }

  public async Task BcfZipToJson(Stream source, string target) {
    var builder = new BcfBuilder();
    var bcf = await builder.BuildFromStream(source);
    await FileWriter.WriteJson(bcf, target);
  }

  public async Task BcfZipToJson(string source, string target) {
    await using var fileStream = new FileStream(source, FileMode.Open, FileAccess.Read);
    await BcfZipToJson(fileStream, target);
  }

  public async Task JsonToBcfZip(string source, string target) {
    // Project and DocumentInfo are optional
    var extensions =
      await JsonExtensions.ParseObject<Extensions>($"{source}/extensions.json");
    var project =
      await JsonExtensions.ParseObject<ProjectInfo>($"{source}/project.json");
    var documents =
      await JsonExtensions.ParseObject<DocumentInfo>($"{source}/documents.json");
    var markups = await JsonExtensions.ParseMarkups<Markup>(source);

    var bcf = new Bcf {
      Markups = markups,
      Extensions = extensions,
      Project = project,
      Document = documents,
      Version = new Version()
    };

    await FileWriter.WriteBcf(bcf, target);
  }

  public async Task<Stream> ToBcfStream(IBcf bcf, BcfVersionEnum targetVersion) {
    var converterFn = _converterFnMapper[targetVersion];
    var convertedBcf = converterFn((Bcf)bcf);

    var workingDir = Directory.GetCurrentDirectory();
    var tmpBcfTargetPath = workingDir + "/bcf.bcfzip";
    var writerFn = _writerFnMapper[targetVersion];

    // keep the tmp files till the stream is created
    var tmpFolder = await writerFn(convertedBcf, tmpBcfTargetPath, false);
    var stream = new FileStream(tmpBcfTargetPath, FileMode.Open, FileAccess.Read);

    Directory.Delete(tmpFolder, true);
    File.Delete(tmpBcfTargetPath);

    return stream;
  }

  public Task ToBcfZip(IBcf bcf, string target) {
    return FileWriter.WriteBcf((Bcf)bcf, target);
  }

  public Task ToJson(IBcf bcf, string target) {
    return FileWriter.WriteJson((Bcf)bcf, target);
  }

  public async Task<T> BuildBcfFromStream<T>(Stream stream) {
    var bcf = await _builder.BuildFromStream(stream);
    var targetVersion = BcfVersion.TryParse(typeof(T));
    var converterFn = _converterFnMapper[targetVersion];
    return (T)converterFn(bcf);
  }
}