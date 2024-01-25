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
To create a BCF Model, you can use the BuilderCreator class to obtain a builder 
object.
Then, you can use various functions provided by the builder to fulfill the BCF 
model objects.

**IMPORTANT:** The builder always uses the latest (BCF 3.0) models.

Here's an example:

```csharp
using BcfToolkit;

// Build the BCF Markup
var markup = BcfBuilder.Markup()
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

// Build the BCF Project
var project = BcfBuilder.Project()
    .AddProjectId("projectId")
    .AddProjectName("My project")
    .Build();

// Build the BCF Document
var document = BcfBuilder.Document()
    .AddDocument(d => d
    .AddFileName("document.pdf")
    .AddDescription("This is a document"))
    .Build();

// Build the BCF Extensions
var extensions = BcfBuilder.Extensions()
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

You can also use the default builders if you prefer not to deal with filling the 
required fields. The `builder.WithDefaults()` function serves this for you. 
However in certain cases you may need to replace the component IDs of IFC 
objects with their actual GUIDs during the build process.

```csharp
var markup = BcfBuilder.Markup()
  .WithDefaults()
  .Build();
```