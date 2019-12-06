param (
    [string]$name = $( Read-Host "Migration name" )
)

$startup_project = "..\src\Ramble.Data.Infrastructure.Mssql\Ramble.Data.Infrastructure.Mssql.csproj"
$output = "Migrations\RambleDb"

dotnet --version
dotnet ef migrations add $name -c RambleDbContext -s $startup_project -o $output --verbose

pause "Press any key to continue"