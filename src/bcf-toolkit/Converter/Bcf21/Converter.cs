using System;
using System.Collections.Generic;
using System.IO;
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
  private readonly Dictionary<BcfVersionEnum, Func<Bcf, IBcf>> _converterFnMapper = new();

  /// <summary>
  ///   Defines the file writer function which must be used for write the BCF
  ///   object to the targeted version.
  /// </summary>
  private readonly Dictionary<BcfVersionEnum, Func<IBcf, string, bool, Task<string>>> _writerFnMapper = new();

  public Converter() {
    _converterFnMapper[BcfVersionEnum.Bcf21] = b => b;
    _converterFnMapper[BcfVersionEnum.Bcf30] = SchemaConverterToBcf30.Convert;

    _writerFnMapper[BcfVersionEnum.Bcf21] = FileWriter.WriteBcf;
    _writerFnMapper[BcfVersionEnum.Bcf30] = Bcf30.FileWriter.WriteBcf;
  }

  public async Task BcfZipToJson(Stream source, string target) {
    var bcf = await _builder.BuildFromStream(source);

    // Writing json files
    await FileWriter.WriteJson(bcf, target);
  }

  public async Task BcfZipToJson(string sourcePath, string target) {
    try {
      await using var fileStream =
        new FileStream(sourcePath, FileMode.Open, FileAccess.Read);
      await BcfZipToJson(fileStream, target);
    }
    catch (Exception ex) {
      throw new ArgumentException($"Source path is not readable. {ex.Message}", ex);
    }
  }

  public async Task JsonToBcfZip(string source, string target) {
    // Parsing BCF project - it is an optional file
    var projectPath = $"{source}/project.json";
    var project = Path.Exists(projectPath)
      ? await JsonExtensions.ParseObject<ProjectExtension>(projectPath)
      : new ProjectExtension();

    // Parsing markups
    var markups = await JsonExtensions.ParseMarkups<Markup>(source);

    var bcf = new Bcf {
      Markups = markups,
      Project = project,
      Version = new Version()
    };

    // Writing bcf files
    await FileWriter.WriteBcf(bcf, target);
  }

  public async Task<Stream> ToBcfStream(IBcf bcf, BcfVersionEnum targetVersion) {
    // Convert the bcf to the target version
    var converterFn = _converterFnMapper[targetVersion];
    var convertedBcf = converterFn((Bcf)bcf);

    // Write the converted bcf to tmp and create file stream
    var workingDir = Directory.GetCurrentDirectory();
    var bcfTargetPath = workingDir + "/bcf.bcfzip";
    var writerFn = _writerFnMapper[targetVersion];
    var tmpFolder = await writerFn(convertedBcf, bcfTargetPath, false);
    var stream = new FileStream(bcfTargetPath, FileMode.Open, FileAccess.Read);

    // After the stream is ready the folders should be deleted
    Directory.Delete(tmpFolder, true);
    File.Delete(bcfTargetPath);

    return stream;
  }

  public Task ToBcfZip(IBcf bcf, string target) {
    return FileWriter.WriteBcf((Bcf)bcf, target);
  }

  public Task ToJson(IBcf bcf, string target) {
    return FileWriter.WriteJson((Bcf)bcf, target);
  }

  public async Task<T> BuildBcfFromStream<T>(Stream stream) {
    // Build the bcf from stream
    var bcf = await _builder.BuildFromStream(stream);

    // Convert the bcf to the specified type
    var targetVersion = BcfVersion.TryParse(typeof(T));
    var converterFn = _converterFnMapper[targetVersion];
    return (T)converterFn(bcf);
  }
}