using MagicHotel_Web.Models.Dto;

namespace MagicHotel_Web.Services.IServices
{
    public interface INumeroHotelService
    {
        Task<T> ObtenerTodos<T>(string token);
        Task<T> Obtener<T>(int id, string token);
        Task<T> Crear<T>(NumeroHotelCreateDto dto, string token);
        Task<T> Actualizar<T>(NumeroHoteUpdatelDto dto, string token);
        Task<T> Remover<T>(int id, string token);
    }
}
