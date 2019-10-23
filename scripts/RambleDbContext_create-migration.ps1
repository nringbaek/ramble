param (
    [string]$name = $( Read-Host "Migration name" )
)

$startup_project = "..\src\Ramble.Web\Ramble.Web.csproj"
$project = "..\src\Ramble.Data\Ramble.Data.csproj"
$output = "Migrations\RambleDb"

dotnet --version
dotnet ef migrations add $name -c RambleDbContext -s $startup_project -p $project -o $output

pause "Press any key to continue"