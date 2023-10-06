using System.Threading.Tasks;

namespace BcfConverter.Model;

public interface IRoot {
  public Task WriteBcf(string folder);
}