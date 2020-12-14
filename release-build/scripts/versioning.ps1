# ==============================================================================
# Version Management
# ==============================================================================

function IncrementVersion {
    Param ([string] $sourcesPath, [int32] $versionComponent)

    WriteLabel "Increment Version - Incrementing the version of the application."

    # Read current version
    # ------------------------------------------------------------------------------

    $newVersion = RetrieveVersion -sourcesPath $sourcesPath
    $newVersion.IncrementComponent($versionComponent)
    $newVersionAsString = $newVersion.ToString()


    # Update Version
    # ------------------------------------------------------------------------------

    UpdateAssemblyVersion -sourcesPath $sourcesPath -version $newVersionAsString
    UpdateInstallerVersion -sourcesPath $sourcesPath -version $newVersionAsString


    # save the new version for later (in global)
    # ------------------------------------------------------------------------------

    $global:newVersion = $newVersionAsString

}

function SetVersion {
    Param ([string] $sourcesPath, [string] $version)

    WriteLabel "Set Version - Sets the version of the application to '$version'."


    # Update Version
    # ------------------------------------------------------------------------------

    UpdateAssemblyVersion -sourcesPath $sourcesPath -version $version
    UpdateInstallerVersion -sourcesPath $sourcesPath -version $version


    # save the new version for later (in global)
    # ------------------------------------------------------------------------------

    $global:newVersion = $version

}

function RetrieveVersion {
    Param ([string] $sourcesPath)

    $versionFilePath = $sourcesPath + "\sources\ConsoleTools\AssemblyInfo.Shared.cs"
    $versionFileContent = Get-Content -Path "$versionFilePath" -Raw

    $newVersion = [Version]::new("$versionFileContent")

    return $newVersion
}

function UpdateAssemblyVersion {
    Param ([string] $sourcesPath, [string] $version)

    $assemblyInfoPath = $sourcesPath + "\src\AssemblyInfo.Shared.cs"
    $assemblyInfoContent = Get-Content -Path "$assemblyInfoPath" -Raw

    $newAssemblyInfoContent = $assemblyInfoContent `
    -replace 'AssemblyVersion\("\d*\.\d*\.\d*\.\d*"\)', "AssemblyVersion(""$version"")" `
    -replace 'AssemblyFileVersion\("\d*\.\d*\.\d*\.\d*"\)', "AssemblyFileVersion(""$version"")"

    Set-Content -Path "$assemblyInfoPath" -Value $newAssemblyInfoContent

}

function UpdateInstallerVersion {
    Param ([string] $sourcesPath, [string] $version)

    $installerProductPath = $sourcesPath + "\src\Fresenius.FMT.Installer\Product.wxs"
    $installerProductContent = Get-Content -Path "$installerProductPath" -Raw

    $newInstallerProductContent = $installerProductContent `
    -replace 'Version="\d*\.\d*\.\d*\.\d*"', "Version=""$version"""

    Set-Content -Path "$installerProductPath" -Value $newInstallerProductContent

}


class Version
{
    [int32] $Major
    [int32] $Minor
    [int32] $Patch
    [int32] $Build

    Version ([String] $value)
    {
        $match = [regex]::Match($value,'AssemblyVersion\("(\d*)\.(\d*)\.(\d*)\.(\d*)"\)')

        $this.Major = [int]::Parse($match.Groups[1].Value)
        $this.Minor = [int]::Parse($match.Groups[2].Value)
        $this.Patch = [int]::Parse($match.Groups[3].Value)
        $this.Build = [int]::Parse($match.Groups[4].Value)
    }

    IncrementComponent([int32] $component)
    {
        switch ($component)
        {
            1
            {
                $this.Major = $this.Major + 1
                $this.Minor = 0
                $this.Patch = 0
                $this.Build = 0
            }

            2
            {
                $this.Minor = $this.Minor + 1
                $this.Patch = 0
                $this.Build = 0
            }
        
            3
            {
                $this.Patch = $this.Patch + 1
                $this.Build = 0
            }

            4
            {
                $this.Build = $this.Build + 1
            }
        }
    }

    [String] ToString()
    {
        return $this.Major.ToString() + "." + $this.Minor.ToString() + "." + $this.Patch.ToString() + "." + $this.Build.ToString()
    }
}