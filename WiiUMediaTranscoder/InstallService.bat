@echo off
cls
echo Run as administrator
pause

@setlocal enableextensions
@cd /d "%~dp0"

C:\Windows\Microsoft.NET\Framework64\v4.0.30319\InstallUtil.exe Service\bin\Release\Service.exe

pause