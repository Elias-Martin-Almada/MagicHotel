using MagicHotel_API.Modelos;
using Microsoft.EntityFrameworkCore; // Agrego para usar DbContext

namespace MagicHotel_API.Datos
{
    // Esta Clase se usa para crear los modelos en la DB como tablas 
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
		public DbSet<Usuario> Usuarios { get; set; }
		public DbSet<Hotel> Hoteles { get; set; }
        public DbSet<NumeroHotel> NumeroHoteles { get; set; }
        // Metodo para crear registros en la tabla Hoteles en DB
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel()
                {
                    Id=1,
                    Nombre="Hotel Real",
                    Detalle="Detalle de Hotel",
                    ImagenUrl="",
                    Ocupantes=5,
                    MetrosCuadrados=50,
                    Tarifa=200,
                    Amenidad="",
                    FechaCreacion= DateTime.Now,
                    FechaActualizacion= DateTime.Now
                },
                new Hotel()
                {
                    Id = 2,
                    Nombre = "Premium Vista a la Piscina",
                    Detalle = "Detalle de Hotel",
                    ImagenUrl = "",
                    Ocupantes = 4,
                    MetrosCuadrados = 40,
                    Tarifa = 150,
                    Amenidad = "",
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                }
            );
        }
        
    }
}
