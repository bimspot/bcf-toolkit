using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using bcf_converter.Model;

namespace bcf_converter.Parser {
  public class Xml21 : BCFParser {
    public Task<ConcurrentBag<Topic>> parse(string path) {
      throw new NotImplementedException();
    }
  }
}