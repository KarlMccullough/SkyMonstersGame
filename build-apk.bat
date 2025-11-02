@echo off
echo Building Android APK with Unity...

REM Find Unity installation
set UNITY_PATH="C:\Program Files\Unity\Hub\Editor\2020.3.49f1\Editor\Unity.exe"

REM Build APK via command line
%UNITY_PATH% -batchmode -quit -projectPath "Sky Monsters Game" -buildTarget Android -executeMethod BuildScript.BuildAndroid

echo APK build complete!
pause