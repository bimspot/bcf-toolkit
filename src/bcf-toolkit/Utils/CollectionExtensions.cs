using System.Collections.ObjectModel;

namespace BcfToolkit.Utils;

public static class CollectionExtensions {
  public static void AddIfNotExists<T>(this Collection<T> collection, T item) {
    if(!collection.Contains(item))
      collection.Add(item);
  }
}