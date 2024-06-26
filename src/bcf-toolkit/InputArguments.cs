using System;
using System.CommandLine;
using System.CommandLine.Binding;

namespace BcfToolkit;

public class InputArguments {
  public string SourcePath { get; set; }
  public string Target { get; set; }

  public InputArguments(
    string sourcePath,
    string target) {
    this.SourcePath = sourcePath;
    this.Target = target;
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
  private readonly Option<string> _targetOption;

  public InputArgumentsBinder(
    Option<string> sourcePathOption,
    Option<string> targetOption) {
    _sourcePathOption = sourcePathOption;
    _targetOption = targetOption;
  }

  protected override InputArguments GetBoundValue(BindingContext bindingContext) =>
    new InputArguments
    (
      bindingContext.ParseResult.GetValueForOption(_sourcePathOption),
      bindingContext.ParseResult.GetValueForOption(_targetOption)
    );
}