using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicHotelAPI.Migrations
{
    /// <inheritdoc />
    public partial class ControlarNullables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DetalleEspecial",
                table: "NumeroHoteles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ImagenUrl",
                table: "Hoteles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Detalle",
                table: "Hoteles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Amenidad",
                table: "Hoteles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Hoteles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 1, 21, 11, 44, 21, 119, DateTimeKind.Local).AddTicks(7846), new DateTime(2024, 1, 21, 11, 44, 21, 119, DateTimeKind.Local).AddTicks(7836) });

            migrationBuilder.UpdateData(
                table: "Hoteles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 1, 21, 11, 44, 21, 119, DateTimeKind.Local).AddTicks(7850), new DateTime(2024, 1, 21, 11, 44, 21, 119, DateTimeKind.Local).AddTicks(7849) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DetalleEspecial",
                table: "NumeroHoteles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImagenUrl",
                table: "Hoteles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Detalle",
                table: "Hoteles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Amenidad",
                table: "Hoteles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
        }
    }
}
