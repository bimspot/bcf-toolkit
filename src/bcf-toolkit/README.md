This C# NuGet library allows you to easily build up and convert data into BCF files.
It gives you a straightforward API to build your BCF objects exactly how you want
in your order.

## Installation
You can install the `BcfToolkit` library via NuGet Package Manager or by adding
it to your project's .csproj file.
```
nuget install Smino.Bcf.Toolkit
```

## Usage
### Creating BCF objects
To create a BCF Model, you can use the BuilderCreator class to obtain a builder object.
Then, you can use various functions provided by the builder to fulfill the BCF model
objects.

**IMPORTANT:** The builder always uses the latest (BCF 3.0) models.

Here's an example:

```csharp
using BcfToolkit;

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