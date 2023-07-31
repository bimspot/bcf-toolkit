using System.Threading.Tasks;
using bcf_converter.Parser;

namespace bcf_converter; 

public interface IConverter {
  IJsonToBcfConverter json();
  IBcfParser parser();
}