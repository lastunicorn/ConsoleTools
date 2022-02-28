# ==============================================================================
# Clone
# ==============================================================================

function Clone {
Param ([string] $repoUrl, [string] $destinationDirectory)

    WriteLabel "Clone - Cloning the git repository."

    git clone $repoUrl $destinationDirectory

}


# ==============================================================================
# Commit version increment
# ==============================================================================

function CommitChanges {
    Param ([string] $sourcesPath)

    WriteLabel "Commit Changes - Committing in git the changes: version increment."

    $oldLocation = Get-Location
    Set-Location $sourcesPath

    git add ".\src\AssemblyInfo.Shared.cs"
    git add ".\src\Fresenius.FMT.Installer\Product.wxs"
    git commit -m "incremented version for test release"
    git push

    Set-Location $oldLocation

}

# ==============================================================================
# Add git tag
# ==============================================================================

function AddGitTag {
    Param ([string] $sourcesPath, [string] $newVersion)

    WriteLabel "Add git tag - Adding in git a tag with the new version."

    $oldLocation = Get-Location
    Set-Location $sourcesPath

    git tag -a "test-$newVersion" -m "release for test for version $newVersion"
    git push --tags

    Set-Location $oldLocation

}