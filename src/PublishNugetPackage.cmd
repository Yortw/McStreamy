@echo off
echo Press any key to publish
pause
"..\.nuget\NuGet.exe" push mcstreamy.1.0.0.1.nupkg -Source https://api.nuget.org/v3/index.json
pause