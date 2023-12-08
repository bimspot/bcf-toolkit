using System;
using System.CommandLine;
using System.CommandLine.Binding;

namespace BcfToolkit;

public class InputArguments {
  public string SourcePath { get; set; }
  public string TargetFolder { get; set; }
  public string TargetVersion { get; set; }

  public InputArguments(
    string sourcePath,
    string targetFolder,
    string targetVersion) {
    this.SourcePath = sourcePath;
    this.TargetFolder = targetFolder;
    this.TargetVersion = targetVersion;
    this.Validate();
  }

  /// <summary>
  ///   Validates if the arguments have every required properties and the values are not empty.
  /// </summary>
  /// <exception cref="ArgumentNullException">
  ///   Throws and exception if any of the arguments is missing or wrong.
  /// </exception>
  private void Validate() {
    foreach (var arg in this.GetType().GetProperties())
      if (string.IsNullOrEmpty(arg.GetValue(this, null)?.ToString()))
        throw new ArgumentNullException($"Argument is missing or wrong: {arg.Name}");
  }
}

public class InputArgumentsBinder : BinderBase<InputArguments> {
  private readonly Option<string> _sourcePathOption;
  private readonly Option<string> _targetFolderOption;
  private readonly Option<string> _versionOption;

  public InputArgumentsBinder(
    Option<string> sourcePathOption,
    Option<string> targetFolderOption,
    Option<string> versionOption) {
    _sourcePathOption = sourcePathOption;
    _targetFolderOption = targetFolderOption;
    _versionOption = versionOption;
  }

  protected override InputArguments GetBoundValue(BindingContext bindingContext) =>
    new InputArguments
    (
      bindingContext.ParseResult.GetValueForOption(_sourcePathOption),
      bindingContext.ParseResult.GetValueForOption(_targetFolderOption),
      bindingContext.ParseResult.GetValueForOption(_versionOption)
    );
}