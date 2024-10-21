using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViniciusMiranda.Migrations
{
    /// <inheritdoc />
    public partial class CorrigirImport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImportInss",
                table: "FolhasPagamentos",
                newName: "ImpostoInss");

            migrationBuilder.RenameColumn(
                name: "ImportFgts",
                table: "FolhasPagamentos",
                newName: "ImpostoFgts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImpostoInss",
                table: "FolhasPagamentos",
                newName: "ImportInss");

            migrationBuilder.RenameColumn(
                name: "ImpostoFgts",
                table: "FolhasPagamentos",
                newName: "ImportFgts");
        }
    }
}
