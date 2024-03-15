param ( [Parameter(Mandatory=$true)] [string]$ProjectName )

New-Item -ItemType Directory -Path $ProjectName

# Create README.md
New-Item -ItemType File -Path "$ProjectName\README.md"
$readmeContent = @"
# $ProjectName
- [ ] Configure AppHost by using ``builder.AddProject<$ProjectName_Web>();``
- [ ] Configure $ProjectName.Web Program.cs by calling ``builder.AddServiceDefaults();``
"@
Set-Content -Path "$ProjectName\README.md" -Value $readmeContent

# Create project's folder structure
Set-Location $ProjectName
dotnet new gitignore
dotnet new sln

# Fluent UI Blazor Web
dotnet new fluentblazor --name "$ProjectName.Web"
dotnet sln add "$ProjectName.Web/$ProjectName.Web.csproj"

# Aspire.AppHost
dotnet new aspire-apphost --name "$ProjectName.AppHost"
dotnet sln add "$ProjectName.AppHost/$ProjectName.AppHost.csproj"
dotnet add "$ProjectName.AppHost/$ProjectName.AppHost.csproj" reference "$ProjectName.Web/$ProjectName.Web.csproj"

# Aspire.ServiceDefaults
dotnet new aspire-servicedefaults --name "$ProjectName.ServiceDefaults"
dotnet sln add "$ProjectName.ServiceDefaults/$ProjectName.ServiceDefaults.csproj"
dotnet add "$ProjectName.Web/$ProjectName.Web.csproj" reference "$ProjectName.ServiceDefaults/$ProjectName.ServiceDefaults.csproj"
