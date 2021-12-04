using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DBdata.Migrations
{
    public partial class Studentski_hotel_Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CiklusStudijas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CiklusStudijas", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Drzavas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drzavas", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Fakultets",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fakultets", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GodinaStudijas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GodinaStudijas", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Kantons",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kantons", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Karticas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojKartice = table.Column<string>(nullable: true),
                    StanjeNaKartici = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Karticas", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NacinUplates",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NacinUplates", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Pols",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pols", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Sobas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sprat = table.Column<string>(nullable: true),
                    BrojSobe = table.Column<string>(nullable: true),
                    BrojKreveta = table.Column<int>(nullable: false),
                    ImaBalkon = table.Column<bool>(nullable: false),
                    Napomena = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sobas", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tipKandidatas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipKandidatas", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "vrstaRazlogaOdbijanjas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vrstaRazlogaOdbijanjas", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "VrstaStanjaKonkursas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrstaStanjaKonkursas", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "VrstaStanjaZahtjevas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrstaStanjaZahtjevas", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "VrstaZahtjevas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrstaZahtjevas", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Grads",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    KantonID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grads", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Grads_Kantons_KantonID",
                        column: x => x.KantonID,
                        principalTable: "Kantons",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RezultatKonkursas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojBodova = table.Column<int>(nullable: false),
                    VrstaStanjaKonkursaID = table.Column<int>(nullable: true),
                    VrstaRazlogaOdbijanjaID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RezultatKonkursas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RezultatKonkursas_vrstaRazlogaOdbijanjas_VrstaRazlogaOdbijanjaID",
                        column: x => x.VrstaRazlogaOdbijanjaID,
                        principalTable: "vrstaRazlogaOdbijanjas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RezultatKonkursas_VrstaStanjaKonkursas_VrstaStanjaKonkursaID",
                        column: x => x.VrstaStanjaKonkursaID,
                        principalTable: "VrstaStanjaKonkursas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lokacijas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adresa = table.Column<string>(nullable: true),
                    PostanskiBroj = table.Column<string>(nullable: true),
                    MjestoStanovanjaID = table.Column<int>(nullable: false),
                    KantonID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lokacijas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Lokacijas_Kantons_KantonID",
                        column: x => x.KantonID,
                        principalTable: "Kantons",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lokacijas_Grads_MjestoStanovanjaID",
                        column: x => x.MjestoStanovanjaID,
                        principalTable: "Grads",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Osobljes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    DatumRodjenja = table.Column<string>(nullable: true),
                    GodinaZaposlenja = table.Column<string>(nullable: true),
                    DatumZaposlenja = table.Column<string>(nullable: true),
                    LokacijaID = table.Column<int>(nullable: false),
                    PolID = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    KorisnikID = table.Column<string>(nullable: true),
                    Recepcioer_KorisnikID = table.Column<string>(nullable: true),
                    Referent_KorisnikID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Osobljes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Osobljes_AspNetUsers_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Osobljes_Lokacijas_LokacijaID",
                        column: x => x.LokacijaID,
                        principalTable: "Lokacijas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Osobljes_Pols_PolID",
                        column: x => x.PolID,
                        principalTable: "Pols",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Osobljes_AspNetUsers_Recepcioer_KorisnikID",
                        column: x => x.Recepcioer_KorisnikID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Osobljes_AspNetUsers_Referent_KorisnikID",
                        column: x => x.Referent_KorisnikID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    ImeOca = table.Column<string>(nullable: true),
                    MjestoRodjenjaID = table.Column<int>(nullable: false),
                    ZanimanjeRoditelja = table.Column<string>(nullable: true),
                    PolID = table.Column<int>(nullable: false),
                    JMBG = table.Column<string>(nullable: true),
                    LicnaKarta = table.Column<string>(nullable: true),
                    DatumRodjenja = table.Column<string>(nullable: true),
                    Mobitel = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FakultetID = table.Column<int>(nullable: false),
                    TipKandidataID = table.Column<int>(nullable: false),
                    BrojIndeksa = table.Column<string>(nullable: true),
                    CiklusStudijaID = table.Column<int>(nullable: false),
                    GodinaStudijaID = table.Column<int>(nullable: false),
                    LokacijaID = table.Column<int>(nullable: false),
                    Uselio = table.Column<bool>(nullable: false),
                    KorisnikID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Students_CiklusStudijas_CiklusStudijaID",
                        column: x => x.CiklusStudijaID,
                        principalTable: "CiklusStudijas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_Fakultets_FakultetID",
                        column: x => x.FakultetID,
                        principalTable: "Fakultets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_GodinaStudijas_GodinaStudijaID",
                        column: x => x.GodinaStudijaID,
                        principalTable: "GodinaStudijas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_AspNetUsers_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_Lokacijas_LokacijaID",
                        column: x => x.LokacijaID,
                        principalTable: "Lokacijas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_Grads_MjestoRodjenjaID",
                        column: x => x.MjestoRodjenjaID,
                        principalTable: "Grads",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_Pols_PolID",
                        column: x => x.PolID,
                        principalTable: "Pols",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_tipKandidatas_TipKandidataID",
                        column: x => x.TipKandidataID,
                        principalTable: "tipKandidatas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DnevnaPonudas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tekst = table.Column<string>(nullable: true),
                    Datum = table.Column<string>(nullable: true),
                    KuharicaID = table.Column<int>(nullable: false),
                    KarticaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DnevnaPonudas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DnevnaPonudas_Karticas_KarticaID",
                        column: x => x.KarticaID,
                        principalTable: "Karticas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DnevnaPonudas_Osobljes_KuharicaID",
                        column: x => x.KuharicaID,
                        principalTable: "Osobljes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Obavijests",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naslov = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    DatumVrijeme = table.Column<string>(nullable: true),
                    RecepcioerID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obavijests", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Obavijests_Osobljes_RecepcioerID",
                        column: x => x.RecepcioerID,
                        principalTable: "Osobljes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Obroks",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<string>(nullable: true),
                    Iznos = table.Column<float>(nullable: false),
                    Detalji = table.Column<string>(nullable: true),
                    KuharicaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obroks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Obroks_Osobljes_KuharicaID",
                        column: x => x.KuharicaID,
                        principalTable: "Osobljes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Konkurs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    ImeOca = table.Column<string>(nullable: true),
                    MjestoRodjenjaID = table.Column<int>(nullable: false),
                    ZanimanjeRoditelja = table.Column<string>(nullable: true),
                    PolID = table.Column<int>(nullable: false),
                    JMBG = table.Column<string>(nullable: true),
                    LicnaKarta = table.Column<string>(nullable: true),
                    Mjesto_izdavanja_LK = table.Column<string>(nullable: true),
                    DatumRodjenja = table.Column<string>(nullable: true),
                    Mobitel = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Adresa = table.Column<string>(nullable: true),
                    MjestoStanovanjaID = table.Column<int>(nullable: false),
                    KantonID = table.Column<int>(nullable: false),
                    FakultetID = table.Column<int>(nullable: false),
                    TipKandidataID = table.Column<int>(nullable: false),
                    BrojIndeksa = table.Column<string>(nullable: true),
                    CiklusStudijaID = table.Column<int>(nullable: false),
                    GodinaStudijaID = table.Column<int>(nullable: false),
                    StudentID = table.Column<int>(nullable: true),
                    RezultatKonkursaID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Konkurs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Konkurs_CiklusStudijas_CiklusStudijaID",
                        column: x => x.CiklusStudijaID,
                        principalTable: "CiklusStudijas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Konkurs_Fakultets_FakultetID",
                        column: x => x.FakultetID,
                        principalTable: "Fakultets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Konkurs_GodinaStudijas_GodinaStudijaID",
                        column: x => x.GodinaStudijaID,
                        principalTable: "GodinaStudijas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Konkurs_Kantons_KantonID",
                        column: x => x.KantonID,
                        principalTable: "Kantons",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Konkurs_Grads_MjestoRodjenjaID",
                        column: x => x.MjestoRodjenjaID,
                        principalTable: "Grads",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Konkurs_Grads_MjestoStanovanjaID",
                        column: x => x.MjestoStanovanjaID,
                        principalTable: "Grads",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Konkurs_Pols_PolID",
                        column: x => x.PolID,
                        principalTable: "Pols",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Konkurs_RezultatKonkursas_RezultatKonkursaID",
                        column: x => x.RezultatKonkursaID,
                        principalTable: "RezultatKonkursas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Konkurs_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Konkurs_tipKandidatas_TipKandidataID",
                        column: x => x.TipKandidataID,
                        principalTable: "tipKandidatas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ugovors",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SobaID = table.Column<int>(nullable: false),
                    StudentID = table.Column<int>(nullable: false),
                    KarticaID = table.Column<int>(nullable: false),
                    DodanUgovorOsobljeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ugovors", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ugovors_Osobljes_DodanUgovorOsobljeID",
                        column: x => x.DodanUgovorOsobljeID,
                        principalTable: "Osobljes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ugovors_Karticas_KarticaID",
                        column: x => x.KarticaID,
                        principalTable: "Karticas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ugovors_Sobas_SobaID",
                        column: x => x.SobaID,
                        principalTable: "Sobas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ugovors_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NajavaOdlaskas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumPolaska = table.Column<string>(nullable: true),
                    DatumPovratka = table.Column<string>(nullable: true),
                    UgovorID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NajavaOdlaskas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NajavaOdlaskas_Ugovors_UgovorID",
                        column: x => x.UgovorID,
                        principalTable: "Ugovors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Uplatas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<string>(nullable: true),
                    Stanje = table.Column<float>(nullable: false),
                    NacinUplateID = table.Column<int>(nullable: false),
                    RecepcioerID = table.Column<int>(nullable: false),
                    UgovorID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uplatas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Uplatas_NacinUplates_NacinUplateID",
                        column: x => x.NacinUplateID,
                        principalTable: "NacinUplates",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Uplatas_Osobljes_RecepcioerID",
                        column: x => x.RecepcioerID,
                        principalTable: "Osobljes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Uplatas_Ugovors_UgovorID",
                        column: x => x.UgovorID,
                        principalTable: "Ugovors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Upozorenjes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumSlanja = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    UgovorID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Upozorenjes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Upozorenjes_Ugovors_UgovorID",
                        column: x => x.UgovorID,
                        principalTable: "Ugovors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Zahtjevs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    VrstaZahtjevaID = table.Column<int>(nullable: false),
                    VrstaStanjaZahtjevaID = table.Column<int>(nullable: false),
                    UgovorID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zahtjevs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Zahtjevs_Ugovors_UgovorID",
                        column: x => x.UgovorID,
                        principalTable: "Ugovors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zahtjevs_VrstaStanjaZahtjevas_VrstaStanjaZahtjevaID",
                        column: x => x.VrstaStanjaZahtjevaID,
                        principalTable: "VrstaStanjaZahtjevas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zahtjevs_VrstaZahtjevas_VrstaZahtjevaID",
                        column: x => x.VrstaZahtjevaID,
                        principalTable: "VrstaZahtjevas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DnevnaPonudas_KarticaID",
                table: "DnevnaPonudas",
                column: "KarticaID");

            migrationBuilder.CreateIndex(
                name: "IX_DnevnaPonudas_KuharicaID",
                table: "DnevnaPonudas",
                column: "KuharicaID");

            migrationBuilder.CreateIndex(
                name: "IX_Grads_KantonID",
                table: "Grads",
                column: "KantonID");

            migrationBuilder.CreateIndex(
                name: "IX_Konkurs_CiklusStudijaID",
                table: "Konkurs",
                column: "CiklusStudijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Konkurs_FakultetID",
                table: "Konkurs",
                column: "FakultetID");

            migrationBuilder.CreateIndex(
                name: "IX_Konkurs_GodinaStudijaID",
                table: "Konkurs",
                column: "GodinaStudijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Konkurs_KantonID",
                table: "Konkurs",
                column: "KantonID");

            migrationBuilder.CreateIndex(
                name: "IX_Konkurs_MjestoRodjenjaID",
                table: "Konkurs",
                column: "MjestoRodjenjaID");

            migrationBuilder.CreateIndex(
                name: "IX_Konkurs_MjestoStanovanjaID",
                table: "Konkurs",
                column: "MjestoStanovanjaID");

            migrationBuilder.CreateIndex(
                name: "IX_Konkurs_PolID",
                table: "Konkurs",
                column: "PolID");

            migrationBuilder.CreateIndex(
                name: "IX_Konkurs_RezultatKonkursaID",
                table: "Konkurs",
                column: "RezultatKonkursaID");

            migrationBuilder.CreateIndex(
                name: "IX_Konkurs_StudentID",
                table: "Konkurs",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Konkurs_TipKandidataID",
                table: "Konkurs",
                column: "TipKandidataID");

            migrationBuilder.CreateIndex(
                name: "IX_Lokacijas_KantonID",
                table: "Lokacijas",
                column: "KantonID");

            migrationBuilder.CreateIndex(
                name: "IX_Lokacijas_MjestoStanovanjaID",
                table: "Lokacijas",
                column: "MjestoStanovanjaID");

            migrationBuilder.CreateIndex(
                name: "IX_NajavaOdlaskas_UgovorID",
                table: "NajavaOdlaskas",
                column: "UgovorID");

            migrationBuilder.CreateIndex(
                name: "IX_Obavijests_RecepcioerID",
                table: "Obavijests",
                column: "RecepcioerID");

            migrationBuilder.CreateIndex(
                name: "IX_Obroks_KuharicaID",
                table: "Obroks",
                column: "KuharicaID");

            migrationBuilder.CreateIndex(
                name: "IX_Osobljes_KorisnikID",
                table: "Osobljes",
                column: "KorisnikID",
                unique: true,
                filter: "[KorisnikID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Osobljes_LokacijaID",
                table: "Osobljes",
                column: "LokacijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Osobljes_PolID",
                table: "Osobljes",
                column: "PolID");

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
                name: "IX_RezultatKonkursas_VrstaRazlogaOdbijanjaID",
                table: "RezultatKonkursas",
                column: "VrstaRazlogaOdbijanjaID");

            migrationBuilder.CreateIndex(
                name: "IX_RezultatKonkursas_VrstaStanjaKonkursaID",
                table: "RezultatKonkursas",
                column: "VrstaStanjaKonkursaID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_CiklusStudijaID",
                table: "Students",
                column: "CiklusStudijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_FakultetID",
                table: "Students",
                column: "FakultetID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_GodinaStudijaID",
                table: "Students",
                column: "GodinaStudijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_KorisnikID",
                table: "Students",
                column: "KorisnikID",
                unique: true,
                filter: "[KorisnikID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Students_LokacijaID",
                table: "Students",
                column: "LokacijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_MjestoRodjenjaID",
                table: "Students",
                column: "MjestoRodjenjaID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_PolID",
                table: "Students",
                column: "PolID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_TipKandidataID",
                table: "Students",
                column: "TipKandidataID");

            migrationBuilder.CreateIndex(
                name: "IX_Ugovors_DodanUgovorOsobljeID",
                table: "Ugovors",
                column: "DodanUgovorOsobljeID");

            migrationBuilder.CreateIndex(
                name: "IX_Ugovors_KarticaID",
                table: "Ugovors",
                column: "KarticaID");

            migrationBuilder.CreateIndex(
                name: "IX_Ugovors_SobaID",
                table: "Ugovors",
                column: "SobaID");

            migrationBuilder.CreateIndex(
                name: "IX_Ugovors_StudentID",
                table: "Ugovors",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Uplatas_NacinUplateID",
                table: "Uplatas",
                column: "NacinUplateID");

            migrationBuilder.CreateIndex(
                name: "IX_Uplatas_RecepcioerID",
                table: "Uplatas",
                column: "RecepcioerID");

            migrationBuilder.CreateIndex(
                name: "IX_Uplatas_UgovorID",
                table: "Uplatas",
                column: "UgovorID");

            migrationBuilder.CreateIndex(
                name: "IX_Upozorenjes_UgovorID",
                table: "Upozorenjes",
                column: "UgovorID");

            migrationBuilder.CreateIndex(
                name: "IX_Zahtjevs_UgovorID",
                table: "Zahtjevs",
                column: "UgovorID");

            migrationBuilder.CreateIndex(
                name: "IX_Zahtjevs_VrstaStanjaZahtjevaID",
                table: "Zahtjevs",
                column: "VrstaStanjaZahtjevaID");

            migrationBuilder.CreateIndex(
                name: "IX_Zahtjevs_VrstaZahtjevaID",
                table: "Zahtjevs",
                column: "VrstaZahtjevaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DnevnaPonudas");

            migrationBuilder.DropTable(
                name: "Drzavas");

            migrationBuilder.DropTable(
                name: "Konkurs");

            migrationBuilder.DropTable(
                name: "NajavaOdlaskas");

            migrationBuilder.DropTable(
                name: "Obavijests");

            migrationBuilder.DropTable(
                name: "Obroks");

            migrationBuilder.DropTable(
                name: "Uplatas");

            migrationBuilder.DropTable(
                name: "Upozorenjes");

            migrationBuilder.DropTable(
                name: "Zahtjevs");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "RezultatKonkursas");

            migrationBuilder.DropTable(
                name: "NacinUplates");

            migrationBuilder.DropTable(
                name: "Ugovors");

            migrationBuilder.DropTable(
                name: "VrstaStanjaZahtjevas");

            migrationBuilder.DropTable(
                name: "VrstaZahtjevas");

            migrationBuilder.DropTable(
                name: "vrstaRazlogaOdbijanjas");

            migrationBuilder.DropTable(
                name: "VrstaStanjaKonkursas");

            migrationBuilder.DropTable(
                name: "Osobljes");

            migrationBuilder.DropTable(
                name: "Karticas");

            migrationBuilder.DropTable(
                name: "Sobas");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "CiklusStudijas");

            migrationBuilder.DropTable(
                name: "Fakultets");

            migrationBuilder.DropTable(
                name: "GodinaStudijas");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Lokacijas");

            migrationBuilder.DropTable(
                name: "Pols");

            migrationBuilder.DropTable(
                name: "tipKandidatas");

            migrationBuilder.DropTable(
                name: "Grads");

            migrationBuilder.DropTable(
                name: "Kantons");
        }
    }
}
