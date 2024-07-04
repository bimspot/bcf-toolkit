using System;

namespace BcfToolkit;

/// <summary>
///   Provides logging for different levels of messages.
/// </summary>
public static class Log {
  /// <summary>
  ///   Delegate for handling log messages.
  /// </summary>
  public delegate void LogHandler(string message);

  private static LogHandler? LogDebug { get; set; }
  private static LogHandler? LogInfo { get; set; }
  private static LogHandler? LogWarning { get; set; }
  private static LogHandler? LogError { get; set; }

  /// <summary>
  ///   Configuring the handlers with custom logging functions.
  /// </summary>
  public static void Configure(
    LogHandler? debugHandler = null,
    LogHandler? infoHandler = null,
    LogHandler? warningHandler = null,
    LogHandler? errorHandler = null) {
    LogDebug = debugHandler;
    LogInfo = infoHandler;
    LogWarning = warningHandler;
    LogError = errorHandler;
  }

  /// <summary>
  ///   Configuring the handlers with `Console.WriteLine`.
  /// </summary>
  public static void ConfigureDefault() {
    LogDebug = Console.WriteLine;
    LogInfo = Console.WriteLine;
    LogWarning = Console.WriteLine;
    LogError = Console.WriteLine;
  }

  /// <summary>
  ///   Write a log event with the debug level.
  /// </summary>
  /// <param name="message">Message describing the event.</param>
  public static void Debug(string message) {
    LogDebug?.Invoke(message);
  }

  /// <summary>
  ///   Write a log event with the information level.
  /// </summary>
  /// <param name="message">Message describing the event.</param>
  public static void Info(string message) {
    LogInfo?.Invoke(message);
  }

  /// <summary>
  ///   Write a log event with the warning level.
  /// </summary>
  /// <param name="message">Message describing the event.</param>
  public static void Warning(string message) {
    LogWarning?.Invoke(message);
  }

  /// <summary>
  ///   Write a log event with the error level.
  /// </summary>
  /// <param name="message">Message describing the event.</param>
  public static void Error(string message) {
    LogError?.Invoke(message);
  }
}