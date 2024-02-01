using MagicHotel_Web.Models.Dto;

namespace MagicHotel_Web.Services.IServices
{
    public interface IHotelService
    {
        Task<T> ObtenerTodos<T>();
        Task<T> Obtener<T>(int id);
        Task<T> Crear<T>(HotelCreateDto dto);
        Task<T> Actualizar<T>(HotelUpdateDto dto);
        Task<T> Remover<T>(int id);
    }
}
