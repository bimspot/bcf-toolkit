using System.Threading.Tasks;

namespace BcfToolkit.Model;

public interface IRoot {
  public Task WriteBcf(string folder);
}