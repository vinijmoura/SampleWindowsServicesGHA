param
(
  [string] $ServiceName
)
        
  Start-Process pwsh -ArgumentList "-c `" & {Stop-Service -Name $ServiceName}`"" -Verb RunAs   
  do { Write-Host "Stopping service $ServiceName" } until ((Get-Service -Name $ServiceName).Status -eq "Stopped" )
