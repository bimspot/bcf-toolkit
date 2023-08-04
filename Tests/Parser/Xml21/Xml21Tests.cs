using System.Threading.Tasks;
using bcf;
using bcf.Parser;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using bcf21 = bcf.bcf21;
using bcf30 = bcf.bcf30;

namespace Tests.Parser.Xml21;

[TestFixture]
public class Xml21Tests {
  private JsonSerializer jsonSerializer = null!;

  [SetUp]
  public void setup() {
    var contractResolver = new DefaultContractResolver {
      NamingStrategy = new SnakeCaseNamingStrategy()
    };

    jsonSerializer = new JsonSerializer {
      NullValueHandling = NullValueHandling.Ignore,
      ContractResolver = contractResolver,
      Formatting = Formatting.Indented
    };
  }

  [Test]
  public async Task convertsBcfToJsonFiles21() {
    //var parser = new BcfParser<bcf21.Markup, bcf21.VisualizationInfo>();
    var json21 = new JsonConverter<bcf21.Markup, bcf21.Version>(jsonSerializer);
    var markups = await BcfConverter.ParseMarkups<bcf21.Markup, bcf21.VisualizationInfo>("Resources/TestBcf21.zip");
    await json21.WriteMarkupsJson(markups, "Resources/output/21/json");
  }
  
  [Test]
  public async Task convertsBcfMarkupsToJsonFiles30() {
    //var parser = new BcfParser<bcf30.Markup, bcf30.VisualizationInfo>();
    var json21 = new JsonConverter<bcf30.Markup, bcf30.Version>(jsonSerializer);
    var markups = await BcfConverter.ParseMarkups<bcf30.Markup,bcf30.VisualizationInfo>("Resources/TestBcf30.zip");
    await json21.WriteMarkupsJson(markups, "Resources/output/30/json");
  }
  
  [Test]
  public async Task convertsBcfRootsToJsonFiles30() {
    var extensions = await BcfConverter.ParseExtensions<bcf30.Extensions>("Resources/TestBcf30.zip");
    var project = await BcfConverter.ParseProject<bcf30.ProjectInfo>("Resources/TestBcf30.zip");

    var roots = new { Extensions = extensions, ProjectInfo = project };
    var json21 = new JsonConverter<bcf30.Markup, bcf30.Version>(jsonSerializer);
    //var markups = await BcfParser.ParseMarkups<bcf30.Markup,bcf30.VisualizationInfo>("Resources/TestBcf30.zip");
    //await json21.WriteMarkupsJson(markups, "Resources/output/30/json");
    await json21.WriteBcfRootsJson(roots, "Resources/output/30/json");
  }
  
}