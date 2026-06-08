@echo off


echo =======================================
echo         SooperView Build Options
echo =======================================
echo  1. Debug Build
echo  2. Release Build
echo  3. Release Build with .NET Runtime
echo  4. Archive Current Build
echo =======================================
echo.

set /p CHOICE="Select an option (1-4): "

if "%CHOICE%"=="1" goto build_debug
if "%CHOICE%"=="2" goto build_release
if "%CHOICE%"=="3" goto build_release_single_file
if "%CHOICE%"=="4" goto zip_archive
goto invalid_choice


:build_debug
echo Building SooperView DEBUG
dotnet build SooperView/SooperView/SooperView.csproj -c Debug -o ./Builds
goto build_end

:build_release
echo Building SooperView RELEASE
dotnet publish SooperView/SooperView/SooperView.csproj -c Release /p:DebugType=none -p:PublishDir=../../Builds
goto build_end

:build_release_single_file
echo Building SooperView RELEASE with .NET Runtime
dotnet publish SooperView/SooperView/SooperView.csproj -c Release --self-contained true /p:PublishSingleFile=true /p:EnableCompressionInSingleFile=true /p:DebugType=none -p:PublishDir=../../Builds
goto build_end

:invalid_choice
echo Invalid Choice
goto end

:build_end
:: Download ffmpeg
echo Downloading ffmpeg
mkdir temp
curl -L -o "temp/ffmpeg-windows.zip" "https://github.com/BtbN/FFmpeg-Builds/releases/download/latest/ffmpeg-master-latest-win64-gpl.zip"


:: Unzip ffmpeg to temp folder
echo Unzipping ffmpeg
powershell -Command "$ProgressPreference = 'SilentlyContinue'; Expand-Archive -Path 'temp/ffmpeg-windows.zip' -DestinationPath 'temp' -Force"

:: Moving files
echo Moving ffmpeg to Build
cd Builds
mkdir ffmpeg
cd..
move /Y "temp\ffmpeg-master-latest-win64-gpl\bin\ffmpeg.exe" "Builds\ffmpeg\"
move /Y "temp\ffmpeg-master-latest-win64-gpl\bin\ffprobe.exe" "Builds\ffmpeg\"

:: Remove temp
echo Cleaning up...
rmdir /S /Q "temp"

:: DONE
echo Cleaned up!

::Add option to zip up as SooperView-X.X.X.zip
echo.
echo.
echo.
set /p ZIP_CHOICE="Would you like to zip the build as a release? (y/n)"
if "%ZIP_CHOICE%"=="y" goto zip_archive
if "%ZIP_CHOICE%"=="Y" goto zip_archive
goto zip_end

:zip_archive
powershell -Command "$root=(Get-Item './Builds').FullName; $exe=(Get-Item './Builds/SooperView.exe'); $v=$exe.VersionInfo.FileVersion; $dst=\"./SooperView-$v.zip\"; if(Test-Path $dst){Remove-Item $dst -Force}; [System.Reflection.Assembly]::LoadWithPartialName('System.IO.Compression.FileSystem') | Out-Null; $zip=[System.IO.Compression.ZipFile]::Open($dst, 'Create'); $files=Get-ChildItem './Builds' -Recurse | Where-Object {!$_.PSIsContainer}; $i=0; foreach($f in $files){$i++; $p=[math]::Round(($i/$files.Count)*100); Write-Host -NoNewLine \"`rArchiving: $p%%\"; $rel=$f.FullName.Replace($root, '').TrimStart('\\'); [System.IO.Compression.ZipFileExtensions]::CreateEntryFromFile($zip, $f.FullName, $rel, 'Optimal') | Out-Null}; $zip.Dispose(); Write-Host ' Done!'"
goto zip_end

:zip_end
echo Build Complete!

:end