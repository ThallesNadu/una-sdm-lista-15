using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenDriveApi.Migrations
{
    /// <inheritdoc />
    public partial class AddCidadeEstacaoCarga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "CustoProcessamento",
                table: "OrdensReciclagem",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "EstacoesCarga",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "EstacoesCarga");

            migrationBuilder.AlterColumn<decimal>(
                name: "CustoProcessamento",
                table: "OrdensReciclagem",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");
        }
    }
}
