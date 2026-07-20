using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UTNGolCoinApi.Migrations
{
    /// <inheritdoc />
    public partial class SincronizarPredicciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BilleteraId",
                table: "Predicciones",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PartidoCodigo",
                table: "Predicciones",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BilleteraId",
                table: "Predicciones");

            migrationBuilder.DropColumn(
                name: "PartidoCodigo",
                table: "Predicciones");
        }
    }
}
