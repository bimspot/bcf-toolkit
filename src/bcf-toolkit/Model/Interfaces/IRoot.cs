using System.Threading.Tasks;

namespace BcfToolkit.Model.Interfaces;

public interface IRoot {
  public Task WriteBcf(string folder);
}