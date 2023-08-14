using System.Threading.Tasks;

namespace bcf; 

public interface IRoot {
  public Task WriteBcf(string folder);
}