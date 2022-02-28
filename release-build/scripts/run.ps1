# ==============================================================================
# 
# ==============================================================================

param([String] $configName = "config")


# ------------------------------------------------------------------------------
# Include additional scripts
# ------------------------------------------------------------------------------

. $PSScriptRoot\logging.ps1
. $PSScriptRoot\configuration.ps1
. $PSScriptRoot\sources-directory.ps1
. $PSScriptRoot\git.ps1
. $PSScriptRoot\versioning.ps1
. $PSScriptRoot\build.ps1
. $PSScriptRoot\release-directory.ps1


# ------------------------------------------------------------------------------
# Run the steps of the script.
# ------------------------------------------------------------------------------

LoadConfig -configName $configName
ValidateConfig -ErrorAction Stop
#Cleanup -path $global:config.SourcesPath
#Clone -repoUrl $global:config.RepoUrl -destinationDirectory $global:config.SourcesPath

#if ($global:config.Version -eq $null)
#{
#    $incrementVersion = ($global:config.IncrementVersionComponent -ne $null) -and ($global:config.IncrementVersionComponent -ne 0)
#
#    if ($incrementVersion -eq $true)
#    {
#        IncrementVersion -sourcesPath $global:config.SourcesPath -versionComponent $global:config.IncrementVersionComponent
#    }
#    else
#    {
#        $version = RetrieveVersion -sourcesPath $global:config.SourcesPath
#        $global:newVersion = $version.ToString()
#    }
#}
#else
#{
#    SetVersion -sourcesPath $global:config.SourcesPath -version $global:config.Version
#}

BuildApplication -sourcesPath $global:config.SourcesPath -nugetPath $global:config.NugetPath -msbuildPath $global:config.MsbuildPath
#PublishOutput -sourcesPath $global:config.SourcesPath -outputPath $global:config.OutputPath -newVersion $global:newVersion
#CommitChanges -sourcesPath $global:config.SourcesPath
#AddGitTag -sourcesPath $global:config.SourcesPath -newVersion $global:newVersion