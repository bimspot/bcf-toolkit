using System.Collections.Concurrent;

namespace BcfToolkit.Model.Bcf30;

public class Bcf : IBcf {
  public Bcf() {
    this.Markups = new ConcurrentBag<Markup>();
  }
  
  [System.ComponentModel.DataAnnotations.RequiredAttribute]
  public ConcurrentBag<Markup> Markups { get; set; }

  public DocumentInfo? Document { get; set; }

  [System.ComponentModel.DataAnnotations.RequiredAttribute]
  public Extensions Extensions { get; set; }

  public ProjectInfo? Project { get; set; }
}