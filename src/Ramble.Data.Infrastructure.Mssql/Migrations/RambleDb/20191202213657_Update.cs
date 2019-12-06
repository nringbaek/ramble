using Microsoft.EntityFrameworkCore.Migrations;

namespace Ramble.Data.Infrastructure.Mssql.Migrations.RambleDb
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WallId",
                table: "WallEntries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WallEntries_WallId",
                table: "WallEntries",
                column: "WallId");

            migrationBuilder.AddForeignKey(
                name: "FK_WallEntries_Walls_WallId",
                table: "WallEntries",
                column: "WallId",
                principalTable: "Walls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WallEntries_Walls_WallId",
                table: "WallEntries");

            migrationBuilder.DropIndex(
                name: "IX_WallEntries_WallId",
                table: "WallEntries");

            migrationBuilder.DropColumn(
                name: "WallId",
                table: "WallEntries");
        }
    }
}
