# ==============================================================================
# Cleanup
# ------------------------------------------------------------------------------
# Delete directories from old builds
# ==============================================================================


function Cleanup {
Param ([string] $path)

    WriteLabel "Clenup - Removing old build"

    if (Test-Path $global:config.SourcesPath)
    {
        Remove-Item $global:config.SourcesPath -Recurse -Force;
    }

}