using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicHotelAPI.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablaHotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Hoteles",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "", "Detalle de Hotel", new DateTime(2023, 12, 26, 11, 53, 51, 997, DateTimeKind.Local).AddTicks(7007), new DateTime(2023, 12, 26, 11, 53, 51, 997, DateTimeKind.Local).AddTicks(6997), "", 50, "Hotel Real", 5, 200.0 },
                    { 2, "", "Detalle de Hotel", new DateTime(2023, 12, 26, 11, 53, 51, 997, DateTimeKind.Local).AddTicks(7010), new DateTime(2023, 12, 26, 11, 53, 51, 997, DateTimeKind.Local).AddTicks(7009), "", 40, "Premium Vista a la Piscina", 4, 150.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hoteles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hoteles",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
