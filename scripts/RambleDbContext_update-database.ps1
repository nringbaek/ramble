$startup_project = "..\src\Ramble.Web\Ramble.Web.csproj"
$project = "..\src\Ramble.Data\Ramble.Data.csproj"

dotnet --version
dotnet ef database update -c RambleDbContext -s $startup_project -p $project

pause "Press any key to continue"