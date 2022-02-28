# ==============================================================================
# Build the ConsoleTools Library
# ==============================================================================

function BuildApplication {
    Param ([string] $sourcesPath, [string] $nugetPath, [string] $msbuildPath)

    WriteLabel "Build App - Building the ConsoleTools library."

    $slnPathApp = [System.IO.Path]::Combine($sourcesPath, "sources\ConsoleTools\ConsoleTools.sln")

    # Restore nuget packages
    & $nugetPath restore $slnPathApp

    # Build solution
    & $msbuildPath $slnPathApp /t:ConsoleTools /p:Configuration=Release-Net461 /p:Platform="Any CPU"

}