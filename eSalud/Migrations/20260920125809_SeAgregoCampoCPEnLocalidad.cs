using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eSalud.Migrations
{
    /// <inheritdoc />
    public partial class SeAgregoCampoCPEnLocalidad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CP",
                table: "Localidades",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CP",
                table: "Localidades");
        }
    }
}
