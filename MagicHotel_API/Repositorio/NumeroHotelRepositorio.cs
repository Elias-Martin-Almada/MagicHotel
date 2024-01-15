using MagicHotel_API.Datos;
using MagicHotel_API.Modelos;
using MagicHotel_API.Repositorio.IRepositorio;

namespace MagicHotel_API.Repositorio
{
    public class NumeroHotelRepositorio : Repositorio<NumeroHotel>, INumeroHotelRepositorio
    {
        private readonly ApplicationDbContext _db;

        public NumeroHotelRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<NumeroHotel> Actualizar(NumeroHotel entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.NumeroHoteles.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
