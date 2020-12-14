# ==============================================================================
# Get the release output
# ==============================================================================

function PublishOutput {
    Param ([string] $sourcesPath, [string] $outputPath, [string] $newVersion)

    WriteLabel "Publish Output - Publishing the resulted installation package."

    $sourcePath = Join-Path -Path $sourcesPath -ChildPath "\src\output-installer"

    $destinationDirectory = (Get-Date -f "yyyy MM dd HHmm") + " - FMT " + $newVersion
    $destinationPath = Join-Path -Path $outputPath -ChildPath $destinationDirectory

    Copy-Item -Path "$sourcePath" -Destination "$destinationPath" -Recurse -Container

}