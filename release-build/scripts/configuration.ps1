# ==============================================================================
# Load the configuration
# ==============================================================================

function LoadConfig {
    Param ([string] $configName)

    if ($configName -eq $null)
    {
        throw "No configuration was provided. A configuration is the name of the configuration file without the '.ps1' extension."
    }
    else
    {
        try
        {
            # 1) Search in the current directory for the config file.
            # Works also if the path is absolute.

            $configFilePath = Resolve-Path -Path $configName -ErrorAction Stop
        }
        catch
        {
            $configNameIsPs1File = $configName.EndsWith(".ps1");

            if ($configNameIsPs1File)
            {
                throw "The specified configuration was not found. Configuration: $configName"
            }

            try
            {
                # 2) Search in the current directory for the config file with added ps1 extension.
            
                $configNameWithPs1 = $configName + ".ps1"
                $configFilePath = Resolve-Path -Path $configNameWithPs1 -ErrorAction Stop
            }
            catch
            {
                throw "The specified configuration was not found. Configuration: $configName"
            }
        }
    
        # Load the configuration
        . $configFilePath
    }

}

# ==============================================================================
# Validate Config values
# ------------------------------------------------------------------------------
# Checks if the files specified in the config file exists.
# ==============================================================================

function ValidateConfig {

    WriteLabel "Validate Config - Validating the existence of the third party tools needed for build."

    if (!(Test-Path $global:config.NugetPath))
    {
	    Write-Error -Message ("The nuget file does not exist at the specified location. Location: " + $global:config.NugetPath) -ErrorAction Stop
    }

    if (!(Test-Path $global:config.MsbuildPath))
    {
	    Write-Error -Message ("The msbuild file does not exist at the specified location. Location: " + $global:config.MsbuildPath) -ErrorAction Stop
    }

    if(($global:config.IncrementVersion -ne $null) -and (($global:config.IncrementVersion -lt 1) -or ($global:config.IncrementVersion -gt 4)))
    {
        Write-Error -Message ("Invalid value for IncrementVersion . Value: " + $global:config.IncrementVersion) -ErrorAction Stop
    }

}