@echo off

set soure_directory=..\source\ConsoleTools\bin\Release
set destination_directory=..\nuget\lib\net461

rem ----------------------------------------------------------------------------------------------------
rem Clean up existing files.
rem ----------------------------------------------------------------------------------------------------

echo.
echo ---
echo --- Clean up existing files - lib directory.
echo ---
echo.
if EXIST "%destination_directory%" (
	rmdir /S/Q "%destination_directory%"
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
xcopy /R/Y/S/I "%soure_directory%\*" "%destination_directory%"
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