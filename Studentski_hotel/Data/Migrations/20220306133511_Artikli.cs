using Microsoft.EntityFrameworkCore.Migrations;

namespace DBdata.Migrations
{
    public partial class Artikli : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Detalji",
                table: "Obroks");

            migrationBuilder.CreateTable(
                name: "Artikals",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivArtikla = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artikals", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ArtikalCijenas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cijena = table.Column<float>(nullable: false),
                    ArtikalID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtikalCijenas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ArtikalCijenas_Artikals_ArtikalID",
                        column: x => x.ArtikalID,
                        principalTable: "Artikals",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArtikalObroks",
                columns: table => new
                {
                    ObrokID = table.Column<int>(nullable: false),
                    ArtikalID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtikalObroks", x => new { x.ArtikalID, x.ObrokID });
                    table.ForeignKey(
                        name: "FK_ArtikalObroks_Artikals_ArtikalID",
                        column: x => x.ArtikalID,
                        principalTable: "Artikals",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ArtikalObroks_Obroks_ObrokID",
                        column: x => x.ObrokID,
                        principalTable: "Obroks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtikalCijenas_ArtikalID",
                table: "ArtikalCijenas",
                column: "ArtikalID");

            migrationBuilder.CreateIndex(
                name: "IX_ArtikalObroks_ObrokID",
                table: "ArtikalObroks",
                column: "ObrokID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtikalCijenas");

            migrationBuilder.DropTable(
                name: "ArtikalObroks");

            migrationBuilder.DropTable(
                name: "Artikals");

            migrationBuilder.AddColumn<string>(
                name: "Detalji",
                table: "Obroks",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
