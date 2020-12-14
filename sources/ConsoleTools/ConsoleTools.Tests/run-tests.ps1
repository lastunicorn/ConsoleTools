# ==============================================================================
# This PowerShell script runs the tests and displays in the browser the coverage information.
# ==============================================================================

# In Visual Studio, add in the tests project reference to the "coverlet.msbuild" nuget package. (one time only)

# Run tests and save the code coverage results
dotnet test .\ConsoleTools.Tests.csproj /p:CollectCoverage=true /p:CoverletOutput="./TestResults/coverage.cobertura.xml" /p:CoverletOutputFormat="opencover"

# Install the report generator (one time only):
# dotnet tool install -g dotnet-reportgenerator-globaltool

# Generate the report and display it in browser
reportgenerator -reports:.\TestResults\coverage.cobertura.netcoreapp2.2.xml -reporttypes:"HTML" -targetDir:.\CoverageReport\
start .\CoverageReport\index.html