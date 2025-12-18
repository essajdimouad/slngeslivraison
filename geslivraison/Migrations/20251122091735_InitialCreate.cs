using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace geslivraison.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administrateurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrateurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DemandesLivraison",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdresseDepart = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdresseArrivee = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Statut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: true),
                    AdministrateurId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemandesLivraison", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DemandesLivraison_Administrateurs_AdministrateurId",
                        column: x => x.AdministrateurId,
                        principalTable: "Administrateurs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DemandesLivraison_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoriquesStatuts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Statut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateChangement = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DemandeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoriquesStatuts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoriquesStatuts_DemandesLivraison_DemandeId",
                        column: x => x.DemandeId,
                        principalTable: "DemandesLivraison",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DemandesLivraison_AdministrateurId",
                table: "DemandesLivraison",
                column: "AdministrateurId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandesLivraison_ClientId",
                table: "DemandesLivraison",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoriquesStatuts_DemandeId",
                table: "HistoriquesStatuts",
                column: "DemandeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoriquesStatuts");

            migrationBuilder.DropTable(
                name: "DemandesLivraison");

            migrationBuilder.DropTable(
                name: "Administrateurs");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
