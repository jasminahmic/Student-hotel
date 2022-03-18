using Microsoft.EntityFrameworkCore.Migrations;

namespace DBdata.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlackListID",
                table: "Students",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Students_BlackListID",
                table: "Students",
                column: "BlackListID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_BlackLists_BlackListID",
                table: "Students",
                column: "BlackListID",
                principalTable: "BlackLists",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_BlackLists_BlackListID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_BlackListID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "BlackListID",
                table: "Students");
        }
    }
}
