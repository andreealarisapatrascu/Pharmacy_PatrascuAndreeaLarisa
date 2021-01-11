using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy_PatrascuAndreeaLarisa.Migrations
{
    public partial class ExtendedModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorie",
                columns: table => new
                {
                    CategorieID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeCategorie = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorie", x => x.CategorieID);
                });

            migrationBuilder.CreateTable(
                name: "Furnizor",
                columns: table => new
                {
                    FurnizorID = table.Column<int>(nullable: false),
                    NumeFurnizor = table.Column<string>(nullable: true),
                    Adresa = table.Column<string>(nullable: true),
                    Telefon = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Furnizor", x => x.FurnizorID);
                });

            migrationBuilder.CreateTable(
                name: "Tip",
                columns: table => new
                {
                    TipID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipMedicament = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tip", x => x.TipID);
                });

            migrationBuilder.CreateTable(
                name: "Produs",
                columns: table => new
                {
                    ProdusID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeMedicament = table.Column<string>(nullable: true),
                    CategorieID = table.Column<int>(nullable: false),
                    FurnizorID = table.Column<int>(nullable: false),
                    Doza = table.Column<string>(nullable: true),
                    Pret = table.Column<decimal>(type: "decimal(6, 2)", nullable: false),
                    DataExpirare = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produs", x => x.ProdusID);
                    table.ForeignKey(
                        name: "FK_Produs_Categorie_CategorieID",
                        column: x => x.CategorieID,
                        principalTable: "Categorie",
                        principalColumn: "CategorieID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Produs_Furnizor_FurnizorID",
                        column: x => x.FurnizorID,
                        principalTable: "Furnizor",
                        principalColumn: "FurnizorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormaProdus",
                columns: table => new
                {
                    TipID = table.Column<int>(nullable: false),
                    ProdusID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormaProdus", x => new { x.ProdusID, x.TipID });
                    table.ForeignKey(
                        name: "FK_FormaProdus_Produs_ProdusID",
                        column: x => x.ProdusID,
                        principalTable: "Produs",
                        principalColumn: "ProdusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormaProdus_Tip_TipID",
                        column: x => x.TipID,
                        principalTable: "Tip",
                        principalColumn: "TipID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormaProdus_TipID",
                table: "FormaProdus",
                column: "TipID");

            migrationBuilder.CreateIndex(
                name: "IX_Produs_CategorieID",
                table: "Produs",
                column: "CategorieID");

            migrationBuilder.CreateIndex(
                name: "IX_Produs_FurnizorID",
                table: "Produs",
                column: "FurnizorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormaProdus");

            migrationBuilder.DropTable(
                name: "Produs");

            migrationBuilder.DropTable(
                name: "Tip");

            migrationBuilder.DropTable(
                name: "Categorie");

            migrationBuilder.DropTable(
                name: "Furnizor");
        }
    }
}
