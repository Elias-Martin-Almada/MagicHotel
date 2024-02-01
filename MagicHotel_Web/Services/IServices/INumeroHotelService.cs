using MagicHotel_Web.Models.Dto;

namespace MagicHotel_Web.Services.IServices
{
    public interface INumeroHotelService
    {
        Task<T> ObtenerTodos<T>();
        Task<T> Obtener<T>(int id);
        Task<T> Crear<T>(NumeroHotelCreateDto dto);
        Task<T> Actualizar<T>(NumeroHoteUpdatelDto dto);
        Task<T> Remover<T>(int id);
    }
}
