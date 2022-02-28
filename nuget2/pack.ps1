. .\config.ps1

function PipePropertyTest {
	param
	(
		[parameter(ValueFromPipelineByPropertyName)][int]$intP,
		[parameter(ValueFromPipelineByPropertyName)][string]$stringP
	)
	Write-host "Int: $intP was recieved" -ForegroundColor Green
	Write-host "String: $stringP was recieved" -ForegroundColor magenta
}


if (Test-Path "$packagesDirectory") {
	Remove-Item "$packagesDirectory" -Recurse
}

foreach ($package in $packages) {
	$nuspecExists = $null -ne $package.NuspecPath
	
	if ($nuspecExists) {
		Write-Output "Nuspec file was provided. Try to generate package using the nuspec file."		
		$nuspecFilePath = $package.NuspecPath
		nuget pack "$nuspecFilePath" -OutputDirectory "$packagesDirectory" -Version "$version"
	}
	else {
		$projectExists = $null -ne $package.ProjectPath
		
		if ($projectExists) {
			Write-Output "Project file was specified. Try to generate package using the project file."
			$projectPath = $package.ProjectPath
			dotnet pack --configuration Release --output "$packagesDirectory" "$projectPath"
		}
		else {
			$packageId = $package.PackageId
			Write-Output "Package $packageId could not be generated. Nether nuspec or project files were provided."
		}
	}
}