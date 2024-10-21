using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViniciusMiranda.Migrations
{
    /// <inheritdoc />
    public partial class FolhasModificado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ImportFgts",
                table: "FolhasPagamentos",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ImportInss",
                table: "FolhasPagamentos",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ImpostoIrrf",
                table: "FolhasPagamentos",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SalarioBruto",
                table: "FolhasPagamentos",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SalarioLiquido",
                table: "FolhasPagamentos",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImportFgts",
                table: "FolhasPagamentos");

            migrationBuilder.DropColumn(
                name: "ImportInss",
                table: "FolhasPagamentos");

            migrationBuilder.DropColumn(
                name: "ImpostoIrrf",
                table: "FolhasPagamentos");

            migrationBuilder.DropColumn(
                name: "SalarioBruto",
                table: "FolhasPagamentos");

            migrationBuilder.DropColumn(
                name: "SalarioLiquido",
                table: "FolhasPagamentos");
        }
    }
}
