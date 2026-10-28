using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eSalud.Migrations
{
    /// <inheritdoc />
    public partial class MigracionAdministradores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CP",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<DateOnly>(
                name: "FechaIngreso",
                table: "Legajos",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AlterColumn<bool>(
                name: "Activo",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaIngreso",
                table: "Legajos");

            migrationBuilder.AlterColumn<bool>(
                name: "Activo",
                table: "AspNetUsers",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "CP",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
