using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace gesteco.api.src.gesteco.WebApi.Database.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adresse",
                columns: table => new
                {
                    IdCivique = table.Column<string>(nullable: false),
                    Nom = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresse", x => x.IdCivique);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    IdClient = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreation = table.Column<DateTime>(nullable: false),
                    Nom = table.Column<string>(maxLength: 100, nullable: false),
                    Prenom = table.Column<string>(maxLength: 100, nullable: false),
                    Immaticulation = table.Column<string>(maxLength: 100, nullable: true),
                    Courriel = table.Column<string>(maxLength: 100, nullable: true),
                    NomCommerce = table.Column<string>(nullable: true),
                    Telephone = table.Column<string>(maxLength: 10, nullable: false),
                    IdCivique = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.IdClient);
                });

            migrationBuilder.CreateTable(
                name: "Ecocentre",
                columns: table => new
                {
                    IdEcocentre = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(maxLength: 100, nullable: false),
                    Adresse = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ecocentre", x => x.IdEcocentre);
                });

            migrationBuilder.CreateTable(
                name: "Matiere",
                columns: table => new
                {
                    IdMatiere = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 300, nullable: false),
                    Comptable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matiere", x => x.IdMatiere);
                });

            migrationBuilder.CreateTable(
                name: "ModePaiement",
                columns: table => new
                {
                    IdModePaiement = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModePaiement", x => x.IdModePaiement);
                });

            migrationBuilder.CreateTable(
                name: "Provenance",
                columns: table => new
                {
                    IdProvenance = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCivique = table.Column<string>(nullable: true),
                    Adresse = table.Column<string>(maxLength: 250, nullable: false),
                    Quantite_Disponible = table.Column<long>(nullable: false),
                    Quantite_Initiale = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provenance", x => x.IdProvenance);
                });

            migrationBuilder.CreateTable(
                name: "Tarification",
                columns: table => new
                {
                    IdTarification = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prix = table.Column<double>(nullable: false),
                    Prix_Commerce = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarification", x => x.IdTarification);
                });

            migrationBuilder.CreateTable(
                name: "Quota",
                columns: table => new
                {
                    IdQuota = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateDebut = table.Column<DateTime>(nullable: false),
                    DateFin = table.Column<DateTime>(nullable: false),
                    Quantite_Disponible = table.Column<long>(nullable: false),
                    Quantite_Initiale = table.Column<long>(nullable: false),
                    IdCivique = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quota", x => x.IdQuota);
                    table.ForeignKey(
                        name: "FK_Quota_Adresse_IdCivique",
                        column: x => x.IdCivique,
                        principalTable: "Adresse",
                        principalColumn: "IdCivique",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Entreprise",
                columns: table => new
                {
                    IdEntreprise = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdClient = table.Column<long>(nullable: false),
                    Nom = table.Column<string>(maxLength: 100, nullable: true),
                    IClient = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entreprise", x => x.IdEntreprise);
                    table.ForeignKey(
                        name: "FK_Entreprise_Client_IdClient",
                        column: x => x.IdClient,
                        principalTable: "Client",
                        principalColumn: "IdClient",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Visite",
                columns: table => new
                {
                    IdVisite = table.Column<long>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                    IClient = table.Column<long>(nullable: false),
                    IdProvenance = table.Column<long>(nullable: false),
                    IdEcocentre = table.Column<long>(nullable: false),
                    Employe = table.Column<string>(nullable: true),
                    DateCreation = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visite", x => x.IdVisite);
                    table.ForeignKey(
                        name: "FK_Visite_Client_IClient",
                        column: x => x.IClient,
                        principalTable: "Client",
                        principalColumn: "IdClient",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visite_Ecocentre_IdEcocentre",
                        column: x => x.IdEcocentre,
                        principalTable: "Ecocentre",
                        principalColumn: "IdEcocentre",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visite_Provenance_IdProvenance",
                        column: x => x.IdProvenance,
                        principalTable: "Provenance",
                        principalColumn: "IdProvenance",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Historique_Quota",
                columns: table => new
                {
                    IdHistorique_Quota = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateHistorique = table.Column<DateTime>(nullable: false),
                    DateDebut = table.Column<DateTime>(nullable: false),
                    DateFin = table.Column<DateTime>(nullable: false),
                    Quantite_Utilisee = table.Column<long>(nullable: false),
                    Quantite_Initiale = table.Column<long>(nullable: false),
                    IdCivique = table.Column<string>(nullable: false),
                    IdQuota = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historique_Quota", x => x.IdHistorique_Quota);
                    table.ForeignKey(
                        name: "FK_Historique_Quota_Quota_IdQuota",
                        column: x => x.IdQuota,
                        principalTable: "Quota",
                        principalColumn: "IdQuota",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matiere_Visite",
                columns: table => new
                {
                    IdMatiere_Visite = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdVisite = table.Column<long>(nullable: false),
                    Description = table.Column<string>(maxLength: 300, nullable: false),
                    Comptable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matiere_Visite", x => x.IdMatiere_Visite);
                    table.ForeignKey(
                        name: "FK_Matiere_Visite_Visite_IdVisite",
                        column: x => x.IdVisite,
                        principalTable: "Visite",
                        principalColumn: "IdVisite",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    IdTransaction = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdModePaiement = table.Column<long>(nullable: false),
                    IdVisite = table.Column<long>(nullable: false),
                    Hauteur = table.Column<long>(nullable: false),
                    Largeur = table.Column<long>(nullable: false),
                    Longueur = table.Column<long>(nullable: false),
                    Quantite_Utilisee = table.Column<long>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    Volume = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.IdTransaction);
                    table.ForeignKey(
                        name: "FK_Transaction_ModePaiement_IdModePaiement",
                        column: x => x.IdModePaiement,
                        principalTable: "ModePaiement",
                        principalColumn: "IdModePaiement",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_Visite_IdVisite",
                        column: x => x.IdVisite,
                        principalTable: "Visite",
                        principalColumn: "IdVisite",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entreprise_IdClient",
                table: "Entreprise",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_Historique_Quota_IdQuota",
                table: "Historique_Quota",
                column: "IdQuota");

            migrationBuilder.CreateIndex(
                name: "IX_Matiere_Visite_IdVisite",
                table: "Matiere_Visite",
                column: "IdVisite");

            migrationBuilder.CreateIndex(
                name: "IX_Quota_IdCivique",
                table: "Quota",
                column: "IdCivique");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_IdModePaiement",
                table: "Transaction",
                column: "IdModePaiement");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_IdVisite",
                table: "Transaction",
                column: "IdVisite",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visite_IClient",
                table: "Visite",
                column: "IClient");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entreprise");

            migrationBuilder.DropTable(
                name: "Historique_Quota");

            migrationBuilder.DropTable(
                name: "Matiere");

            migrationBuilder.DropTable(
                name: "Matiere_Visite");

            migrationBuilder.DropTable(
                name: "Tarification");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Quota");

            migrationBuilder.DropTable(
                name: "ModePaiement");

            migrationBuilder.DropTable(
                name: "Visite");

            migrationBuilder.DropTable(
                name: "Adresse");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Ecocentre");

            migrationBuilder.DropTable(
                name: "Provenance");
        }
    }
}
