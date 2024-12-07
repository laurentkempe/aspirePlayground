# Recursively find all dotnet solution with *.sln extension and build each one
Get-ChildItem -Recurse -Filter *.sln | ForEach-Object {
    Write-Host "Building project:" -NoNewline
    Write-Host " $($_.FullName)" -ForegroundColor DarkBlue
    & dotnet build $_.FullName -c Release
    Write-Host
}