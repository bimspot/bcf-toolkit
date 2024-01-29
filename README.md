# bcf-toolkit

A .NET library and a command line utility for converting `BCF` (Building 
Collaboration Format) files into `json` and vice versa.

The tool converts `BCF` information across formats ~~and versions~~. 

## Usage

## Requirements

- dotnet 8

### CLI

The command line interface accepts three arguments:
 * the source bcf file or json folder
 * the target bcf file or json folder
 
The json representation is one file for every `Markup`, while the BCF format
is a zipped file as per the standard.

```
~ bcf-converter /path/to/source.bcfzip /path/to/target/json/folder

~ bcf-converter /path/to/source/json/folder /path/to/target.bcfzip
```

### As A Library
This C# NuGet library allows the user to easily build up and convert data into 
BCF files. It gives a straightforward API to build the BCF objects exactly in 
the order of the user's choice.

#### Installation
`BcfConverter` library can be installed via NuGet Package Manager or by adding 
it to the project's .csproj file.
```
nuget install BCFConverter
```

#### Usage
##### Creating BCF objects
To create a BCF Model, `BuilderBuilder` class can be used. Then, various 
functions provided by the builder can be used to fulfill the BCF model objects. 

Here are some examples:

```csharp
using BcfToolkit.Builder.Bcf30;

var builder = new BcfBuilder();
var bcf = builder
  .AddMarkup(m => m
    .SetTitle("Simple title")
    .SetDescription("This is a description")
    .AddLabel("Architecture")
    .SetPriority("Critical")
    .SetTopicType("Clash")
    .SetTopicStatus("Active")
    .AddComment(c => c
      .SetComment("This is a comment")
      .SetDate(DateTime.Now)
      .SetAuthor("jimmy@page.com"))
    .AddViewPoint(v => v
        .SetPerspectiveCamera(pCam => pCam
          .SetCamera(cam => cam
            .SetViewPoint(10, 10, 10))),
      snapshotData)) // Provide a snapshot data here
  .SetExtensions(e => e
    .AddPriority("Critical")
    .AddPriority("Major")
    .AddPriority("Normal")
    .AddPriority("Minor")
    .AddTopicType("Issue")
    .AddTopicType("Fault")
    .AddTopicType("Clash")
    .AddTopicType("Remark")
    .AddTopicLabel("Architecture")
    .AddTopicLabel("Structure")
    .AddTopicLabel("MEP"))
  .SetProject(p => p
    .SetProjectId("projectId")
    .SetProjectName("My project"))
  .SetDocumentInfo(dI => dI
    .AddDocument(d => d
      .SetFileName("document.pdf")
      .SetDescription("This is a document")))
  .Build();
```

The `BcfBuilder` class can also consume BCF files as a stream and build up the
model objects.

```csharp
using BcfToolkit.Builder.Bcf30;

await using var stream = new FileStream(source, FileMode.Open, FileAccess.Read);
var builder = new BcfBuilder();
var bcf = await builder
    .BuildFromStream(stream);
```

The default builders can be used if the user prefers not to deal with filling 
the required fields. The `builder.WithDefaults()` function serves this. 
However in certain cases the user may need to replace the component IDs of IFC 
objects with the actual GUIDs during the build process.

```csharp
using BcfToolkit.Builder.Bcf30;

var builder = new BcfBuilder();
var bcf = builder
    .WithDefaults()
    .Build();
```
##### Using BCF workers
The workers are implemented to use predefined workflows to convert `BCF` files 
into `json`. The aimed BCF version must be set first then `ConverterContext` 
class lets the nested object to do the conversion accordingly.

```csharp
using BcfToolkit;
using BcfToolkit.Model;

var version = BcfVersion.Parse(arguments.TargetVersion);
var context = new ConverterContext(version);
await context.Convert("sourcePath", "targetPath");
```

The exact worker can be called directly as well for both converting directions,
`BCF` into `json` and back.

```csharp
using BcfToolkit.Worker.Bcf30;

var worker = new ConverterWorker()
worker.BcfZipToJson(source, target);
```

```csharp
using BcfToolkit.Worker.Bcf30;

var worker = new ConverterWorker()
worker.JsonToBcfZip(source, target);
```

## File Structure

The structure of the BCF is per [the standard][3]. There is, however, no 
standard for the JSON format other than the [BCF API specification][4].

The naming convention for this converter is taken from the BCF API, but the
output is opinionated:

There is one JSON for every `Markup` and within the structure of the information
follows that of the XML. There are no separate files for screenshots, they are
base64 encoded in the field `snapshot_data` of the `Viewpoint`. The files are
named using the `uuid` of the `Topic` within.

```
|- json
  |- 2ea98d1e-77ae-467f-bbc9-1aed1c1785f6.json
  |- 3395f1b1-893f-4ca0-8b7d-c2d17d7a9198.json
  |- c799e527-a413-43f8-8871-80918a52b0f0.json
  |- ...
  |- project.json
  |- extensions.json
  |- documents.json
```

## Development

The development of the tool is ongoing, the table below shows the currently 
completed features.

|          | BCF 2.0 | BCF 2.1 | BCF 3.0 | JSON 2.0 | JSON 2.1 | JSON 3.0 |
|----------|:-------:|:-------:|:-------:|:--------:|:--------:|:--------:|
| BCF 2.0  |         |         |         |          |          |          |
| BCF 2.1  |         |         |         |          |    X     |          |
| BCF 3.0  |         |         |         |          |          |    X     |
| JSON 2.0 |         |         |         |          |          |          |
| JSON 2.1 |         |    X    |         |          |          |          |
| JSON 3.0 |         |         |    X    |          |          |          |

The models for the BCF in-memory representation were auto-created from the
[latest XSDs][1] using the [`XmlSchemaClassGenerator`][2].

```
~ dotnet tool install --global dotnet-xscgen
~ xscgen -n [namespace] version.xsd
```

To publish, run the script at `dist/publish.sh`.

### TODO

- profile memory and CPU usage

### Contribution

Code style guide can be found in the `bcf-toolkit.sln.DotSettings` file.

[1]: https://github.com/buildingSMART/BCF-XML/tree/master/Schemas
[2]: https://github.com/mganss/XmlSchemaClassGenerator
[3]: https://github.com/BuildingSMART/BCF-XML/tree/master/Documentation
[4]: https://github.com/BuildingSMART/BCF-API