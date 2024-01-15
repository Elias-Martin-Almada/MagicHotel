using MagicHotel_API.Datos;
using MagicHotel_API.Modelos;
using MagicHotel_API.Repositorio.IRepositorio;

namespace MagicHotel_API.Repositorio
{
    public class HotelRepositorio : Repositorio<Hotel>, IHotelRepositorio
    {
        private readonly ApplicationDbContext _db;

        public HotelRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Hotel> Actualizar(Hotel entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.Hoteles.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
