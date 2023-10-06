# bcf-converter

A .NET library and a command line utility for converting `BCF` (Building 
Collaboration Format) files into `json` and vice versa.

The tool converts `BCF` information across formats ~~and versions~~. 

## Usage

## Requirements

- dotnet 7

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
This C# NuGet library allows you to easily build up convert data into BCF files.
It gives you a straightforward API to build your BCF objects exactly how you want
in your order.

#### Installation
You can install the `BcfConverter` library via NuGet Package Manager or by adding 
it to your project's .csproj file.
```
nuget install BCFConverter
```

#### Usage
##### Creating BCF objects
To create a BCF Model, you can use the BuilderCreator class to obtain a builder object. 
Then, you can use various functions provided by the builder to fulfill the BCF model 
objects. 

**IMPORTANT:** The builder always creates BCF 3.0 models.

Here's an example:

```csharp
using BCFConverter;

// Create a markup builder
var markupBuilder = BuilderCreator.CreateMarkupBuilder();

// Build the BCF Markup
var bcfMarkup = markupBuilder
    .AddTitle("Simple title")
    .AddDescription("This is a description")
    .AddLabel("Architecture")
    .AddPriority("Critical")
    .AddTopicType("Clash")
    .AddTopicStatus("Active")
    .AddComment(c => c
        .AddComment("This is a comment")
        .AddDate(DateTime.Now)
        .AddAuthor("jimmy@page.com"))
    .AddViewPoint(v => v
        .AddPerspectiveCamera(pCam => pCam
            .AddCamera(cam => cam
                .AddViewPoint(10, 10, 10))),
        snapshotData) // Provide snapshot data here
    .Build();

// Create a project builder
var projectBuilder = BuilderCreator.CreateProjectBuilder();

// Build the BCF Project
var project = projectBuilder
    .AddProjectId("projectId")
    .AddProjectName("My project")
    .Build();
    
// Create a document builder
var documentBuilder = BuilderCreator.CreateDocumentBuilder();

// Build the BCF Document
var document = builder
    .AddDocument(d => d
    .AddFileName("document.pdf")
    .AddDescription("This is a document"))
    .Build();

// Create an extensions builder
var extBuilder = BuilderCreator.CreateExtensionsBuilder();

// Build the BCF Extensions
var extensions = builder
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
    .AddTopicLabel("MEP")
    .Build();
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
  |- bcfRoot.json
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

Code style guide can be found in the `bcf-converter.sln.DotSettings` file.

[1]: https://github.com/buildingSMART/BCF-XML/tree/master/Schemas
[2]: https://github.com/mganss/XmlSchemaClassGenerator
[3]: https://github.com/BuildingSMART/BCF-XML/tree/master/Documentation
[4]: https://github.com/BuildingSMART/BCF-API