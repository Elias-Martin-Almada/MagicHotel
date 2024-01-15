using MagicHotel_API.Modelos;

namespace MagicHotel_API.Repositorio.IRepositorio
{
    public interface IHotelRepositorio : IRepositorio<Hotel>
    {
        Task<Hotel> Actualizar(Hotel entidad);
    }
}
