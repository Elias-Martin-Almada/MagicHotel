using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicHotelAPI.Migrations
{
    /// <inheritdoc />
    public partial class UsuarioMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Hoteles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 2, 2, 10, 59, 26, 947, DateTimeKind.Local).AddTicks(3346), new DateTime(2024, 2, 2, 10, 59, 26, 947, DateTimeKind.Local).AddTicks(3320) });

            migrationBuilder.UpdateData(
                table: "Hoteles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 2, 2, 10, 59, 26, 947, DateTimeKind.Local).AddTicks(3350), new DateTime(2024, 2, 2, 10, 59, 26, 947, DateTimeKind.Local).AddTicks(3349) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");

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
    }
}
