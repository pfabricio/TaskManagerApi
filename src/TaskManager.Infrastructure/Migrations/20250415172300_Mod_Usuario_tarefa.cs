using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Mod_Usuario_tarefa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Usuarios_CriadoPor",
                table: "Tarefas");

            migrationBuilder.RenameColumn(
                name: "CriadoPor",
                table: "Tarefas",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Tarefas_CriadoPor",
                table: "Tarefas",
                newName: "IX_Tarefas_UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Usuarios_UsuarioId",
                table: "Tarefas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Usuarios_UsuarioId",
                table: "Tarefas");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Tarefas",
                newName: "CriadoPor");

            migrationBuilder.RenameIndex(
                name: "IX_Tarefas_UsuarioId",
                table: "Tarefas",
                newName: "IX_Tarefas_CriadoPor");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Usuarios_CriadoPor",
                table: "Tarefas",
                column: "CriadoPor",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
