# Publish script — creates self-contained win-x64 executable
# Usage: .\publish.ps1 [-Runtime win-x64|win-x86]

param(
    [string]$Runtime = "win-x64"
)

$ErrorActionPreference = "Stop"
$ProjectPath = "src\HeChuyenGiaThuocBenh.UI\HeChuyenGiaThuocBenh.UI.csproj"
$OutputDir = "publish\$Runtime"

Write-Host "Building $Runtime release..." -ForegroundColor Cyan

dotnet publish $ProjectPath `
    --configuration Release `
    --runtime $Runtime `
    --self-contained true `
    --output $OutputDir `
    /p:PublishSingleFile=true `
    /p:IncludeNativeLibrariesForSelfExtract=true

if ($LASTEXITCODE -ne 0) {
    Write-Host "Publish FAILED (exit $LASTEXITCODE)" -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host ""
Write-Host "Published to: $OutputDir" -ForegroundColor Green
Write-Host "Executable:   $OutputDir\HeChuyenGiaThuocBenh.UI.exe" -ForegroundColor Green
Write-Host ""
Write-Host "Before running, ensure SQL Server is accessible and update appsettings.json if needed."
