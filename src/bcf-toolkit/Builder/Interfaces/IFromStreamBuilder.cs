using System.IO;
using System.Threading.Tasks;

namespace BcfToolkit.Builder.Interfaces;

public interface IFromStreamBuilder<TItem> {
  /// <summary>
  ///   It builds and returns the specified item from a specified stream.
  /// </summary>
  /// <returns>Returns the built item.</returns>
  Task<TItem> BuildFromStream(Stream source);
}