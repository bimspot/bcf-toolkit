using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf21;

namespace BcfToolkit.Converter.Bcf21;

/// <summary>
///   Converter strategy class for converting BCF 2.1 files to JSON
///   and back.
/// </summary>
public class Converter : IConverter {
  /// <summary>
  ///   The method parses the BCF file of version 2.1 and writes into JSON.
  ///   The root of the BCF zip contains the following files:
  ///   - project.bcfp (optional)
  ///   - bcf.version
  ///   Topic folder structure inside a BCFzip archive:
  ///   - markup.bcf
  ///   Additionally:
  ///   - Viewpoint files (BCFV)
  ///   - Snapshot files (PNG/JPEG)
  ///   - Bitmaps
  /// </summary>
  /// <param name="source">The source path to the BCFzip.</param>
  /// <param name="target">The target path where the JSON is written.</param>
  public async Task BcfToJson(string source, string target) {
    // Parsing BCF root file structure
    var project = await BcfConverter.ParseProject<ProjectExtension>(source);
    var root = new Root {
      Project = project
    };

    // Parsing topics folder (markups)
    var markups =
      await BcfConverter.ParseMarkups<Markup, VisualizationInfo>(source);

    // Writing json files
    await JsonConverter.WriteJson(target, markups, root);
  }

  /// <summary>
  ///   The method reads the JSON files and creates BCF 2.1 version.
  ///   The json folder must contain files which are named using the
  ///   `uuid` of the `Topic` within, and `bcfRoot.json`.
  /// </summary>
  /// <param name="source">The source folder to the JSON files.</param>
  /// <param name="target">The target path where the BCF is written.</param>
  public async Task JsonToBcf(string source, string target) {
    // Parsing BCF root - it is an optional file
    var rootPath = $"{source}/bcfRoot.json";
    var root = Path.Exists(rootPath)
      ? await JsonConverter.ParseObject<Root>(rootPath)
      : new Root();

    // Parsing markups
    var markups = await JsonConverter.ParseMarkups<Markup>(source);

    // Writing bcf files
    await BcfConverter.WriteBcf<Markup, VisualizationInfo, Root, Version>(
      target, markups, root);
  }

  /// <summary>
  ///   The method writes the specified BCF 2.1 models to BCF 2.1 files.
  /// </summary>
  /// <param name="target">The target path where the BCF is written.</param>
  /// <param name="markups">Array of `IMarkup` interface objects.</param>
  /// <param name="root">The `IRoot` interface of the BCF, it contains all the root info.</param>
  /// <returns></returns>
  public async Task ToBcf(
    string target,
    ConcurrentBag<IMarkup> markups,
    IRoot root) {
    var convertedMarkups =
      new ConcurrentBag<Markup>(markups.Select(m => (Markup)m));
    var convertedRoot = (Root)root;
    await BcfConverter.WriteBcf<Markup, VisualizationInfo, Root, Version>(
      target, convertedMarkups, convertedRoot);
  }

  /// <summary>
  ///   The method writes the specified BCF 2.1 models to JSON files.
  /// </summary>
  /// <param name="target">The target path where the JSON is written.</param>
  /// <param name="markups">Array of `IMarkup` interface objects.</param>
  /// <param name="root">The `IRoot` interface of the BCF, it contains all the root info.</param>
  /// <returns></returns>
  public async Task ToJson(
    string target,
    ConcurrentBag<IMarkup> markups,
    IRoot root) {
    var convertedMarkups =
      new ConcurrentBag<Markup>(markups.Select(m => (Markup)m));
    var convertedRoot = (Root)root;
    await JsonConverter.WriteJson(target, convertedMarkups, convertedRoot);
  }
}