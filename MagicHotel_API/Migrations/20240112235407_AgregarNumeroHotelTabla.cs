using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicHotelAPI.Migrations
{
    /// <inheritdoc />
    public partial class AgregarNumeroHotelTabla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NumeroHoteles",
                columns: table => new
                {
                    HotelNo = table.Column<int>(type: "int", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    DetalleEspecial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumeroHoteles", x => x.HotelNo);
                    table.ForeignKey(
                        name: "FK_NumeroHoteles_Hoteles_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hoteles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Hoteles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 1, 12, 20, 54, 6, 892, DateTimeKind.Local).AddTicks(3787), new DateTime(2024, 1, 12, 20, 54, 6, 892, DateTimeKind.Local).AddTicks(3777) });

            migrationBuilder.UpdateData(
                table: "Hoteles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 1, 12, 20, 54, 6, 892, DateTimeKind.Local).AddTicks(3789), new DateTime(2024, 1, 12, 20, 54, 6, 892, DateTimeKind.Local).AddTicks(3789) });

            migrationBuilder.CreateIndex(
                name: "IX_NumeroHoteles_HotelId",
                table: "NumeroHoteles",
                column: "HotelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumeroHoteles");

            migrationBuilder.UpdateData(
                table: "Hoteles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 12, 26, 11, 53, 51, 997, DateTimeKind.Local).AddTicks(7007), new DateTime(2023, 12, 26, 11, 53, 51, 997, DateTimeKind.Local).AddTicks(6997) });

            migrationBuilder.UpdateData(
                table: "Hoteles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 12, 26, 11, 53, 51, 997, DateTimeKind.Local).AddTicks(7010), new DateTime(2023, 12, 26, 11, 53, 51, 997, DateTimeKind.Local).AddTicks(7009) });
        }
    }
}
