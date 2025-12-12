$processesToRemove = Get-Content -Path ($PSScriptRoot + "\remove.txt")
$processesToRemove = $processesToRemove + (Get-Content -Path ($PSScriptRoot + "\alwaysremove.txt"))
$processesToKeep = Get-Content -Path ($PSScriptRoot + "\alwayskeep.txt")
$runOncePath = "HKCU:\Software\Microsoft\Windows\CurrentVersion\RunOnce"

foreach ($processToRemove in $processesToRemove)
{
    if (Test-Path $runOncePath)
    {
        $valueNames = (Get-Item $runOncePath).Property

        foreach ($name in $valueNames | Where-Object { $_ -like 'Application Restart*' }) {
            $data = (Get-ItemProperty -Path $runOncePath -Name $name).$name
            $remove = $true
            if ($data -is [string] -and $data -match $processToRemove -and $processToRemove -notin $processesToKeep) {
                Remove-ItemProperty -Path $runOncePath -Name $name -ErrorAction SilentlyContinue
            }
        }
    }
}