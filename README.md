# GoogleSpreadSheetExporter

App that exports a given worksheet from a Google Sheet.
Uses OAuth2 to access private documents.

## How to run

1. Copy App.config.example to App.config and edit it.(Instructions are included in the example.)
2. Open the solution with Visual Studio and switch the build configuration to Release.
3. Build and Run.

...or you can just download the execution file from [release](https://github.com/Arisix/GoogleSpreadSheetExporter/releases/download/v1.0.0/GoogleSpreadSheetExporter.zip).

## Usage

```
Usage: greet [OPTIONS]+ message
Greet a list of individuals with an optional message.
If no message is specified, a generic greeting is used.

Options:
  -i, --input=VALUE          Config file path.(Not App.config.)
  -o, --output=VALUE         Output directory path.
  -h, --help                 show this message and exit
```

## Example

```
GoogleSpreadSheetExporter.exe -i input.example -o ./output/
```

**Output path must be a directory.**
