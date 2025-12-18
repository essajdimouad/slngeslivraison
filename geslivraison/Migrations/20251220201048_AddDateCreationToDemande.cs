using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace geslivraison.Migrations
{
    /// <inheritdoc />
    public partial class AddDateCreationToDemande : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DemandesLivraison_Administrateurs_AdministrateurId",
                table: "DemandesLivraison");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoriquesStatuts_DemandesLivraison_DemandeId",
                table: "HistoriquesStatuts");

            migrationBuilder.DropIndex(
                name: "IX_HistoriquesStatuts_DemandeId",
                table: "HistoriquesStatuts");

            migrationBuilder.DropIndex(
                name: "IX_DemandesLivraison_AdministrateurId",
                table: "DemandesLivraison");

            migrationBuilder.DropColumn(
                name: "Statut",
                table: "HistoriquesStatuts");

            migrationBuilder.DropColumn(
                name: "AdministrateurId",
                table: "DemandesLivraison");

            migrationBuilder.RenameColumn(
                name: "DemandeId",
                table: "HistoriquesStatuts",
                newName: "NouveauStatut");

            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "Clients",
                newName: "Adresse");

            migrationBuilder.AddColumn<int>(
                name: "AncienStatut",
                table: "HistoriquesStatuts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DemandeLivraisonId",
                table: "HistoriquesStatuts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Statut",
                table: "DemandesLivraison",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreation",
                table: "DemandesLivraison",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Administrateurs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_HistoriquesStatuts_DemandeLivraisonId",
                table: "HistoriquesStatuts",
                column: "DemandeLivraisonId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandesLivraison_AdminId",
                table: "DemandesLivraison",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_DemandesLivraison_Administrateurs_AdminId",
                table: "DemandesLivraison",
                column: "AdminId",
                principalTable: "Administrateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoriquesStatuts_DemandesLivraison_DemandeLivraisonId",
                table: "HistoriquesStatuts",
                column: "DemandeLivraisonId",
                principalTable: "DemandesLivraison",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DemandesLivraison_Administrateurs_AdminId",
                table: "DemandesLivraison");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoriquesStatuts_DemandesLivraison_DemandeLivraisonId",
                table: "HistoriquesStatuts");

            migrationBuilder.DropIndex(
                name: "IX_HistoriquesStatuts_DemandeLivraisonId",
                table: "HistoriquesStatuts");

            migrationBuilder.DropIndex(
                name: "IX_DemandesLivraison_AdminId",
                table: "DemandesLivraison");

            migrationBuilder.DropColumn(
                name: "AncienStatut",
                table: "HistoriquesStatuts");

            migrationBuilder.DropColumn(
                name: "DemandeLivraisonId",
                table: "HistoriquesStatuts");

            migrationBuilder.DropColumn(
                name: "DateCreation",
                table: "DemandesLivraison");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Administrateurs");

            migrationBuilder.RenameColumn(
                name: "NouveauStatut",
                table: "HistoriquesStatuts",
                newName: "DemandeId");

            migrationBuilder.RenameColumn(
                name: "Adresse",
                table: "Clients",
                newName: "RequestId");

            migrationBuilder.AddColumn<string>(
                name: "Statut",
                table: "HistoriquesStatuts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Statut",
                table: "DemandesLivraison",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AdministrateurId",
                table: "DemandesLivraison",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoriquesStatuts_DemandeId",
                table: "HistoriquesStatuts",
                column: "DemandeId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandesLivraison_AdministrateurId",
                table: "DemandesLivraison",
                column: "AdministrateurId");

            migrationBuilder.AddForeignKey(
                name: "FK_DemandesLivraison_Administrateurs_AdministrateurId",
                table: "DemandesLivraison",
                column: "AdministrateurId",
                principalTable: "Administrateurs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoriquesStatuts_DemandesLivraison_DemandeId",
                table: "HistoriquesStatuts",
                column: "DemandeId",
                principalTable: "DemandesLivraison",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
