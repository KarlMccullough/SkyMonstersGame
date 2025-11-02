# PowerShell script to build Unity project locally with Docker
Write-Host "Building Unity project locally with Docker..." -ForegroundColor Green

# Set paths
$unityLicensePath = "C:\ProgramData\Unity\Unity_lic.ulf"
$projectPath = "$PWD\Sky Monsters Game"
$buildPath = "$PWD\build"

# Create build directory if it doesn't exist
if (!(Test-Path $buildPath)) {
    New-Item -ItemType Directory -Path $buildPath
}

# Read Unity license
$unityLicense = Get-Content $unityLicensePath -Raw

# Run Docker build
Write-Host "Starting Docker container..." -ForegroundColor Yellow
docker run --rm `
  -v "${projectPath}:/workspace" `
  -v "${buildPath}:/build" `
  -e "UNITY_LICENSE=$unityLicense" `
  -e "BUILD_TARGET=Android" `
  -e "BUILD_NAME=SkyMonsters" `
  unityci/editor:ubuntu-2020.3.48f1-android-1 `
  /bin/bash -c "cd /workspace && /opt/unity/Editor/Unity -batchmode -quit -projectPath . -buildTarget Android -buildPath /build/SkyMonsters.apk"

Write-Host "Build complete! Check build folder for APK." -ForegroundColor Green