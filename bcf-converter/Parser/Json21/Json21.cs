using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using bcf;
using Newtonsoft.Json;
using Version = bcf.Version;

namespace bcf_converter.Parser.Json21 {
  public class Json21 : IJsonToBcfConverter {
    /// <summary>
    ///   The JSON serialiser used in the instance.
    /// </summary>
    private readonly JsonSerializer jsonSerializer;

    /// <summary>
    ///   Creates and returns a new instance of the Json21 converter.
    /// </summary>
    /// <param name="jsonSerializer">
    ///  The JSON serialiser used in the instance.
    /// </param>
    public Json21(JsonSerializer jsonSerializer) {
      this.jsonSerializer = jsonSerializer;
    }

    /// <inheritdoc/>
    public Task JsonToBcf(string sourceFolder, string targetFile) {
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
        var version = new Version {
          VersionId = "2.1",
          DetailedVersion = "2.1"
        };
        await using var versionWriter =
          File.CreateText($"{tmpFolder}/bcf.version");
        new XmlSerializer(typeof(Version)).Serialize(versionWriter, version);

        var files = new List<string>(Directory.EnumerateFiles(sourceFolder));

        foreach (var file in files) {
          if (file.EndsWith("json") == false) {
            Console.WriteLine($" - File is not json, skipping ${file}");
            continue;
          }
          Console.WriteLine($" - Processing {file}");

          using var json = File.OpenText(file);
          var markup =
            (Markup)this.jsonSerializer.Deserialize(json, typeof(Markup));

          // Creating the target folder
          if (markup.Topic?.Guid == null) {
            Console.WriteLine(
              $" - Topic of Topic Guid is missing, skipping {file}");
            continue;
          }
          var guid = markup.Topic.Guid;
          var topicFolder = $"{tmpFolder}/{guid}";
          Directory.CreateDirectory(topicFolder);

          // Markup
          await using var markupWriter =
            File.CreateText($"{topicFolder}/markup.bcf");
          new XmlSerializer(typeof(Markup)).Serialize(markupWriter, markup);

          // Viewpoint
          await using var viewpointWriter =
            File.CreateText($"{topicFolder}/viewpoint.bcfv");
          new XmlSerializer(typeof(VisualizationInfo)).Serialize(
            viewpointWriter,
            markup.Viewpoints.First().VisualizationInfo);

          // Snapshot
          var snapshotFileName = markup.Viewpoints.First().Snapshot;
          var base64String = markup.Viewpoints.First().SnapshotData;
          if (snapshotFileName == null || base64String == null) continue;
          var result = Regex.Replace(base64String,
            @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
          await File.WriteAllBytesAsync(
            $"{topicFolder}/{snapshotFileName}",
            Convert.FromBase64String(result));
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

    /// <inheritdoc/>
    public Task WriteJson(ConcurrentBag<Markup> markups, string targetFolder) {
      return Task.Run(async () => {
        var jsonSerializerSettings = new JsonSerializerSettings {
          NullValueHandling = this.jsonSerializer.NullValueHandling,
          ContractResolver = this.jsonSerializer.ContractResolver
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
              Formatting.None, jsonSerializerSettings);

          var path = $"{targetFolder}/{markup.Topic.Guid}.json";
          await using var writer = File.CreateText(path);
          await writer.WriteAsync(json);
        }
      });
    }
  }
}