using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eSalud.Migrations
{
    /// <inheritdoc />
    public partial class CorrexionTablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicoEspecialidad_Especialidad_EspecialidadId",
                table: "MedicoEspecialidad");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicoEspecialidad_Medicos_MedicoId",
                table: "MedicoEspecialidad");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicoEspecialidad",
                table: "MedicoEspecialidad");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Especialidad",
                table: "Especialidad");

            migrationBuilder.RenameTable(
                name: "MedicoEspecialidad",
                newName: "MedicoEspecialidades");

            migrationBuilder.RenameTable(
                name: "Especialidad",
                newName: "Especialidades");

            migrationBuilder.RenameIndex(
                name: "IX_MedicoEspecialidad_MedicoId",
                table: "MedicoEspecialidades",
                newName: "IX_MedicoEspecialidades_MedicoId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicoEspecialidad_EspecialidadId",
                table: "MedicoEspecialidades",
                newName: "IX_MedicoEspecialidades_EspecialidadId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicoEspecialidades",
                table: "MedicoEspecialidades",
                column: "MedicoEspecialidadId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Especialidades",
                table: "Especialidades",
                column: "EspecialidadId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicoEspecialidades_Especialidades_EspecialidadId",
                table: "MedicoEspecialidades",
                column: "EspecialidadId",
                principalTable: "Especialidades",
                principalColumn: "EspecialidadId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicoEspecialidades_Medicos_MedicoId",
                table: "MedicoEspecialidades",
                column: "MedicoId",
                principalTable: "Medicos",
                principalColumn: "MedicoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicoEspecialidades_Especialidades_EspecialidadId",
                table: "MedicoEspecialidades");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicoEspecialidades_Medicos_MedicoId",
                table: "MedicoEspecialidades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicoEspecialidades",
                table: "MedicoEspecialidades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Especialidades",
                table: "Especialidades");

            migrationBuilder.RenameTable(
                name: "MedicoEspecialidades",
                newName: "MedicoEspecialidad");

            migrationBuilder.RenameTable(
                name: "Especialidades",
                newName: "Especialidad");

            migrationBuilder.RenameIndex(
                name: "IX_MedicoEspecialidades_MedicoId",
                table: "MedicoEspecialidad",
                newName: "IX_MedicoEspecialidad_MedicoId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicoEspecialidades_EspecialidadId",
                table: "MedicoEspecialidad",
                newName: "IX_MedicoEspecialidad_EspecialidadId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicoEspecialidad",
                table: "MedicoEspecialidad",
                column: "MedicoEspecialidadId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Especialidad",
                table: "Especialidad",
                column: "EspecialidadId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicoEspecialidad_Especialidad_EspecialidadId",
                table: "MedicoEspecialidad",
                column: "EspecialidadId",
                principalTable: "Especialidad",
                principalColumn: "EspecialidadId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicoEspecialidad_Medicos_MedicoId",
                table: "MedicoEspecialidad",
                column: "MedicoId",
                principalTable: "Medicos",
                principalColumn: "MedicoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
