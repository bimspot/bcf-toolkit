# bcf-converter

A .NET library and a command line utility for converting `BCF` (Building 
Collaboration Format) files into `json` and vice versa.

The tool converts `BCF` information across formats ~~and versions~~. 

## Usage

### CLI

The command line interface accepts three arguments:
 * the source bcf file or json folder
 * the target bcf file or json folder
 * (optional) the BCF version of the output, the default is 2.1
 
The json representation is one file for every Markup, while the BCF format
is a zipped file as per the standard.

```
$ ifc-converter /path/to/source.bcfzip /path/to/target/folder 2.1

$ ifc-converter /path/to/json/folder /path/to/target/bcf.bcfzip 2.1
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