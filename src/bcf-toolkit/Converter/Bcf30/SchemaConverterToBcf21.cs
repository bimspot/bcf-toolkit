using System;
using BcfToolkit.Builder;
using BcfToolkit.Model;

namespace BcfToolkit.Converter.Bcf30;

public static class SchemaConverterToBcf21 {
  
  public static Model.Bcf21.Bcf Convert(IBcf from) {
    var result = new Model.Bcf21.Bcf();
    var sourceProperties = from.GetType().GetProperties();
    var targetProperties = typeof(Model.Bcf21.Bcf).GetProperties();
    foreach (var sourceProperty in sourceProperties) {
      var targetProperty = Array.Find(targetProperties,
        p => p.Name == sourceProperty.Name &&
             p.PropertyType == sourceProperty.PropertyType);
      if (targetProperty != null) {
        targetProperty.SetValue(result, sourceProperty.GetValue(from));
      }
    }
    
    //TODO Handle manually the differencies
    return BuilderUtils.ValidateItem(result);
  }
}