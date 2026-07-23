using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UTNGolCoinApi.Migrations
{
    /// <inheritdoc />
    public partial class RemoverPartidos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Partidos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Partidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    CuotaEmpate = table.Column<decimal>(type: "numeric", nullable: false),
                    CuotaLocal = table.Column<decimal>(type: "numeric", nullable: false),
                    CuotaVisitante = table.Column<decimal>(type: "numeric", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    Fase = table.Column<string>(type: "text", nullable: true),
                    FechaPartido = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GolesLocal = table.Column<int>(type: "integer", nullable: true),
                    GolesVisitante = table.Column<int>(type: "integer", nullable: true),
                    Grupo = table.Column<string>(type: "text", nullable: true),
                    NombreSede = table.Column<string>(type: "text", nullable: true),
                    NumeroPartidoFifa = table.Column<int>(type: "integer", nullable: true),
                    SedeId = table.Column<int>(type: "integer", nullable: true),
                    SeleccionLocal = table.Column<string>(type: "text", nullable: true),
                    SeleccionLocalId = table.Column<int>(type: "integer", nullable: true),
                    SeleccionVisitante = table.Column<string>(type: "text", nullable: true),
                    SeleccionVisitanteId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partidos", x => x.Id);
                });
        }
    }
}
