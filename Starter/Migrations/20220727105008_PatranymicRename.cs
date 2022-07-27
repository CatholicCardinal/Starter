using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Starter.Migrations
{
    public partial class PatranymicRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Patranomic",
                table: "Records",
                newName: "Patronymic");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Patronymic",
                table: "Records",
                newName: "Patranomic");
        }
    }
}
