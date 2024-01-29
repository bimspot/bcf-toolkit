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
