using System;
using System.Collections.Generic;
using BcfToolkit.Model;
using BcfToolkit.Model.Bcf30;

namespace BcfToolkit.Builder.Bcf30;

public partial class BcfBuilder :
  IBcfBuilderExtension<BcfBuilder, ExtensionsBuilder, DocumentInfoBuilder> {
  public BcfBuilder SetExtensions(Action<ExtensionsBuilder> builder) {
    var extensions =
      (Extensions)BuilderUtils.BuildItem<ExtensionsBuilder, IExtensions>(builder);
    _bcf.Extensions = extensions;
    return this;
  }

  public BcfBuilder SetDocumentInfo(Action<DocumentInfoBuilder> builder) {
    var documentInfo =
      (DocumentInfo)BuilderUtils.BuildItem<DocumentInfoBuilder, IDocumentInfo>(builder);
    _bcf.Document = documentInfo;
    return this;
  }
}