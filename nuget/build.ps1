
$rootDirectory = ".."


Write-Output "----------------------------------------------------------------------------------------------------"
Write-Output "Clean up existing files."
Write-Output "----------------------------------------------------------------------------------------------------"

if (Test-Path "lib")
{
    Remove-Item "lib" -Recurse -Force;
}

if (Test-Path "*.nupkg")
{
    Remove-Item "*.nupkg" -Recurse -Force;
}

if (Test-Path "changelog.txt")
{
    Remove-Item "changelog.txt" -Recurse -Force;
}

if (Test-Path "readme.txt")
{
    Remove-Item "readme.txt" -Recurse -Force;
}


Write-Output "----------------------------------------------------------------------------------------------------"
Write-Output "Retrieve all files."
Write-Output "----------------------------------------------------------------------------------------------------"

New-Item -ItemType Directory -Force -Path "lib\net45"
New-Item -ItemType Directory -Force -Path "lib\netcoreapp2.2"

$projectDirs = @(
"$rootDirectory\sources\ConsoleTools\ConsoleTools.Core",
"$rootDirectory\sources\ConsoleTools\ConsoleTools.Controls",
"$rootDirectory\sources\ConsoleTools\ConsoleTools.Controls.Menus"
"$rootDirectory\sources\ConsoleTools\ConsoleTools.Controls.Spinners"
"$rootDirectory\sources\ConsoleTools\ConsoleTools.Controls.Tables"
)

for ($i = 0; $i -lt $projectDirs.length; $i++)
{
	$projectDir = $projectDirs[$i]
	
	Write-Output "---> Retrieve assemblies from project: $projectDir"
	
	Copy-Item -Path "$projectDir\bin\Release\net45\*.dll" -Destination "lib\net45" -Recurse -Container
	Copy-Item -Path "$projectDir\bin\Release\net45\*.xml" -Destination "lib\net45" -Recurse -Container

	Copy-Item -Path "$projectDir\bin\Release\netcoreapp2.2\*.dll" -Destination "lib\netcoreapp2.2" -Recurse -Container
	Copy-Item -Path "$projectDir\bin\Release\netcoreapp2.2\*.xml" -Destination "lib\netcoreapp2.2" -Recurse -Container
	Copy-Item -Path "$projectDir\bin\Release\netcoreapp2.2\*.json" -Destination "lib\netcoreapp2.2" -Recurse -Container
}

Write-Output "---> Retrieve changelog file"
Copy-Item -Path "$rootDirectory\doc\changelog.txt" -Destination "." -Recurse -Container

Write-Output "---> Retrieve readme file"
Copy-Item -Path "$rootDirectory\readme.txt" -Destination "." -Recurse -Container


Write-Output "----------------------------------------------------------------------------------------------------"
Write-Output "Create package"
Write-Output "----------------------------------------------------------------------------------------------------"

nuget pack


Write-Output "----------------------------------------------------------------------------------------------------"
Write-Output "Clean up files."
Write-Output "----------------------------------------------------------------------------------------------------"

if (Test-Path "lib")
{
    Remove-Item "lib" -Recurse -Force;
}

if (Test-Path "changelog.txt")
{
    Remove-Item "changelog.txt" -Recurse -Force;
}

if (Test-Path "readme.txt")
{
    Remove-Item "readme.txt" -Recurse -Force;
}