using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using bcf2json.Model;

namespace bcf2json.Parser {
  public interface BCFParser {
    Task<ConcurrentBag<Topic>> parse(string path);
  }
}