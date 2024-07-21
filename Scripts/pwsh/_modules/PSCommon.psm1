
<#
.SYNOPSIS
This function is used to create a backup of a file.

.DESCRIPTION
The Backup-File function creates a backup of the specified file by appending a timestamp to the file name. The backup file will be saved in the same directory as the original file.

.PARAMETER FilePath
The path to the file that needs to be backed up.

.EXAMPLE
Backup-File -FilePath "C:\Path\To\File.txt"
This example creates a backup of the "File.txt" located at "C:\Path\To\" directory.

.NOTES
Author: Ajay Krishna
Date: 21st July 2024
#>
function Backup-File {
    param (
        [Parameter(Mandatory = $true)]
        [ValidateScript({Test-Path $_ -PathType 'Leaf'})]
        [string]$FilePath
    )

    # Split directory path and file name
    $directoryPath = Split-Path -Path $FilePath -Parent
    $fileName = Split-Path -Path $FilePath -Leaf

    # Create backup file name
    $backupPath = $fileName + '.' + (Get-Date -Format 'yyyyMMddHHmmss') + '.bak'

    # Create directory if it does not exist
    if (-not (Test-Path $directoryPath)) {
        New-Item -Path $directoryPath -ItemType Directory
    }

    if (Test-Path $backupPath) {
        Write-Warning "Backup file already exists at $backupPath"
    }
    else {
        Copy-Item -Path $FilePath -Destination $backupPath
        Write-Host "File backed up successfully to $backupPath"
    }
}