using Microsoft.EntityFrameworkCore.Migrations;

namespace DBdata.Migrations
{
    public partial class osoblje : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DnevnaPonudas_Osobljes_KuharicaID",
                table: "DnevnaPonudas");

            migrationBuilder.DropForeignKey(
                name: "FK_Obavijests_Osobljes_RecepcioerID",
                table: "Obavijests");

            migrationBuilder.DropForeignKey(
                name: "FK_Obroks_Osobljes_KuharicaID",
                table: "Obroks");

            migrationBuilder.DropForeignKey(
                name: "FK_Osobljes_AspNetUsers_Recepcioer_KorisnikID",
                table: "Osobljes");

            migrationBuilder.DropForeignKey(
                name: "FK_Osobljes_AspNetUsers_Referent_KorisnikID",
                table: "Osobljes");

            migrationBuilder.DropForeignKey(
                name: "FK_Uplatas_Osobljes_RecepcioerID",
                table: "Uplatas");

            migrationBuilder.DropIndex(
                name: "IX_Uplatas_RecepcioerID",
                table: "Uplatas");

            migrationBuilder.DropIndex(
                name: "IX_Osobljes_Recepcioer_KorisnikID",
                table: "Osobljes");

            migrationBuilder.DropIndex(
                name: "IX_Osobljes_Referent_KorisnikID",
                table: "Osobljes");

            migrationBuilder.DropIndex(
                name: "IX_Obroks_KuharicaID",
                table: "Obroks");

            migrationBuilder.DropIndex(
                name: "IX_Obavijests_RecepcioerID",
                table: "Obavijests");

            migrationBuilder.DropIndex(
                name: "IX_DnevnaPonudas_KuharicaID",
                table: "DnevnaPonudas");

            migrationBuilder.DropColumn(
                name: "RecepcioerID",
                table: "Uplatas");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Osobljes");

            migrationBuilder.DropColumn(
                name: "Recepcioer_KorisnikID",
                table: "Osobljes");

            migrationBuilder.DropColumn(
                name: "Referent_KorisnikID",
                table: "Osobljes");

            migrationBuilder.DropColumn(
                name: "KuharicaID",
                table: "Obroks");

            migrationBuilder.DropColumn(
                name: "RecepcioerID",
                table: "Obavijests");

            migrationBuilder.DropColumn(
                name: "KuharicaID",
                table: "DnevnaPonudas");

            migrationBuilder.AddColumn<int>(
                name: "OsobljeID",
                table: "Uplatas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RolaID",
                table: "Osobljes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OsobljeID",
                table: "Obroks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OsobljeID",
                table: "Obavijests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OsobljeID",
                table: "DnevnaPonudas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Rolas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rolas", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Uplatas_OsobljeID",
                table: "Uplatas",
                column: "OsobljeID");

            migrationBuilder.CreateIndex(
                name: "IX_Osobljes_RolaID",
                table: "Osobljes",
                column: "RolaID");

            migrationBuilder.CreateIndex(
                name: "IX_Obroks_OsobljeID",
                table: "Obroks",
                column: "OsobljeID");

            migrationBuilder.CreateIndex(
                name: "IX_Obavijests_OsobljeID",
                table: "Obavijests",
                column: "OsobljeID");

            migrationBuilder.CreateIndex(
                name: "IX_DnevnaPonudas_OsobljeID",
                table: "DnevnaPonudas",
                column: "OsobljeID");

            migrationBuilder.AddForeignKey(
                name: "FK_DnevnaPonudas_Osobljes_OsobljeID",
                table: "DnevnaPonudas",
                column: "OsobljeID",
                principalTable: "Osobljes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Obavijests_Osobljes_OsobljeID",
                table: "Obavijests",
                column: "OsobljeID",
                principalTable: "Osobljes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Obroks_Osobljes_OsobljeID",
                table: "Obroks",
                column: "OsobljeID",
                principalTable: "Osobljes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Osobljes_Rolas_RolaID",
                table: "Osobljes",
                column: "RolaID",
                principalTable: "Rolas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Uplatas_Osobljes_OsobljeID",
                table: "Uplatas",
                column: "OsobljeID",
                principalTable: "Osobljes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DnevnaPonudas_Osobljes_OsobljeID",
                table: "DnevnaPonudas");

            migrationBuilder.DropForeignKey(
                name: "FK_Obavijests_Osobljes_OsobljeID",
                table: "Obavijests");

            migrationBuilder.DropForeignKey(
                name: "FK_Obroks_Osobljes_OsobljeID",
                table: "Obroks");

            migrationBuilder.DropForeignKey(
                name: "FK_Osobljes_Rolas_RolaID",
                table: "Osobljes");

            migrationBuilder.DropForeignKey(
                name: "FK_Uplatas_Osobljes_OsobljeID",
                table: "Uplatas");

            migrationBuilder.DropTable(
                name: "Rolas");

            migrationBuilder.DropIndex(
                name: "IX_Uplatas_OsobljeID",
                table: "Uplatas");

            migrationBuilder.DropIndex(
                name: "IX_Osobljes_RolaID",
                table: "Osobljes");

            migrationBuilder.DropIndex(
                name: "IX_Obroks_OsobljeID",
                table: "Obroks");

            migrationBuilder.DropIndex(
                name: "IX_Obavijests_OsobljeID",
                table: "Obavijests");

            migrationBuilder.DropIndex(
                name: "IX_DnevnaPonudas_OsobljeID",
                table: "DnevnaPonudas");

            migrationBuilder.DropColumn(
                name: "OsobljeID",
                table: "Uplatas");

            migrationBuilder.DropColumn(
                name: "RolaID",
                table: "Osobljes");

            migrationBuilder.DropColumn(
                name: "OsobljeID",
                table: "Obroks");

            migrationBuilder.DropColumn(
                name: "OsobljeID",
                table: "Obavijests");

            migrationBuilder.DropColumn(
                name: "OsobljeID",
                table: "DnevnaPonudas");

            migrationBuilder.AddColumn<int>(
                name: "RecepcioerID",
                table: "Uplatas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Osobljes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Recepcioer_KorisnikID",
                table: "Osobljes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Referent_KorisnikID",
                table: "Osobljes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KuharicaID",
                table: "Obroks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecepcioerID",
                table: "Obavijests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KuharicaID",
                table: "DnevnaPonudas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Uplatas_RecepcioerID",
                table: "Uplatas",
                column: "RecepcioerID");

            migrationBuilder.CreateIndex(
                name: "IX_Osobljes_Recepcioer_KorisnikID",
                table: "Osobljes",
                column: "Recepcioer_KorisnikID",
                unique: true,
                filter: "[Recepcioer_KorisnikID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Osobljes_Referent_KorisnikID",
                table: "Osobljes",
                column: "Referent_KorisnikID",
                unique: true,
                filter: "[Referent_KorisnikID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Obroks_KuharicaID",
                table: "Obroks",
                column: "KuharicaID");

            migrationBuilder.CreateIndex(
                name: "IX_Obavijests_RecepcioerID",
                table: "Obavijests",
                column: "RecepcioerID");

            migrationBuilder.CreateIndex(
                name: "IX_DnevnaPonudas_KuharicaID",
                table: "DnevnaPonudas",
                column: "KuharicaID");

            migrationBuilder.AddForeignKey(
                name: "FK_DnevnaPonudas_Osobljes_KuharicaID",
                table: "DnevnaPonudas",
                column: "KuharicaID",
                principalTable: "Osobljes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Obavijests_Osobljes_RecepcioerID",
                table: "Obavijests",
                column: "RecepcioerID",
                principalTable: "Osobljes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Obroks_Osobljes_KuharicaID",
                table: "Obroks",
                column: "KuharicaID",
                principalTable: "Osobljes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Osobljes_AspNetUsers_Recepcioer_KorisnikID",
                table: "Osobljes",
                column: "Recepcioer_KorisnikID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Osobljes_AspNetUsers_Referent_KorisnikID",
                table: "Osobljes",
                column: "Referent_KorisnikID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Uplatas_Osobljes_RecepcioerID",
                table: "Uplatas",
                column: "RecepcioerID",
                principalTable: "Osobljes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
