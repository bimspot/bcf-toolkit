using System.Collections.Concurrent;
using System.Threading.Tasks;
using bcf_converter.Model;

namespace bcf_converter.Parser {
  public interface BCFParser {
    Task<ConcurrentBag<Topic>> parse(string path);
  }
}