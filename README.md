This C# NuGet library allows you to easily build up and convert data into BCF 
files. It gives you a straightforward API to build your BCF objects exactly how 
you want in your order.

## Installation
You can install the `BcfToolkit` library via NuGet Package Manager or by adding
it to your project's .csproj file.
```
nuget install Smino.Bcf.Toolkit
```

## Usage

### Creating BCF objects
To create a BCF Model, `BcfBuilder` class can be used. Then, various
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

### Using BCF worker
The `BCF` worker is designed to facilitate the conversion of `BCF` files to
`JSON` and vice versa using predefined workflows. The appropriate workflow is
selected based on the file extensions of the source and target. For example, if
the source file ends with `.bcfzip`, the `BcfZipToJson` converter is used.

#### Basic conversion

```csharp
using BcfToolkit;

var worker = new Worker();
await worker.Convert(source, target);
```

#### Direct converter usage
You can also call specific converters directly for converting between `BCF` and
`JSON`.

##### BCF to JSON

```csharp
using BcfToolkit.Converter.Bcf30;

var converter = new Converter()
converter.BcfZipToJson(source, target);
```

##### JSON to BCF

```csharp
using BcfToolkit.Converter.Bcf30;

var converter = new Converter()
converter.JsonToBcfZip(source, target);
```

#### Stream-based conversion
The library supports consuming `BCF` archives as streams. The code determines
the version of the source and delegates the conversion to the appropriate nested
converter object, converting it to `BCF` 3.0 as needed.

##### BCF from Stream

```csharp
using BcfToolkit;

await using var stream = new FileStream(source, FileMode.Open, FileAccess.Read);

var worker = new Worker();
var bcf = await worker.BcfFromStream(stream);
```

##### BCF to Stream
The worker can generate a file stream from a specified `BCF` object, converting
it to the desired version.
> IMPORTANT: The user is responsible for disposing of the stream after use.

```csharp
using BcfToolkit;

var worker = new Worker();
var stream = await worker.ToBcf(bcf, BcfVersionEnum.Bcf30);
// custom code to use the stream...
await stream.FlushAsync();
```

##### Specifying Output Stream
You can define an output stream to which the conversion results will be written.

```csharp
using BcfToolkit;

var worker = new Worker();
var outputStream = new MemoryStream();
worker.ToBcf(bcf, BcfVersionEnum.Bcf30, outputStream);
// custom code to use the stream...
await outputStream.FlushAsync();
```

#### Cancellation Support
The worker supports `CancellationToken` to allow for the cancellation of
asynchronous operations.

```csharp
using BcfToolkit;

var worker = new Worker();
var outputStream = new MemoryStream();
var source = new CancellationTokenSource();
var token = source.Token;
worker.ToBcf(bcf, BcfVersionEnum.Bcf30, outputStream, token);
// custom code to use the stream...
await outputStream.FlushAsync();
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
  |- version.json
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


### Contribution

Code style guide can be found in the `bcf-toolkit.sln.DotSettings` file.

[1]: https://github.com/buildingSMART/BCF-XML/tree/master/Schemas
[2]: https://github.com/mganss/XmlSchemaClassGenerator
[3]: https://github.com/BuildingSMART/BCF-XML/tree/master/Documentation
[4]: https://github.com/BuildingSMART/BCF-API