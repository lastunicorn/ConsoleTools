param([String] $configName = "config")


# ==============================================================================
# Ensure logs directory
# ==============================================================================

# The path where to store the log files.
$logsPath = "$PSScriptRoot\~logs"

if (!(Test-Path $logsPath))
{
	New-Item -ItemType "directory" -Path $logsPath
}


# ==============================================================================
# Run the build
# ==============================================================================

$timestamp = Get-Date -f "yyyy MM dd HHmmss"
. $PSScriptRoot\scripts\run.ps1 -configName $configName > "$logsPath\$timestamp.log"