using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarioSimulador.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Simulaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dias = table.Column<int>(type: "int", nullable: false),
                    StockInicial = table.Column<int>(type: "int", nullable: false),
                    PuntoReorden = table.Column<int>(type: "int", nullable: false),
                    CantidadReorden = table.Column<int>(type: "int", nullable: false),
                    DiasReabastecimiento = table.Column<int>(type: "int", nullable: false),
                    Iteraciones = table.Column<int>(type: "int", nullable: false),
                    PromedioDesabasto = table.Column<double>(type: "float", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simulaciones", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Simulaciones");
        }
    }
}
