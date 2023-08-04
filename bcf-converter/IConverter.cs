using System.Threading.Tasks;

namespace bcf; 

public interface IConverter {
  Task BcfToJson(string source, string target);
  Task JsonToBcf(string source, string target);
}