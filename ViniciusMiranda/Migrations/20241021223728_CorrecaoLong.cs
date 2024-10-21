using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViniciusMiranda.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoLong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FolhasPagamentos_Funcionarios_FuncionarioId1",
                table: "FolhasPagamentos");

            migrationBuilder.DropIndex(
                name: "IX_FolhasPagamentos_FuncionarioId1",
                table: "FolhasPagamentos");

            migrationBuilder.DropColumn(
                name: "FuncionarioId1",
                table: "FolhasPagamentos");

            migrationBuilder.CreateIndex(
                name: "IX_FolhasPagamentos_FuncionarioId",
                table: "FolhasPagamentos",
                column: "FuncionarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_FolhasPagamentos_Funcionarios_FuncionarioId",
                table: "FolhasPagamentos",
                column: "FuncionarioId",
                principalTable: "Funcionarios",
                principalColumn: "FuncionarioId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FolhasPagamentos_Funcionarios_FuncionarioId",
                table: "FolhasPagamentos");

            migrationBuilder.DropIndex(
                name: "IX_FolhasPagamentos_FuncionarioId",
                table: "FolhasPagamentos");

            migrationBuilder.AddColumn<long>(
                name: "FuncionarioId1",
                table: "FolhasPagamentos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_FolhasPagamentos_FuncionarioId1",
                table: "FolhasPagamentos",
                column: "FuncionarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_FolhasPagamentos_Funcionarios_FuncionarioId1",
                table: "FolhasPagamentos",
                column: "FuncionarioId1",
                principalTable: "Funcionarios",
                principalColumn: "FuncionarioId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
