using System.Collections.Concurrent;
using System.Threading.Tasks;
using bcf_converter.Model;

namespace bcf_converter.Parser {
  /// <summary>
  ///   The `BCFParser` protocol defines the methods to be implemented by any
  ///   BCF file parsers.
  /// </summary>
  public interface BCFParser {
    /// <summary>
    ///   The method unzips the bcfzip at the specified path into the memory,
    ///   and parses the xml files within to create a Topic representation of
    ///   the data.
    /// </summary>
    /// <param name="path">The absolute path to the bcfzip.</param>
    /// <returns>Returns a Task with a List of Topic models.</returns>
    Task<ConcurrentBag<Topic>> parse(string path);
  }
}