[CmdletBinding()]
param (
    [Parameter()]
    [string]
    $targetDir,
    [Parameter()]
    [string]
    $targetDll,
    [Parameter(Mandatory = $false)]
    [string]
    $ci
)

$isCI = $ci -eq 'true';

if (Test-Path $targetDll) { Rename-Item $targetDll -NewName ggit.gha }
if (!$isCI) { Return }

Set-Location GGit\bin

Get-ChildItem . -Recurse | Where-Object {$_.Extension -ne ".gha" -and $_.Extension -ne ".dll" -and $_ -is [io.fileinfo]} | ForEach-Object {Remove-Item -Force -Path $_.FullName};
Get-ChildItem . | Where-Object {$_.Name -ne "lib" -and $_ -is [io.directoryinfo]} | ForEach-Object {Remove-Item -Force -Recurse -Path $_.FullName};
Compress-Archive -Path. -DestinationPath ggit.zip -CompressionLevel "NoCompression";