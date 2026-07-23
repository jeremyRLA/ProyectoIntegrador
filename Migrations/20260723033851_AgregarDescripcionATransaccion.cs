using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UTNGolCoinApi.Migrations
{
    /// <inheritdoc />
    public partial class AgregarDescripcionATransaccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Transacciones",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Transacciones");
        }
    }
}
