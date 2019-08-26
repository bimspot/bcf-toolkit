using System.Collections.Generic;
using System.Threading.Tasks;
using bcf2json.Model;

namespace bcf2json.Parser {
  public interface BCFParser {
    Task<List<Topic>> jsonFrom(string path);
  }
}