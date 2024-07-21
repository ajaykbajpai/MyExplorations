# Import PSCommon module
Import-Module -Name ./_modules/PSCommon

# Backup host file
Backup-File -FilePath 'C:\Windows\System32\drivers\etc\hosts'