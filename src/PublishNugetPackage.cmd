@echo off
echo Press any key to publish
pause
".nuget\NuGet.exe" push McStreamy.1.0.0.0.nupkg
pause