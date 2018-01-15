@echo off

set root_directory=..

rem ----------------------------------------------------------------------------------------------------
rem Clean up existing files.
rem ----------------------------------------------------------------------------------------------------

echo.
echo ---
echo --- Clean up existing files - lib directory
echo ---
echo.
if EXIST "lib" (
	rmdir /S/Q "lib"
	if %errorlevel% neq 0 goto :error
)

echo.
echo ---
echo --- Clean up existing files - .nupkg files
echo ---
echo.
if EXIST "*.nupkg" (
	del *.nupkg
	if %errorlevel% neq 0 goto :error
)

echo.
echo ---
echo --- Clean up existing files - changelog file
echo ---
echo.
if EXIST "changelog.txt" (
	del "changelog.txt"
	if %errorlevel% neq 0 goto :error
)

echo.
echo ---
echo --- Clean up existing files - readme file
echo ---
echo.
if EXIST "readme.txt" (
	del "readme.txt"
	if %errorlevel% neq 0 goto :error
)

rem ----------------------------------------------------------------------------------------------------
rem Retrieve all files.
rem ----------------------------------------------------------------------------------------------------

echo.
echo ---
echo --- Retrieve assemblies - Net461
echo ---
echo.
xcopy /R/Y/S/I "%root_directory%\source\ConsoleTools\bin\Release-Net461\*" "lib\net461"
if %errorlevel% neq 0 goto :error

echo.
echo ---
echo --- Retrieve assemblies - Net45
echo ---
echo.
xcopy /R/Y/S/I "%root_directory%\source\ConsoleTools\bin\Release-Net45\*" "lib\net45"
if %errorlevel% neq 0 goto :error

echo.
echo ---
echo --- Retrieve changelog file
echo ---
echo.
xcopy /Y "%root_directory%\doc\changelog.txt" .
if %errorlevel% neq 0 goto :error

echo.
echo ---
echo --- Retrieve readme file
echo ---
echo.
xcopy /Y "%root_directory%\readme.txt" .
if %errorlevel% neq 0 goto :error

rem ----------------------------------------------------------------------------------------------------
rem Create package
rem ----------------------------------------------------------------------------------------------------

echo.
echo ---
echo --- Create nuget package
echo ---
echo.
nuget pack

rem ----------------------------------------------------------------------------------------------------
rem Clean up files.
rem ----------------------------------------------------------------------------------------------------

echo.
echo ---
echo --- Clean up files - lib directory
echo ---
echo.
rmdir /S/Q "lib"
if %errorlevel% neq 0 goto :error

echo.
echo ---
echo --- Clean up files - changelog file
echo ---
echo.
del "changelog.txt"
if %errorlevel% neq 0 goto :error

echo.
echo ---
echo --- Clean up files - readme file
echo ---
echo.
del "readme.txt"
if %errorlevel% neq 0 goto :error

rem ----------------------------------------------------------------------------------------------------
rem Success
rem ----------------------------------------------------------------------------------------------------

:success
echo.
echo.
echo ---
echo ---
echo --- Success
echo ---
echo ---
goto :end

rem ----------------------------------------------------------------------------------------------------
rem Error
rem ----------------------------------------------------------------------------------------------------

:error
echo.
echo.
echo ---
echo ---
echo --- Error
echo ---
echo ---

rem ----------------------------------------------------------------------------------------------------
rem End
rem ----------------------------------------------------------------------------------------------------

:end