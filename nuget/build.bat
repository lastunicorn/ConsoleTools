@echo off

set root_directory=..
set assemblies_directory=lib\net461

rem ----------------------------------------------------------------------------------------------------
rem Clean up existing files.
rem ----------------------------------------------------------------------------------------------------

echo.
echo ---
echo --- Clean up existing files - lib directory.
echo ---
echo.
if EXIST "lib" (
	rmdir /S/Q "lib"
	if %errorlevel% neq 0 goto :error
)

echo.
echo ---
echo --- Clean up existing files - .nupkg files.
echo ---
echo.
del *.nupkg
if %errorlevel% neq 0 goto :error

echo.
echo ---
echo --- Retrieve assemblies
echo ---
echo.
xcopy /R/Y/S/I "%root_directory%\source\ConsoleTools\bin\Release\*" "%assemblies_directory%"
if %errorlevel% neq 0 goto :error

echo.
echo ---
echo --- Retrieve changelog file
echo ---
echo.
xcopy /R/Y/S/I "%root_directory%\doc\changelog.txt" .
if %errorlevel% neq 0 goto :error

echo.
echo ---
echo --- Create nuget package
echo ---
echo.
nuget pack

echo.
echo ---
echo --- Delete the uncompressed files
echo ---
echo.
rmdir /S/Q "lib"
if %errorlevel% neq 0 goto :error


:success
echo.
echo.
echo ---
echo ---
echo --- Success
echo ---
echo ---
goto :end

:error
echo.
echo.
echo ---
echo ---
echo --- Error
echo ---
echo ---

:end