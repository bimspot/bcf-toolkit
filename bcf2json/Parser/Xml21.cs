using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using bcf2json.Model;

namespace bcf2json.Parser {
  public class Xml21 : BCFParser {
    public Task<ConcurrentBag<Topic>> parse(string path) {
      throw new NotImplementedException();
    }
  }
}