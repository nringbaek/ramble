using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ramble.Data.Infrastructure.Mssql.Migrations.RambleDb
{
    public partial class AddedFileSupport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EntryContent",
                table: "WallEntries",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EntryType",
                table: "WallEntries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Filename = table.Column<string>(nullable: true),
                    FileLocationId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropColumn(
                name: "EntryContent",
                table: "WallEntries");

            migrationBuilder.DropColumn(
                name: "EntryType",
                table: "WallEntries");
        }
    }
}
