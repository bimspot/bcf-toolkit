# bcf-converter

A .NET library and a command line utility for converting `BCF` (Building 
Collaboration Format) files into `json` and vice versa.

The tool converts `BCF` information across formats and versions. The 
development is ongoing, the table below shows the currently completed features.

|          | XML 2.0 | XML 2.1 | JSON 2.0 | JSON 2.1 |
|:--------:|---------|---------|----------|----------|
| XML 2.0  |         |         |          |     X    |
| XML 2.1  |         |         |          |          |
| JSON 2.0 |         |         |          |          |
| JSON 2.1 |         |         |          |          |

## Usage

### CLI

The command line interface accepts three arguments:
 * the source file
 * the target file
 * (optional) the BCF version of the output, the default is 2.1
 * (the version of the source is inferred from the file)

```
$ ifc-converter /path/to/source.bcfzip /path/to/target.json 2.1
```
