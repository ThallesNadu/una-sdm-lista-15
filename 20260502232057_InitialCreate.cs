using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenDriveApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Baterias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumeroSerie = table.Column<string>(type: "TEXT", nullable: false),
                    CapacidadeKWh = table.Column<double>(type: "REAL", nullable: false),
                    SaudeBateria = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baterias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstacoesCarga",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Localizacao = table.Column<string>(type: "TEXT", nullable: false),
                    TipoCarga = table.Column<string>(type: "TEXT", nullable: false),
                    CargaDisponivelKW = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstacoesCarga", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrdensReciclagem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BateriaId = table.Column<int>(type: "INTEGER", nullable: false),
                    EstacaoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Prioridade = table.Column<string>(type: "TEXT", nullable: false),
                    CustoProcessamento = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdensReciclagem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegistrosTelemetria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BateriaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Temperatura = table.Column<double>(type: "REAL", nullable: false),
                    Velocidade = table.Column<double>(type: "REAL", nullable: false),
                    DataLeitura = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosTelemetria", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Baterias");

            migrationBuilder.DropTable(
                name: "EstacoesCarga");

            migrationBuilder.DropTable(
                name: "OrdensReciclagem");

            migrationBuilder.DropTable(
                name: "RegistrosTelemetria");
        }
    }
}
