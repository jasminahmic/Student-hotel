using Microsoft.EntityFrameworkCore.Migrations;

namespace DBdata.Migrations
{
    public partial class RazlogBlackLista : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RazlogZaBlackListu",
                table: "Students",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RazlogZaBlackListu",
                table: "Students");
        }
    }
}
