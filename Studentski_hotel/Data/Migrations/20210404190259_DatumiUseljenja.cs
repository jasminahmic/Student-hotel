using Microsoft.EntityFrameworkCore.Migrations;

namespace DBdata.Migrations
{
    public partial class DatumiUseljenja : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DatumIseljenja",
                table: "Ugovors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DatumUseljenja",
                table: "Ugovors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatumIseljenja",
                table: "Ugovors");

            migrationBuilder.DropColumn(
                name: "DatumUseljenja",
                table: "Ugovors");
        }
    }
}
