using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace bcf.Converter; 

public static class JsonConverter {
  /// <summary>
  ///   
  /// </summary>
  /// <param name="sourceFolder"></param>
  /// <param name="targetFile"></param>
  /// <returns></returns>
  /// <exception cref="ApplicationException"></exception>
  public static Task JsonToBcf<TMarkup, TVersion>(string sourceFolder, string targetFile) where TVersion : new() {
    return Task.Run(async () => {
      var targetFolder = Path.GetDirectoryName(targetFile);

      if (targetFolder == null) {
        throw new ApplicationException(
          "Target folder not found ${targetFolder}");
      }

      // Will create a tmp folder for the intermediate files.
      var tmpFolder = $"{targetFolder}/tmp";
      if (Directory.Exists(tmpFolder)) {
        Directory.Delete(tmpFolder, true);
      }
      Directory.CreateDirectory(tmpFolder);
        
      // Create the version file
      var version = new TVersion();
      await using var versionWriter =
        File.CreateText($"{tmpFolder}/bcf.version");
      new XmlSerializer(typeof(TVersion)).Serialize(versionWriter, version);

      var files = new List<string>(Directory.EnumerateFiles(sourceFolder));

      var contractResolver = new DefaultContractResolver {
        NamingStrategy = new SnakeCaseNamingStrategy()
      };
      var jsonSerializer = new JsonSerializer {
        NullValueHandling = NullValueHandling.Ignore,
        ContractResolver = contractResolver
      };
      foreach (var file in files) {
        if (file.EndsWith("json") == false) {
          Console.WriteLine($" - File is not json, skipping ${file}");
          continue;
        }
        Console.WriteLine($" - Processing {file}");

        using var json = File.OpenText(file);
        var markup =
          (IMarkup)jsonSerializer.Deserialize(json, typeof(TMarkup));

        // Creating the target folder
        if (markup.GetTopic()?.Guid == null) {
          Console.WriteLine(
            $" - Topic Guid is missing, skipping {file}");
          continue;
        }
        var guid = markup.GetTopic()?.Guid;
        var topicFolder = $"{tmpFolder}/{guid}";
        Directory.CreateDirectory(topicFolder);

        // Markup
        await using var markupWriter =
          File.CreateText($"{topicFolder}/markup.bcf");
        new XmlSerializer(typeof(TMarkup)).Serialize(markupWriter, markup);

        // Viewpoint
        // await using var viewpointWriter =
        //   File.CreateText($"{topicFolder}/viewpoint.bcfv");
        // new XmlSerializer(typeof(VisualizationInfo)).Serialize(
        //   viewpointWriter,
        //   markup.Viewpoints.First().VisualizationInfo);

        // Snapshot
        // var snapshotFileName = markup.Viewpoints.First().Snapshot;
        // var base64String = markup.Viewpoints.First().SnapshotData;
        // if (snapshotFileName == null || base64String == null) continue;
        // var result = Regex.Replace(base64String,
        //   @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
        // await File.WriteAllBytesAsync(
        //   $"{topicFolder}/{snapshotFileName}",
        //   Convert.FromBase64String(result));
      }

      // zip shit
      Console.WriteLine($"Zipping the output: {targetFile}");
      if (File.Exists(targetFile)) {
        File.Delete(targetFile);
      }
      ZipFile.CreateFromDirectory(tmpFolder, targetFile);
      Directory.Delete(tmpFolder, true);
    });
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="markups"></param>
  /// <param name="targetFolder"></param>
  /// <returns></returns>
  public static Task WriteMarkupsJson<TMarkup>(ConcurrentBag<TMarkup> markups, string targetFolder) {
    return Task.Run(async () => {
      // TODO make a default serializer to avoid code repeat
      var contractResolver = new DefaultContractResolver {
        NamingStrategy = new SnakeCaseNamingStrategy()
      };
      var jsonSerializerSettings = new JsonSerializerSettings {
        NullValueHandling = NullValueHandling.Ignore,
        ContractResolver = contractResolver
      };

      // Creating the target folder
      if (Directory.Exists(targetFolder)) {
        Directory.Delete(targetFolder, true);
      }
      Directory.CreateDirectory(targetFolder);

      // Writing to disk, one markup per file.
      foreach (var markup in markups) {
        var json = JsonConvert
          .SerializeObject(
            markup,
            Formatting.None, 
            jsonSerializerSettings);

        var path = $"{targetFolder}/{((IMarkup)markup!).GetTopic().Guid}.json";
        await using var writer = File.CreateText(path);
        await writer.WriteAsync(json);
      }
    });
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="obj"></param>
  /// <param name="targetFolder"></param>
  /// <returns></returns>
  public static Task WriteBcfRootsJson(object obj, string targetFolder) {
    return Task.Run(async () => {
      // TODO make a default serializer to avoid code repeat
      var contractResolver = new DefaultContractResolver {
        NamingStrategy = new SnakeCaseNamingStrategy()
      };
      var jsonSerializerSettings = new JsonSerializerSettings {
        NullValueHandling = NullValueHandling.Ignore,
        ContractResolver = contractResolver
      };

      // Creating the target folder
      if (Directory.Exists(targetFolder)) {
        Directory.Delete(targetFolder, true);
      }
      Directory.CreateDirectory(targetFolder);
      
      var json = JsonConvert
        .SerializeObject(
          obj,
          Formatting.None, jsonSerializerSettings);
      var path = $"{targetFolder}/bcfRoots.json";
      await using var writer = File.CreateText(path);
      await writer.WriteAsync(json);
    });
  }
}