using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UTNGolCoinApi.Migrations
{
    /// <inheritdoc />
    public partial class SincronizacionDeModelos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EquipoVisitante",
                table: "Partidos",
                newName: "Equipo2");

            migrationBuilder.RenameColumn(
                name: "EquipoLocal",
                table: "Partidos",
                newName: "Equipo1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Equipo2",
                table: "Partidos",
                newName: "EquipoVisitante");

            migrationBuilder.RenameColumn(
                name: "Equipo1",
                table: "Partidos",
                newName: "EquipoLocal");
        }
    }
}
