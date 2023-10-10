using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BcfToolkit.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RecursiveDataAnnotationsValidation;

namespace BcfToolkit.Converter;

/// <summary>
///   The `JsonConverter` static class opens and parses the BCF json files
///   and puts their contents into the BCF models. It also writes the in
///   memory BCF models into json files.
/// </summary>
public static class JsonConverter {
  /// <summary>
  ///   The method parses the markup json files to create an in memory
  ///   representation of the data.
  ///   Topic folder structure inside a BCFzip archive:
  ///   The folder name is the GUID of the topic. This GUID is in the UUID form.
  ///   The GUID must be all-lowercase. The folder contains the following file:
  ///   * markup.bcf
  ///   Additionally the folder can contain other files:
  ///   * Viewpoint files
  ///   * Snapshot files
  ///   * Bitmaps
  /// </summary>
  /// <param name="sourceFolder"></param>
  /// <returns></returns>
  /// <exception cref="ApplicationException"></exception>
  public static Task<ConcurrentBag<TMarkup>> ParseMarkups<TMarkup>(
    string sourceFolder)
    where TMarkup : IMarkup {
    return Task.Run(async () => {
      // A thread-safe storage for the parsed topics.
      var markups = new ConcurrentBag<TMarkup>();

      var files = new List<string>(Directory.EnumerateFiles(sourceFolder));
      var topicFiles = files
        .Where(file =>
          Regex.IsMatch(Path.GetFileNameWithoutExtension(file).Replace("-", ""),
            "^[a-fA-F0-9]+$"))
        .ToList();

      foreach (var file in topicFiles) {
        if (file.EndsWith("json") == false) {
          Console.WriteLine($" - File is not json, skipping ${file}");
          continue;
        }

        Console.WriteLine($" - Processing {file}");

        var markup = await ParseObject<TMarkup>(file);
        markups.Add(markup);
      }

      return markups;
    });
  }

  /// <summary>
  ///   The method opens the json file at the specified source path,
  ///   and parses to the given object type to create an in memory
  ///   representation of the data.
  /// </summary>
  /// <param name="source">The source path of the json file.</param>
  /// <typeparam name="T">The json file is deserializes to this type.</typeparam>
  /// <returns></returns>
  public static Task<T> ParseObject<T>(string source) {
    return Task.Run(() => {
      var contractResolver = new DefaultContractResolver {
        NamingStrategy = new SnakeCaseNamingStrategy()
      };
      var jsonSerializer = new JsonSerializer {
        NullValueHandling = NullValueHandling.Ignore,
        ContractResolver = contractResolver
      };
      using var json = File.OpenText(source);
      var deserialized = (T)jsonSerializer.Deserialize(json, typeof(T));
      var validator = new RecursiveDataAnnotationValidator();
      var validationErrors = new List<ValidationResult>();
      if (validator.TryValidateObjectRecursive(deserialized, validationErrors))
        return deserialized;
      var errors = string.Join(
        "\n",
        validationErrors.Select(e => e.ErrorMessage));
      throw new ArgumentException($"Missing required fields(s):\n {errors}");
    });
  }

  /// <summary>
  ///   The method writes the BCF objects to json file.
  /// </summary>
  /// <param name="targetFolder">The target folder where the json files will be saved.</param>
  /// <param name="markups">The list of `Markup` objects.</param>
  /// <param name="root">The `Root` object.</param>
  /// <returns></returns>
  public static Task WriteJson<TMarkup, TRoot>(
    string targetFolder,
    ConcurrentBag<TMarkup> markups,
    TRoot root) {
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
      if (Directory.Exists(targetFolder)) Directory.Delete(targetFolder, true);
      Directory.CreateDirectory(targetFolder);

      // Writing markups to disk, one markup per file.
      foreach (var markup in markups) {
        var jsonMarkup = JsonConvert
          .SerializeObject(
            markup,
            Formatting.None,
            jsonSerializerSettings);

        var pathMarkup =
          $"{targetFolder}/{((IMarkup)markup!).GetTopic()!.Guid}.json";
        await using var writerM = File.CreateText(pathMarkup);
        await writerM.WriteAsync(jsonMarkup);
      }

      // Writing BCF root file
      var json = JsonConvert
        .SerializeObject(
          root,
          Formatting.None, jsonSerializerSettings);
      var path = $"{targetFolder}/bcfRoot.json";
      await using var writerR = File.CreateText(path);
      await writerR.WriteAsync(json);
    });
  }
}