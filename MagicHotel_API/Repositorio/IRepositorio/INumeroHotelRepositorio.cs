using MagicHotel_API.Modelos;

namespace MagicHotel_API.Repositorio.IRepositorio
{
    public interface INumeroHotelRepositorio : IRepositorio<NumeroHotel>
    {
        Task<NumeroHotel> Actualizar(NumeroHotel entidad);
    }
}
