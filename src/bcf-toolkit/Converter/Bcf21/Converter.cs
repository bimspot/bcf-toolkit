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
  private readonly Dictionary<BcfVersionEnum, Func<Bcf, IBcf>> _converterFnMapper =
    new() {
      [BcfVersionEnum.Bcf21] = b => b,
      [BcfVersionEnum.Bcf30] = SchemaConverterToBcf30.Convert
    };

  /// <summary>
  ///   Defines the file writer function which must be used for write the BCF
  ///   object to the targeted version.
  /// </summary>
  private readonly Dictionary<BcfVersionEnum, Func<IBcf, string, bool, Task<string>>> _writerFnMapper =
    new() {
      [BcfVersionEnum.Bcf21] = FileWriter.WriteBcf,
      [BcfVersionEnum.Bcf30] = Bcf30.FileWriter.WriteBcf
    };

  public async Task BcfZipToJson(Stream source, string targetPath) {
    var bcf = await _builder.BuildFromStream(source);
    await FileWriter.WriteJson(bcf, targetPath);
  }

  public async Task BcfZipToJson(string sourcePath, string targetPath) {
    try {
      await using var fileStream =
        new FileStream(sourcePath, FileMode.Open, FileAccess.Read);
      await BcfZipToJson(fileStream, targetPath);
    }
    catch (Exception ex) {
      throw new ArgumentException($"Source path is not readable. {ex.Message}", ex);
    }
  }

  public async Task JsonToBcfZip(string source, string target) {
    // Project is optional
    var projectPath = $"{source}/project.json";
    var project = Path.Exists(projectPath)
      ? await JsonExtensions.ParseObject<ProjectExtension>(projectPath)
      : new ProjectExtension();

    var markups = await JsonExtensions.ParseMarkups<Markup>(source);

    var bcf = new Bcf {
      Markups = markups,
      Project = project,
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