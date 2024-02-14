using MagicHotel_Web.Models.Dto;

namespace MagicHotel_Web.Services.IServices
{
    public interface IHotelService
    {
        Task<T> ObtenerTodos<T>(string token);
        Task<T> ObtenerTodosPaginado<T>(string token, int pageNumber = 1, int pageSize = 4);
        Task<T> Obtener<T>(int id, string token);
        Task<T> Crear<T>(HotelCreateDto dto, string token);
        Task<T> Actualizar<T>(HotelUpdateDto dto, string token);
        Task<T> Remover<T>(int id, string token);
    }
}
