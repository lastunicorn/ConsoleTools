
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

Write-Output "---"
Write-Output "--- Retrieve assemblies - net45"
Write-Output "---"

New-Item -ItemType Directory -Force -Path "lib\net45"
Copy-Item -Path "$rootDirectory\sources\ConsoleTools\ConsoleTools\bin\Release\net45\*.dll" -Destination "lib\net45" -Recurse -Container
Copy-Item -Path "$rootDirectory\sources\ConsoleTools\ConsoleTools\bin\Release\net45\*.xml" -Destination "lib\net45" -Recurse -Container

Write-Output "---"
Write-Output "--- Retrieve assemblies - netcoreapp2.2"
Write-Output "---"

New-Item -ItemType Directory -Force -Path "lib\netcoreapp2.2"
Copy-Item -Path "$rootDirectory\sources\ConsoleTools\ConsoleTools\bin\Release\netcoreapp2.2\*.dll" -Destination "lib\netcoreapp2.2" -Recurse -Container
Copy-Item -Path "$rootDirectory\sources\ConsoleTools\ConsoleTools\bin\Release\netcoreapp2.2\*.xml" -Destination "lib\netcoreapp2.2" -Recurse -Container

Write-Output "---"
Write-Output "--- Retrieve changelog file"
Write-Output "---"

Copy-Item -Path "$rootDirectory\doc\changelog.txt" -Destination "." -Recurse -Container

Write-Output "---"
Write-Output "--- Retrieve readme file"
Write-Output "---"

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