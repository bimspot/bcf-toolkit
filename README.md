# bcf-converter

A .NET library and a command line utility for converting `BCF` (Building 
Collaboration Format) files into `json` and vice versa.

The tool converts `BCF` information across formats ~~and versions~~. 

## Usage

### CLI

The command line interface accepts three arguments:
 * the source bcf file or json folder
 * the target bcf file or json folder
 
The json representation is one file for every Markup, while the BCF format
is a zipped file as per the standard.

```
$ ifc-converter /path/to/source.bcfzip /path/to/target/json/folder

$ ifc-converter /path/to/source/json/folder /path/to/target.bcfzip
```

## File Structure

The structure of the BCF is per [the standard][3]. There is however no standard
for the JSON format other than the [BCF API specification][4].

The naming convention for this converter is taken from the BCF API, but the
output is opinionated.

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
```

## Development

The development of the tool is ongoing, the table below shows the currently 
completed features.

|          | XML 2.0 | XML 2.1 | JSON 2.0 | JSON 2.1 |
|----------|:-------:|:-------:|:--------:|:--------:|
| XML 2.0  |         |         |          |          |
| XML 2.1  |         |         |          |     X    |
| JSON 2.0 |         |         |          |          |
| JSON 2.1 |         |    X    |          |          |

The models for the BCF in-memory representation were auto-created from the
[latest XSDs][1] using the [`XmlSchemaClassGenerator`][2].

To publish, run the script at `dist/publish.sh`.

### Contribution

Code style guide can be found in the `bcf-converter.sln.DotSettings` file.

[1]: https://github.com/buildingSMART/BCF-XML/tree/master/Schemas
[2]: https://github.com/mganss/XmlSchemaClassGenerator
[3]: https://github.com/BuildingSMART/BCF-XML/tree/master/Documentation
[4]: https://github.com/BuildingSMART/BCF-API