# Recursively find all dotnet solution with *.sln extension and build each one
Get-ChildItem -Recurse -Filter *.sln | ForEach-Object { & dotnet build $_.FullName -c Release }
