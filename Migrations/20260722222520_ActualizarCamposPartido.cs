using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UTNGolCoinApi.Migrations
{
    /// <inheritdoc />
    public partial class ActualizarCamposPartido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Equipo1",
                table: "Partidos");

            migrationBuilder.DropColumn(
                name: "Equipo2",
                table: "Partidos");

            migrationBuilder.AddColumn<string>(
                name: "Fase",
                table: "Partidos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GolesLocal",
                table: "Partidos",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GolesVisitante",
                table: "Partidos",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Grupo",
                table: "Partidos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreSede",
                table: "Partidos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumeroPartidoFifa",
                table: "Partidos",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SedeId",
                table: "Partidos",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeleccionLocal",
                table: "Partidos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeleccionLocalId",
                table: "Partidos",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeleccionVisitante",
                table: "Partidos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeleccionVisitanteId",
                table: "Partidos",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fase",
                table: "Partidos");

            migrationBuilder.DropColumn(
                name: "GolesLocal",
                table: "Partidos");

            migrationBuilder.DropColumn(
                name: "GolesVisitante",
                table: "Partidos");

            migrationBuilder.DropColumn(
                name: "Grupo",
                table: "Partidos");

            migrationBuilder.DropColumn(
                name: "NombreSede",
                table: "Partidos");

            migrationBuilder.DropColumn(
                name: "NumeroPartidoFifa",
                table: "Partidos");

            migrationBuilder.DropColumn(
                name: "SedeId",
                table: "Partidos");

            migrationBuilder.DropColumn(
                name: "SeleccionLocal",
                table: "Partidos");

            migrationBuilder.DropColumn(
                name: "SeleccionLocalId",
                table: "Partidos");

            migrationBuilder.DropColumn(
                name: "SeleccionVisitante",
                table: "Partidos");

            migrationBuilder.DropColumn(
                name: "SeleccionVisitanteId",
                table: "Partidos");

            migrationBuilder.AddColumn<string>(
                name: "Equipo1",
                table: "Partidos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Equipo2",
                table: "Partidos",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
