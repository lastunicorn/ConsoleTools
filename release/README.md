# Console Tools - Release Procedure

## Before Starting

- Verify the content of the `doc\changelog.txt` file.
- Verify the version to be consistent in all files:
  - `doc\changelog.txt` file;
  - `sources\ConsoleTools\AssemblyInfo.Shared.cs` file;
  - `release\ConsoleTools.proj` file.

## Step 1 - Create the release

- Open a Developer Command Prompt for Visual Studio
- Run the `ConsoleTools.proj` file:

```
msbuild ConsoleTools.proj
```

The resulted files are located in the `output` directory.

## Step 2 - Increment version

Increment the version in all files:

- `doc\changelog.txt` file;
- `sources\ConsoleTools\AssemblyInfo.Shared.cs` file;
- `release\ConsoleTools.proj` file.
