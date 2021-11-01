. .\config.ps1

if (Test-Path "$packagesDirectory")
{
	Remove-Item "$packagesDirectory" -Recurse
}

foreach ($package in $packages)
{
	$nuspecExists = $package.NuspecPath -ne $null
	
	if ($nuspecExists)
	{
		Write-Output "Nuspec file was specified. Try to generate package using the nuspec file."		
		$nuspecFilePath = $package.NuspecPath
		nuget pack "$nuspecFilePath" -OutputDirectory "$packagesDirectory" -Version "$version"
	}
	else
	{
		$projectExists = $package.ProjectPath -ne $null
		
		if ($projectExists)
		{
			Write-Output "Project file was specified. Try to generate package using the project file."
			$projectPath = $package.ProjectPath
			dotnet pack --configuration Release --output "$packagesDirectory" "$projectPath"
		}
		else
		{
			$packageId = $package.PackageId
			Write-Output "Package $packageId could not be generated. Nether nuspec or project files were provided."
		}
	}
}