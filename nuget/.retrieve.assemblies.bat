@echo off

set soure_directory=..\source\ConsoleTools\bin\Release
set destination_directory=..\nuget\lib\net461

echo Delete destination directoty
rmdir /S/Q "%destination_directory%"

echo Retrieve assemblies
xcopy /R/Y/S/I "%soure_directory%\*" "%destination_directory%"