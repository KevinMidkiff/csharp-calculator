# C# Calculator

C# calculator

## Pre-Requisites 

The following must be installed on your system to build and run the calculator.

* [Visual Studio Code](https://code.visualstudio.com/download)
* [.NET SDK 7.0+](https://dotnet.microsoft.com/en-us/download)
* [C# Dev Kit VSCode Extention](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit)

## Compilation

1. Open the C# project in VSCode
2. Open the Command-Pallete (`CTRL + SHIFT + P`) and run the `.NET: Build` command

This should build the binary for the calculator at `./csharp-calculator/bin/csharp-calculator.exe`.

## Usage

```
usage: csharp-calculator [-h | --help] <expr>
        -h | --help - Show this help
        expr        - Simple mathematical expression to calculate

Examples:
        $ ./csharp-calculator 1 * (3 + 4) / 55
        $ ./csharp-calculator 55 + 2
        $ ./csharp-calculator 3 / 6
```
> Run the binary with `-h` or `--help` to see the help above.

## Running Unit Tests

To execute the unit tests, do the following:

1. Open the C# project in VSCode
2. Open the Command-Pallete (`CTRL + SHIFT + P`) 
3. Run the `Test: Run All Tests` command

### Refreshing Tests

If in the development process new tests are added, VS Code will not recognize them until 
the `.NET: Rebuild` command from the Command-Pallete is executed. If this is not done, 
then when the `Test: Run All Tests` command is executed any new tests will not be 
ran.