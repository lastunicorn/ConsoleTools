# ==============================================================================
# This file works like a project file. It contains configurations based on which
# the scripts will do the build.
# ------------------------------------------------------------------------------
# Relative paths:
#     If relative paths are used, use the $PSScriptRoot variable to specify a location relative to this config file,
#     instead of using paths relative to the current directory.
#     The script may be called from any other directory than the script's root directory.
# ==============================================================================

$global:config = @{

    # The url of the origin repository.
    RepoUrl = "https://github.com/lastunicorn/ConsoleTools.git"

	# The path to the nuget executable
	NugetPath = "$PSScriptRoot\tools\NuGet.exe"

	# The path to the msbuild executable
	#$msbuild = "c:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe"
	MsbuildPath = "c:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\MSBuild.exe"

	# The path where to download the sources
	SourcesPath = "$PSScriptRoot\~sources"

	# The path where to copy the resulted installer package
	# Ex:
	# $outputPath = ".\releases"
	OutputPath = "\\fscj\Projects\Fresenius FMT\Releases for Test"

    # If a value is specified, it represents the version component to be incremented.
    # To skip the version incrementation, initialize this value with $null or skip it altogether
    # If the Version property is specified, this property is ignored.
    # Possible Values = null, 1, 2, 3, 4
    IncrementVersionComponent = 3

    # If this property is specified, its value is used as the version of the release.
    # If this property is specified, IncrementVersionComponent is ignored.
    # Values pattern: "major.minor.patch.build"
    #Version = "4.2.1.0"
}