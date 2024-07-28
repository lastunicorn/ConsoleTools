# Console Tools - Version Management

How Version number is propagated throughout the whole project at build time?

## Visual Studio Build

- `\sources\ConsoleTools\Directory.build.props` file; (Version = 0.0.0.0)
  - All net5.0 projects (assemblies) => Version = 0.0.0.0
  - All nuget packages  => Version = 0.0.0
- `\sources\ConsoleTools\AssemblyInfo.Shared.cs` file; (Version = value)
  - All net framework projects (assemblies) => Version = value

## Release Build

- `\doc\changelog.txt` file (Version = value1)
- `\sources\ConsoleTools\AssemblyInfo.Shared.cs` file; (Version = value2)
  - All net framework projects (assemblies) => Version = value2
- `\release\ConsoleTools.proj` file. (Version = value3)
  - All net5.0 projects (assemblies) => Version = value3
  - All nuget packages  => Version = value3
    - changelog.txt => Version = value1
  - Zip package => Version = value3
    - changelog.txt => Version = value1