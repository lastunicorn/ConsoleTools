. .\config.ps1
. .\nuget-api-key.ps1

foreach ($package in $packages)
{
	$packageId = $package.PackageId
	$packageFileName = "$packageId.$version.nupkg"
	$packagePath = Join-Path -Path "$packagesDirectory" -ChildPath "$packageFileName"
	
	Write-Output "Publishing package $packagePath..."	
	#dotnet nuget push "$packagePath" --api-key "$apiKey" --source https://api.nuget.org/v3/index.json
}