﻿// <auto-generated />
using System;
using MagicHotel_API.Datos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MagicHotelAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240202135927_UsuarioMigration")]
    partial class UsuarioMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MagicHotel_API.Modelos.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenidad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Detalle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImagenUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MetrosCuadrados")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ocupantes")
                        .HasColumnType("int");

                    b.Property<double>("Tarifa")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Hoteles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenidad = "",
                            Detalle = "Detalle de Hotel",
                            FechaActualizacion = new DateTime(2024, 2, 2, 10, 59, 26, 947, DateTimeKind.Local).AddTicks(3346),
                            FechaCreacion = new DateTime(2024, 2, 2, 10, 59, 26, 947, DateTimeKind.Local).AddTicks(3320),
                            ImagenUrl = "",
                            MetrosCuadrados = 50,
                            Nombre = "Hotel Real",
                            Ocupantes = 5,
                            Tarifa = 200.0
                        },
                        new
                        {
                            Id = 2,
                            Amenidad = "",
                            Detalle = "Detalle de Hotel",
                            FechaActualizacion = new DateTime(2024, 2, 2, 10, 59, 26, 947, DateTimeKind.Local).AddTicks(3350),
                            FechaCreacion = new DateTime(2024, 2, 2, 10, 59, 26, 947, DateTimeKind.Local).AddTicks(3349),
                            ImagenUrl = "",
                            MetrosCuadrados = 40,
                            Nombre = "Premium Vista a la Piscina",
                            Ocupantes = 4,
                            Tarifa = 150.0
                        });
                });

            modelBuilder.Entity("MagicHotel_API.Modelos.NumeroHotel", b =>
                {
                    b.Property<int>("HotelNo")
                        .HasColumnType("int");

                    b.Property<string>("DetalleEspecial")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.HasKey("HotelNo");

                    b.HasIndex("HotelId");

                    b.ToTable("NumeroHoteles");
                });

            modelBuilder.Entity("MagicHotel_API.Modelos.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombres")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rol")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("MagicHotel_API.Modelos.NumeroHotel", b =>
                {
                    b.HasOne("MagicHotel_API.Modelos.Hotel", "Hotel")
                        .WithMany()
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });
#pragma warning restore 612, 618
        }
    }
}