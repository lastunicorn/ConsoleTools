@echo off

set root_directory=..
set version=0.2.0

rem ----------------------------------------------------------------------------------------------------
rem Clean up existing files.
rem ----------------------------------------------------------------------------------------------------

echo.
echo ---
echo --- Clenup existing files - ConsoleTools directoty
echo ---
echo.
if EXIST "ConsoleTools" (
	rmdir /S/Q "ConsoleTools"
	if %errorlevel% neq 0 goto :error
)

echo.
echo ---
echo --- Clean up existing files - package file (7z file).
echo ---
echo.
del ConsoleTools*.7z
if %errorlevel% neq 0 goto :error

rem ----------------------------------------------------------------------------------------------------
rem Retrieve assemblies.
rem ----------------------------------------------------------------------------------------------------

echo.
echo ---
echo --- Retrieve assemblies
echo ---
echo.
xcopy /Y/S/I "%root_directory%\source\ConsoleTools\bin\Release-Net461\*" "ConsoleTools\lib\Net461"
if %errorlevel% neq 0 goto :error

echo.
echo ---
echo --- Retrieve assemblies
echo ---
echo.
xcopy /Y/S/I "%root_directory%\source\ConsoleTools\bin\Release-Net45\*" "ConsoleTools\lib\Net45"
if %errorlevel% neq 0 goto :error

rem ----------------------------------------------------------------------------------------------------
rem Retrieve license file.
rem ----------------------------------------------------------------------------------------------------

echo.
echo ---
echo --- Retrieve license file
echo ---
echo.
xcopy /Y "%root_directory%\LICENSE" "ConsoleTools\*"
if %errorlevel% neq 0 goto :error

rem ----------------------------------------------------------------------------------------------------
rem Retrieve readme file.
rem ----------------------------------------------------------------------------------------------------

echo.
echo ---
echo --- Retrieve readme file
echo ---
echo.
xcopy /Y "%root_directory%\readme.txt" "ConsoleTools\*"
if %errorlevel% neq 0 goto :error

rem ----------------------------------------------------------------------------------------------------
rem Retrieve changelog file.
rem ----------------------------------------------------------------------------------------------------

echo.
echo ---
echo --- Retrieve changelog file
echo ---
echo.
xcopy /Y "%root_directory%\doc\changelog.txt" "ConsoleTools\*"
if %errorlevel% neq 0 goto :error

rem ----------------------------------------------------------------------------------------------------
rem Zip
rem ----------------------------------------------------------------------------------------------------

rem 7-Zip returns the following exit codes:
rem 
rem Code Meaning 
rem 0 No error 
rem 1 Warning (Non fatal error(s)). For example, one or more files were locked by some other application, so they were not compressed. 
rem 2 Fatal error 
rem 7 Command line error 
rem 8 Not enough memory for operation 
rem 255 User stopped the process 

echo.
echo ---
echo --- Create the zip package
echo ---
echo.
7z a "ConsoleTools-v%version%.7z" "ConsoleTools"
if %errorlevel% neq 0 goto :error

echo.
echo ---
echo --- Test the zip package
echo ---
echo.
7z t "ConsoleTools-v%version%.7z"
if %errorlevel% neq 0 goto :error

rem ----------------------------------------------------------------------------------------------------
rem Clean up
rem ----------------------------------------------------------------------------------------------------

echo.
echo ---
echo --- Delete the uncompressed files
echo ---
echo.
rmdir /S/Q "ConsoleTools"
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