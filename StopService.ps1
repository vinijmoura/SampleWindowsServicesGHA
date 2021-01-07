param
(
  [string] $ServiceName
)
  Stop-Service -Name $ServiceName -NoWait
  do { Write-Host "Stopping service $ServiceName" } until ((Get-Service -Name $ServiceName).Status -eq "Stopped" )
